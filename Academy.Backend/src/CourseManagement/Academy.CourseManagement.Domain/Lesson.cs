﻿using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Domain
{
    public class Lesson : Entity<LessonId>
    {
        public Title Title { get; private set; }
        public Content Content { get; private set; }
        public LessonType LessonType { get; private set; }
        public Position Position { get; private set; }
        public IReadOnlyList<Question> Questions { get; private set; } = new List<Question>();
        public IReadOnlyList<Attachment> Attachments { get; private set; } = new List<Attachment>();
        public PracticeLessonData PracticeLessonData { get; private set; }

        public Lesson(Title title, Content content, LessonType lessonType)
        {
            Title = title;
            Content = content;
            LessonType = lessonType;
        }

    }
}
