using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Domain
{
    public class Module : Entity<ModuleId>
    {
        private List<Lesson> _lessons = new();
        public Title Title { get; private set; }
        public Description Description { get; set; }
        public Position Position { get; private set; } = null!;
        public IReadOnlyList<Lesson> Lessons => _lessons.AsReadOnly();

        public Module(
            ModuleId id,
            Title title, 
            Description description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public UnitResult<Error> AddLesson(Lesson lesson)
        {
            Position position = Position.Create(_lessons.Count + 1).Value;
            lesson.SetPosition(position);

            _lessons.Add(lesson);

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> RemoveLesson(LessonId lessonId)
        {
            var lesson = _lessons.FirstOrDefault(l => l.Id == lessonId);

            if(lesson is null)
            {
                return Errors.General.NotFound(lessonId.Value);
            }

            _lessons.Remove(lesson);
            return UnitResult.Success<Error>();
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

        public UnitResult<Error> UpdateTitle(Title title)
        {
            Title = title;

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> UpdateDescription(Description description)
        {
            Description = description;

            return UnitResult.Success<Error>();
        }
    }
}
