using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects.Ids
{
    public class ModuleId : ComparableValueObject
    {
        public Guid Value { get; set; }

        private ModuleId(Guid id)
        {
            Value = id;
        }

        public static ModuleId Create(Guid id) => new ModuleId(id);
        public static ModuleId NewModuleId() => new ModuleId(Guid.NewGuid());

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
