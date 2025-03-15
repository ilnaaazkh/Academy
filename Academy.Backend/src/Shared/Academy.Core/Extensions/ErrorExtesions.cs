using Academy.SharedKernel;

namespace Academy.Core.Extensions
{
    public static class ErrorExtesions
    {
        public static ErrorList ToErrorList(this Error error)
        {
            return new ErrorList([error]);
        }
    }
}
