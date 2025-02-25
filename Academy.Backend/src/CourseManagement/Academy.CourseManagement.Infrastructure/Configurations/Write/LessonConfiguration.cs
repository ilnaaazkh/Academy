﻿using Academy.CourseManagement.Domain;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;


namespace Academy.CourseManagement.Infrastructure.Configurations.Write
{
    internal class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("lessons");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasConversion(id => id.Value,
                    value => LessonId.Create(value));

            builder.Property(l => l.Title)
                .HasMaxLength(Title.MAX_LENGTH)
                .IsRequired()
                .HasConversion(title => title.Value,
                    value => Title.Create(value).Value);

            builder.Property(l => l.Content)
                .HasConversion(content => content.Value,
                    value => Content.Create(value).Value);

            builder.Property(l => l.LessonType)
                .IsRequired()
                .HasConversion(type => type.Value,
                    value => LessonType.Create(value).Value);

            builder.Property(l => l.Position)
                .IsRequired()
                .HasConversion(position => position.Value,
                    value => Position.Create(value).Value);

            builder.Property(l => l.Questions)
                .HasConversion(
                    q => JsonSerializer.Serialize(q, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<IReadOnlyList<Question>>(v, JsonSerializerOptions.Default)!,
                    new ValueComparer<IReadOnlyList<Question>>(
                        (c1, c2) => c1!.SequenceEqual(c2!),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList())
                    );

            builder.Property(l => l.Attachments)
                .HasConversion(
                    a => JsonSerializer.Serialize(a, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<IReadOnlyList<Attachment>>(v, JsonSerializerOptions.Default)!,
                    new ValueComparer<IReadOnlyList<Attachment>>(
                        (c1, c2) => c1!.SequenceEqual(c2!),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList())
                    );

            builder.Property(l => l.PracticeLessonData)
                .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<PracticeLessonData>(v, JsonSerializerOptions.Default)!);
        }
    }
}
