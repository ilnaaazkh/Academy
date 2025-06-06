﻿using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.AddLesson
{
    public record AddLessonCommand(
        Guid CourseId,
        Guid ModuleId,
        string Title,
        string LessonType,
        Guid UserId) : ICommand;
}
