using Academy.CourseManagement.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.CourseManagement.Infrastructure.Configurations.Read
{

    public class ModuleDtoConfiguration : IEntityTypeConfiguration<ModuleDto>
    {
        public void Configure(EntityTypeBuilder<ModuleDto> builder)
        {
            builder.ToTable(Tables.Modules);

            builder.HasKey(m => m.Id);

            builder
                .HasMany(m => m.Lessons)
                .WithOne()
                .HasForeignKey("module_id");

            builder.Navigation(m => m.Lessons).AutoInclude();
        }
    }
}
