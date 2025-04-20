using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.Core.Models;
using Academy.FilesService.Contracts;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;

namespace Academy.Management.Application.Authorings.AddFilesToAuthoring
{
    public record AddAttachemntsToAuthoringCommand(Guid AuthoringId, IEnumerable<UploadFileCommand> Files) : ICommand;

    public class AddAttachemntsToAuthoringCommandHandler : ICommandHandler<IReadOnlyList<string>, AddAttachemntsToAuthoringCommand>
    {
        private const string BUCKET = "authorings";
        private readonly string[] ALLOWED_EXTENSIONS = [".pdf", ".doc", ".docs", ".jpg", ".png", ".jpeg"];
        private readonly IAuthoringsRepository _authoringsRepository;
        private readonly IFilesServiceContract _filesService;

        public AddAttachemntsToAuthoringCommandHandler(
            IAuthoringsRepository authoringsRepository,
            IFilesServiceContract filesService)
        {
            _authoringsRepository = authoringsRepository;
            _filesService = filesService;
        }

        public async Task<Result<IReadOnlyList<string>, ErrorList>> Handle(
            AddAttachemntsToAuthoringCommand command,
            CancellationToken cancellationToken = default)
        {
            var invalidExtensions = command.Files
                .Select(f => Path.GetExtension(f.FileName))
                .Where(ext => !ALLOWED_EXTENSIONS.Contains(ext, StringComparer.OrdinalIgnoreCase))
                .Distinct()
                .ToArray();

            if (invalidExtensions.Any())
            {
                var extList = string.Join(", ", invalidExtensions);
                return Error.Validation(
                    "invalid.file.extensions",
                    $"Files with the following extensions are not allowed: {extList}",
                    nameof(command.Files)
                ).ToErrorList();
            }

            var authoring = await _authoringsRepository.GetById(command.AuthoringId, cancellationToken);
            if (authoring is null)
                return Errors.General.NotFound(command.AuthoringId).ToErrorList();

            var uploadResult = await _filesService.UploadFiles(command.Files, BUCKET, cancellationToken);
            if (uploadResult.IsFailure)
                return uploadResult.Error;

            var attachments = new List<Attachment>();

            foreach (var (file, path) in command.Files.Zip(uploadResult.Value))
            {
                var attachment = Attachment.Create(file.FileName, path);
                if (attachment.IsFailure)
                    return attachment.Error.ToErrorList();

                attachments.Add(attachment.Value);
            }

            authoring.AddAttachments(attachments);
            await _authoringsRepository.Save(authoring, cancellationToken);

            return attachments.Select(a => a.FileUrl).ToList();
        }

    }
}
