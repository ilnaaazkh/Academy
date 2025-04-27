using Academy.CourseManagement.Domain;
using Academy.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.CourseManagement.Application.DTOs
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Position { get; set; }

        public string LessonType { get; set; } = default!;
        public IReadOnlyList<Question> Questions { get; private set; } = new List<Question>();
        public IReadOnlyList<Attachment> Attachments { get; private set; } = new List<Attachment>();
        public PracticeLessonData PracticeLessonData { get; private set; }
    }
}
