using Academy.CourseManagement.Application.Courses.CodeExecution;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.ComponentModel;
using System.Threading;

namespace Academy.CourseManagement.Infrastructure.CodeRunner
{
    public class DockerCodeRunner : ICodeRunner
    {
        private const string IMAGENAME = "csharp-sandbox";
        private const string DOCKERFILE = "CodeRunnerDockerfile";
        private readonly DockerClient _client;

        public DockerCodeRunner()
        {
            _client = new DockerClientConfiguration()
                .CreateClient();
        }

        public async Task<Result<string, Error>> Run(string code, CancellationToken cancellationToken)
        {

            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            await File.WriteAllTextAsync(Path.Combine(tempDir, "Program.cs"), code, cancellationToken);

            var csproj = @"<Project Sdk=""Microsoft.NET.Sdk"">
                              <PropertyGroup>
                                <OutputType>Exe</OutputType>
                                <TargetFramework>net8.0</TargetFramework>
                              </PropertyGroup>
                            </Project>";

            await File.WriteAllTextAsync(Path.Combine(tempDir, "UserCode.csproj"), csproj, cancellationToken);

            const string image = "mcr.microsoft.com/dotnet/sdk:8.0";

            var container = await _client.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = image,
                Cmd = new[] { "/bin/sh", "-c", "dotnet build && dotnet run --no-build" },
                HostConfig = new HostConfig
                {
                    Binds = new List<string> { $"{tempDir}:/app" },
                    AutoRemove = true,
                    Memory = 256 * 1024 * 1024,
                    MemorySwap = 256 * 1024 * 1024,
                },
                WorkingDir = "/app"
            }, cancellationToken);

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(5));

            await _client.Containers.StartContainerAsync(container.ID, null, cts.Token);

            var logTask = Task.Run(async () =>
            {
                using var logStream = await _client.Containers.GetContainerLogsAsync(
                    container.ID,
                    tty: false,
                    new ContainerLogsParameters
                    {
                        ShowStdout = true,
                        ShowStderr = true,
                        Follow = true
                    },
                    cancellationToken
                );

                return await logStream.ReadOutputToEndAsync(cancellationToken);
            });

            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);

            var completedTask = await Task.WhenAny(logTask, timeoutTask);

            if (completedTask == timeoutTask)
            {
                await _client.Containers.KillContainerAsync(container.ID, new ContainerKillParameters(), cancellationToken);
                return "Время выполнения кода превышено.";
            }

            var (stdout, stderr) = await logTask;

            var cleanOutput = ExtractUserOutput(stdout);

            var result = string.IsNullOrWhiteSpace(stderr) ? cleanOutput : $"{cleanOutput}\nErrors:\n{stderr}";

            return result;
        }

        private string ExtractUserOutput(string log)
        {
            if (string.IsNullOrWhiteSpace(log))
                return string.Empty;

            var lines = log.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                           .Select(l => l.TrimEnd('\r'))
                           .ToList();

            // Найдём последнюю строку со "Time Elapsed", которая сигнализирует об окончании билда
            var buildEndIndex = lines.FindLastIndex(l => l.Contains("Time Elapsed"));

            // Если строка найдена и не последняя — вернём все строки после неё
            if (buildEndIndex != -1 && buildEndIndex + 1 < lines.Count)
            {
                return string.Join('\n', lines.Skip(buildEndIndex + 1)).Trim();
            }

            // Если не нашли — пытаемся найти "Build succeeded." или "Build FAILED."
            var fallbackIndex = lines.FindLastIndex(l => l.Contains("Build succeeded.") || l.Contains("Build FAILED."));
            if (fallbackIndex != -1 && fallbackIndex + 1 < lines.Count)
            {
                return string.Join('\n', lines.Skip(fallbackIndex + 1)).Trim();
            }

            // В крайнем случае возвращаем всё
            return string.Join('\n', lines).Trim();
        }

    }
}
