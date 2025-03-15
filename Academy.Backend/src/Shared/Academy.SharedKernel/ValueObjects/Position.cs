using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects
{
    public class Position : ComparableValueObject
    {
        public int Value { get; }

        private Position(int position)
        {
            Value = position;
        }

        public Result<Position, Error> MoveForward() => Create(Value + 1);
        public Result<Position, Error> MoveBack() => Create(Value - 1);

        public static Result<Position, Error> Create(int position)
        {
            if(position < 1)
            {
                return Errors.General.ValueIsInvalid(nameof(position));
            }

            return new Position(position);
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }

        public static bool operator >(Position left, Position right) 
        {
            return left.Value > right.Value;
        }

        public static bool operator <(Position left, Position right) 
        {
            return left.Value < right.Value;
        }
    }
}
