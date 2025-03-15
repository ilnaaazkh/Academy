using Academy.CourseManagement.Domain;
using Academy.SharedKernel.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.CourseManagement.Infrastructure.Configurations.Write
{
    internal class AuthorshipConfiguration : IEntityTypeConfiguration<Authorship>
    {
        public void Configure(EntityTypeBuilder<Authorship> builder)
        {
            builder.ToTable("authorships");

            builder.HasKey(a => new { a.CourseId, a.AuthorId });

            builder.Property(a => a.CourseId)
                .HasConversion(courseId => courseId.Value, value => CourseId.Create(value));

            builder.Property(a => a.AuthorId)
                .HasConversion(authorId => authorId.Value, value => AuthorId.Create(value));
        }
    }
}
