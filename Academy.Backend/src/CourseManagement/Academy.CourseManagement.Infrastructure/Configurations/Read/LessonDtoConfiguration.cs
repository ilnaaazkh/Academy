using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Domain;
using Academy.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Academy.CourseManagement.Infrastructure.Configurations.Read
{
    public class LessonDtoConfiguration : IEntityTypeConfiguration<LessonDto>
    {
        public void Configure(EntityTypeBuilder<LessonDto> builder)
        {
            builder.ToTable(Tables.Lessons);

            builder.HasKey(x => x.Id);

            builder.Property(l => l.Questions)
              .HasConversion(
                  q => JsonConvert.SerializeObject(q),
                  v => JsonConvert.DeserializeObject<IReadOnlyList<Question>>(v)!,
                  new ValueComparer<IReadOnlyList<Question>>(
                      (c1, c2) => c1!.SequenceEqual(c2!),
                      c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                      c => c.ToList())
                  )
              .HasColumnType("jsonb");

            builder.Property(l => l.Attachments)
                .HasConversion(
                    a => JsonConvert.SerializeObject(a),
                    v => JsonConvert.DeserializeObject<IReadOnlyList<Attachment>>(v)!,
                    new ValueComparer<IReadOnlyList<Attachment>>(
                        (c1, c2) => c1!.SequenceEqual(c2!),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList())
                    )
                .HasColumnType("jsonb");

            builder.Property(l => l.PracticeLessonData)
                .IsRequired(false)
                .HasConversion(p => JsonConvert.SerializeObject(p),
                    v => JsonConvert.DeserializeObject<PracticeLessonData>(v)!)
                .HasColumnType("jsonb");
        }
    }
}
