using Academy.Core.Extensions;
using Academy.SharedKernel.ValueObjects;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace Academy.CourseManagement.Application.Courses.AddPracticeData
{
    public class AddPracticeDataCommandValidator : AbstractValidator<AddPracticeDataCommand> 
    {
        public AddPracticeDataCommandValidator()
        {
            RuleFor(c => new { c.TemplateCode, c.Tests })
                .MustBeValueObject(c => 
                    PracticeLessonData.Create(
                        c.TemplateCode, 
                        c.Tests.Select(t => new Test(t.Input.ToList(), t.Expected))
                        )
                    );
        }
    }
}
