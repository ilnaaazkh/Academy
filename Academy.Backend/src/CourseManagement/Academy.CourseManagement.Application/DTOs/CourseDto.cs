using Academy.CourseManagement.Domain;
using System;
namespace Academy.CourseManagement.Application.DTOs
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; } = default!;
        public string? Preview { get; set; } = string.Empty; 
    }
}
