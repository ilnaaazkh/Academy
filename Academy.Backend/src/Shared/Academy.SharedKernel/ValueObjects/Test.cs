namespace Academy.SharedKernel.ValueObjects
{
    public class Test
    {
        public List<int> Input { get; }
        public int Expected { get; }
        
        public Test(List<int> input, int expected)
        {
            Input = input;
            Expected = expected;
        }
    }
}