using Academy.FilesService.Presentation;
using Academy.CourseManagement.Presentation;
using Academy.Accounts.Presentation;
using Academy.Web.Middlewares;
using Academy.Web.Extensions;
using Academy.Accounts.Infrastructure.Seeding;
using Academy.Management.Presentation;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CoursesController).Assembly)
    .AddApplicationPart(typeof(FilesController).Assembly)
    .AddApplicationPart(typeof(AuthoringsController).Assembly);


builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenConfiguration();

builder.Services
    .AddCourseManagementModule()
    .AddFilesService(builder.Configuration)
    .AddAccountsService(builder.Configuration)
    .AddManagementService();

var app = builder.Build();

var accountsSeeder = app.Services.GetRequiredService<AccountsSeeder>();
await accountsSeeder.SeedAsync();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.MapControllers();

app.Run();
