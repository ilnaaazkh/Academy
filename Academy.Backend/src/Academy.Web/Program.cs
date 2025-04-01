using Academy.FilesService.Presentation;
using Academy.CourseManagement.Presentation;
using Academy.Accounts.Presentation;
using Academy.Web.Migrations;
using Academy.Web.Middlewares;
using Academy.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddApplicationPart(typeof(CoursesController).Assembly)
    .AddApplicationPart(typeof(FilesController).Assembly);


builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenConfiguration();

builder.Services
    .AddCourseManagementModule()
    .AddFilesService(builder.Configuration)
    .AddAccountsService(builder.Configuration);

var app = builder.Build();
app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
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
