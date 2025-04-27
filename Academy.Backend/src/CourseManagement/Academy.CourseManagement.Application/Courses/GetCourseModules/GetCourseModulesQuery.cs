using Academy.Core.Abstractions;
using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Academy.CourseManagement.Application.Courses.GetCourseModules
{
    public record GetCourseModulesQuery(Guid CourseId) : IQuery;

    public class GetCourseModulesQueryHandler : IQueryHandler<IReadOnlyList<ModuleDto>, GetCourseModulesQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetCourseModulesQueryHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<IReadOnlyList<ModuleDto>>> Handle(GetCourseModulesQuery query, CancellationToken cancellationToken = default)
        {
            var modulesQuery = _readDbContext.Modules
                .Where(m => m.CourseId == query.CourseId)
                .OrderBy(m => m.Position);

            var modules = await modulesQuery.ToListAsync(cancellationToken);

            foreach(var module in modules)
            {
                module.Lessons = module.Lessons.OrderBy(l => l.Position).ToList();
            }

            return modules;
        }
    }
}
