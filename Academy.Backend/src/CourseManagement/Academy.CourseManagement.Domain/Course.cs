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

        private List<Module> _modules = new();
        public IReadOnlyList<Module> Modules => _modules;

        public Course() { }            
        
        public Course(Title title, Description description)
        {
            Title = title;
            Description = description;
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
    }
}
