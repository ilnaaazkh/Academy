using Academy.CourseManagement.Application.Courses;
using Academy.CourseManagement.Domain;
using Academy.CourseManagement.Infrastructure.DbContexts;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Academy.CourseManagement.Infrastructure.Repositories
{
    internal class CoursesRepository : ICoursesRepository
    {
        private readonly CourseManagementWriteDbContext _dbContext;

        public CoursesRepository(CourseManagementWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Course course, CancellationToken cancellationToken = default)
        {
            await _dbContext.Courses.AddAsync(course, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return course.Id.Value;
        }

        public async Task<Result<Guid, Error>> Remove(Course course, CancellationToken cancellationToken = default)
        {
            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return course.Id.Value;
        }

        public async Task<Result<Course, Error>> GetById(CourseId id, CancellationToken cancellationToken = default)
        {
            var course = await _dbContext.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (course is null)
            {
                return Errors.General.NotFound(id.Value);
            }

            return course;
        }

        public async Task<Result<Guid, Error>> Save(Course course, CancellationToken cancellationToken = default)
        {
            _dbContext.Courses.Attach(course);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return course.Id.Value;
        }
    }

}
