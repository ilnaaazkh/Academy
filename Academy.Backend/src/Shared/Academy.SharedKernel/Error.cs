namespace Academy.SharedKernel
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public string? InvalidField { get; set; } = null;

        private Error(string code, string message, ErrorType type, string? invalidField = null)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidField = invalidField;
        }

        public static Error NotFound(string code, string message) => new Error(code, message, ErrorType.NotFound);
        public static Error Conflict(string code, string message) => new Error(code, message, ErrorType.Conflict);
        public static Error Failure(string code, string message) => new Error(code, message, ErrorType.NotFound);
        public static Error Validation(string code, string message, string? invalidField) => new Error(code, message, ErrorType.Validation, invalidField);
    }
}
