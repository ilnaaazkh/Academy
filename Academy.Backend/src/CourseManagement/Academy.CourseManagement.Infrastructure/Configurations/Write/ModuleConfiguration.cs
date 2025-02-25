using Academy.CourseManagement.Domain;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.CourseManagement.Infrastructure.Configurations.Write
{
    internal class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("modules");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .IsRequired()
                .HasConversion(id => id.Value,
                    value => ModuleId.Create(value));

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(Title.MAX_LENGTH)
                .HasConversion(t => t.Value,
                    value => Title.Create(value).Value);

            builder.Property(m => m.Description)
               .IsRequired()
               .HasMaxLength(Description.MAX_LENGTH)
               .HasConversion(t => t.Value,
                   value => Description.Create(value).Value);

            builder.Property(m => m.Position)
                .IsRequired()
                .HasConversion(pos => pos.Value,
                value => Position.Create(value).Value);

            builder.HasMany(c => c.Lessons)
                .WithOne()
                .HasForeignKey("module_id");
        }
    }
}
