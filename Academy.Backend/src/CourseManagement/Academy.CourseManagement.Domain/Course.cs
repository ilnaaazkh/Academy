using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Domain
{
    public class Course : Entity<CourseId>
    {
        public Title Title { get; private set; }
        public Description Description { get; set; }
        public Status Status { get; private set; }
        private List<Module> _modules = new();
        public IReadOnlyList<Module> Modules => _modules;
        public string? Preview { get; set; }
        public Guid AuthorId { get; }

        public Course() { }            
        
        public Course(CourseId id, 
            Title title, 
            Description description, 
            Guid authorId)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = Status.Draft;
            AuthorId = authorId;
        }

        public UnitResult<Error> SetPreview(string fileName)
        {
            Preview = fileName;
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> AddModule(Module module)
        {
            var modulePosition = Position.Create(_modules.Count + 1).Value;
            module.SetPosition(modulePosition);

            _modules.Add(module);
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> RemoveModule(ModuleId moduleId)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId);

            if(module is null)
            {
                return Errors.General.NotFound(moduleId.Value);
            }

            var modulesToMove = _modules.Where(m => m.Position > module.Position);

            foreach(var moduleToMove in modulesToMove)
            {
                moduleToMove.MoveBack();
            }

            _modules.Remove(module);

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> AddLesson(ModuleId moduleId, Lesson lesson)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId);

            if(module is null)
            {
                return Errors.General.NotFound(moduleId.Value);
            }

            module.AddLesson(lesson);
            return UnitResult.Success<Error>();
        }
        
        public UnitResult<Error> RemoveLesson(ModuleId moduleId, LessonId lessonId)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId);
            
            if(module is null)
            {
                return Errors.General.NotFound(moduleId.Value);
            }

            var deletionResult = module.RemoveLesson(lessonId);
            return deletionResult;
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

        public Result<Module, Error> GetModuleById(ModuleId moduleId)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId);

            if(module == null)
            {
                return Errors.General.NotFound(moduleId.Value);
            }

            return module;
        }

        public UnitResult<Error> AddTestToLesson(
            ModuleId moduleId,
            LessonId lessonId,
            IEnumerable<Question> questions)
        {
            var moduleResult = GetModuleById(moduleId);

            if (moduleResult.IsFailure)
                return moduleResult.Error;

            return moduleResult.Value.AddTestToLesson(lessonId, questions);
        }

        public UnitResult<Error> AddAttachmentsToLesson(
            ModuleId moduleId, 
            LessonId lessonId,
            IEnumerable<Attachment> attachments)
        {
            var moduleResult = GetModuleById(moduleId);

            if(moduleResult.IsFailure)
                return moduleResult.Error;

            return moduleResult.Value.AddAttachmentsToLesson(lessonId, attachments);
        }

        public UnitResult<Error> AddPracticeDataToLesson(
            ModuleId moduleId,
            LessonId lessonId,
            PracticeLessonData practiceLessonData)
        {
            var moduleResult = GetModuleById(moduleId);

            if (moduleResult.IsFailure)
                return moduleResult.Error;

            return moduleResult.Value.AddPracticeDataToLesson(lessonId, practiceLessonData);
        }

        public UnitResult<Error> SetLessonContent(
            ModuleId moduleId,
            LessonId lessonId,
            Content content)
        {
            var moduleResult = GetModuleById(moduleId);

            if (moduleResult.IsFailure)
                return moduleResult.Error;

            return moduleResult.Value.SetLessonContent(lessonId, content);
        }
    }
}
