using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Domain
{
    public class Module : Entity<ModuleId>
    {
        private List<Lesson> lessons = new();
        public Title Title { get; private set; }
        public Description Description { get; set; }
        public Position Position { get; private set; } = null!;
        public IReadOnlyList<Lesson> Lessons => lessons.AsReadOnly();

        public Module(Title title, Description description)
        {
            Title = title;
            Description = description;
        }

        public UnitResult<Error> SetPosition(Position position)
        {
            Position = position;
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> MoveForward()
        {
            var moveResult = Position.MoveForward();

            if (moveResult.IsFailure)
            {
                return moveResult.Error;
            }

            Position = moveResult.Value;

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> MoveBack()
        {
            var moveResult = Position.MoveBack();

            if (moveResult.IsFailure)
            {
                return moveResult.Error;
            }

            Position = moveResult.Value;

            return UnitResult.Success<Error>();
        }
    }
}
