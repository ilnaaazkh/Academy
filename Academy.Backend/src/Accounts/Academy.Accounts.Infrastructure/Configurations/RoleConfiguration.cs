using Academy.Accounts.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.Accounts.Infrastructure.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(x => x.Id);

            builder.HasMany(r => r.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

            //сидирование ролей
        }
    }
}
