using Microsoft.EntityFrameworkCore;
using Academy.CourseManagement.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Academy.SharedKernel.ValueObjects.Ids;
using Academy.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace Academy.CourseManagement.Infrastructure.Configurations.Write
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("authors");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(id => id.Value,
                    value => AuthorId.Create(value));

            builder.Property(a => a.Email)
                .HasConversion(email => email.Value,
                    value => Email.Create(value).Value);

            builder.Property(a => a.PhoneNumber)
                .HasConversion(phone => phone.Value,
                    value => PhoneNumber.Create(value).Value);

            builder.ComplexProperty(a => a.FullName, fb =>
            {
                fb.Property(f => f.FirstName)
                    .IsRequired()
                    .HasMaxLength(FullName.MAX_LENGTH)
                    .HasColumnName("first_name");

                fb.Property(f => f.LastName)
                    .IsRequired()
                    .HasMaxLength(FullName.MAX_LENGTH)
                    .HasColumnName("last_name");
            });

            builder.Property(a => a.Bio)
               .HasMaxLength(Description.MAX_LENGTH)
               .HasConversion(t => t.Value,
                   value => Description.Create(value).Value);

            builder.Property(l => l.SocialLinks)
               .HasConversion(
                   q => JsonSerializer.Serialize(q, JsonSerializerOptions.Default),
                   v => JsonSerializer.Deserialize<IReadOnlyList<SocialLink>>(v, JsonSerializerOptions.Default)!,
                   new ValueComparer<IReadOnlyList<SocialLink>>(
                       (c1, c2) => c1!.SequenceEqual(c2!),
                       c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                       c => c.ToList())
                   );

            builder.HasMany<Authorship>()
                .WithOne()
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
