using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Domain
{
    public class Lesson : Entity<LessonId>
    {
        public Title Title { get; private set; }
        public Content? Content { get; private set; }
        public LessonType LessonType { get; private set; }
        public Position Position { get; private set; }
        public IReadOnlyList<Question> Questions { get; private set; } = new List<Question>();
        public IReadOnlyList<Attachment> Attachments { get; private set; } = new List<Attachment>();
        public PracticeLessonData PracticeLessonData { get; private set; }

        //ef core
        private Lesson(){}
        public Lesson(
            LessonId id,
            Title title, 
            LessonType lessonType)
        {
            Id = id;
            Title = title;
            LessonType = lessonType;
        }

        public UnitResult<Error> AddTest(IEnumerable<Question> questions)
        {
            if (LessonType != LessonType.Test)
                return Errors.Lesson.CannotAddTestToNonTestLesson();

            Questions = questions.ToList();

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> AddAttachments(IEnumerable<Attachment> attachments)
        {
            Attachments = Attachments.Concat(attachments).ToList();
            
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> SetContent(Content content)
        {
            Content = content;
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> AddPracticeLessonData(PracticeLessonData practiceLessonData)
        {
            if (LessonType != LessonType.Practice)
                return Errors.Lesson.CannotAddPracticeToNonPracticeLesson();

            PracticeLessonData = practiceLessonData;

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
    }
}
