using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.CourseManagement.Infrastructure.Configurations.Read
{
    internal class CourseDtoConfiguration : IEntityTypeConfiguration<CourseDto>
    {
        public void Configure(EntityTypeBuilder<CourseDto> builder)
        {
            builder.ToTable(Tables.Courses);

            builder.HasKey(c => c.Id);
        }
    }
}
