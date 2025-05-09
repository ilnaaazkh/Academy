﻿using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.AddModule
{
    public record AddModuleCommand(Guid CourseId, string Title, Guid UserId) : ICommand;
}
