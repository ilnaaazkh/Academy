using Academy.CourseManagement.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.CourseManagement.Infrastructure.Configurations.Read
{

    public partial class LessonDtoConfiguration
    {
        public class LessonInfoDtoConfiguration : IEntityTypeConfiguration<LessonInfoDto>
        {
            public void Configure(EntityTypeBuilder<LessonInfoDto> builder)
            {
                builder.ToTable(Tables.Lessons);
                builder.HasKey(x => x.Id);

                builder.Property(x => x.LessonType).HasColumnName("lesson_type");
                builder.Property(x => x.Position).HasColumnName("position");
                builder.Property(x => x.Title).HasColumnName("title");
            }
        }
    }
}
