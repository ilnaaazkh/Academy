using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Application.Interfaces;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Academy.CourseManagement.Application.Courses.GetLesson
{
    public class GetLessonQueryHandler : IQueryHandler<LessonDto, GetLessonQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetLessonQueryHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<LessonDto?>> Handle(GetLessonQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _readDbContext.Lessons.FirstOrDefaultAsync(l => l.Id == query.LessonId);

            return result;
        }
    }
}
