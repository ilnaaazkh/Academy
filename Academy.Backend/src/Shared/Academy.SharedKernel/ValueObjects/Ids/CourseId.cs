using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.SharedKernel.ValueObjects.Ids
{
    public class CourseId : ComparableValueObject
    {
        public Guid Value { get; set; }

        private CourseId(Guid value)
        {
            Value = value;
        }

        public static CourseId Create(Guid id) => new CourseId(id);
        public static CourseId NewCourseId() => new CourseId(Guid.NewGuid());

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
