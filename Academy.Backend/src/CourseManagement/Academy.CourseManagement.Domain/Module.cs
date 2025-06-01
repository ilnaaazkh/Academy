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
        public Position Position { get; private set; } = null!;
        public IReadOnlyList<Lesson> Lessons => _lessons.AsReadOnly();

        public Module(
            ModuleId id,
            Title title)
        {
            Id = id;
            Title = title;
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

            var lessonsToMove = _lessons.Where(l => l.Position > lesson.Position);

            foreach(var lessonToMove in lessonsToMove)
            {
                var moveResult = lessonToMove.MoveBack();

                if (moveResult.IsFailure)
                    return moveResult.Error;
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


        public UnitResult<Error> AddTestToLesson(
            LessonId lessonId,
            IEnumerable<Question> questions)
        {
            var lessonResult = GetLessonById(lessonId);

            if (lessonResult.IsFailure)
                return lessonResult.Error;

            return lessonResult.Value.AddTest(questions);
        }

        public UnitResult<Error> AddAttachmentsToLesson(
            LessonId lessonId, 
            IEnumerable<Attachment> attachments)
        {
            var lessonResult = GetLessonById(lessonId);

            if (lessonResult.IsFailure)
                return lessonResult.Error;

            return lessonResult.Value.AddAttachments(attachments);
        }

        public UnitResult<Error> RemoveAttachmentsFromLesson(
            LessonId lessonId,
            string fileUrl)
        {
            var lessonResult = GetLessonById(lessonId);

            if (lessonResult.IsFailure)
                return lessonResult.Error;

            return lessonResult.Value.RemoveAttachment(fileUrl);
        }
   
        public UnitResult<Error> AddPracticeDataToLesson(
            LessonId lessonId, 
            PracticeLessonData practiceLessonData)
        {
            var lessonResult = GetLessonById(lessonId);

            if (lessonResult.IsFailure)
                return lessonResult.Error;

            return lessonResult.Value.AddPracticeLessonData(practiceLessonData);
        }

        public UnitResult<Error> SetLessonContent(
            LessonId lessonId,
            Content content)
        {
            var lessonResult = GetLessonById(lessonId);

            if (lessonResult.IsFailure)
                return lessonResult.Error;

            return lessonResult.Value.SetContent(content);
        }

        private Result<Lesson, Error> GetLessonById(LessonId id)
        {
            var lesson = _lessons.FirstOrDefault(l => l.Id == id);

            if (lesson == null)
            {
                return Errors.General.NotFound(id.Value);
            }

            return lesson;
        }
    }
}
