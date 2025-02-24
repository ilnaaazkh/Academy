namespace Academy.SharedKernel
{
    public static class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.invalid", $"{label} is invalid", name);
            }

            public static Error ValueIsRequired(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.required", $"{label} is required", name);
            }

            public static Error NotFound(Guid? id)
            {
                var forId = id == null ? "" : $" for id={id}";
                return Error.NotFound("record.not.found", $"record not found{forId}");
            }
        }
    }
}
