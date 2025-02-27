using Microsoft.EntityFrameworkCore;
using Academy.CourseManagement.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel.ValueObjects;

namespace Academy.CourseManagement.Infrastructure.Configurations.Write
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(id => id.Value,
                    value => CourseId.Create(value));

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(Title.MAX_LENGTH)
                .HasConversion(t => t.Value,
                    value => Title.Create(value).Value);

            builder.Property(c => c.Description)
               .IsRequired()
               .HasMaxLength(Description.MAX_LENGTH)
               .HasConversion(t => t.Value,
                   value => Description.Create(value).Value);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(Status.MAX_LENGTH)
                .HasConversion(id => id.Value, value => Status.Create(value).Value);

            builder.HasMany(c => c.Modules)
                .WithOne()
                .HasForeignKey("course_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Authorship>()
                .WithOne()
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
