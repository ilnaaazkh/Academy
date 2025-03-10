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

        public static class Question
        {
            public static Error AtLeastTwoAnswersRequired(string questionTitle)
            {
                return Error.Validation(
                    "question.at.least.two.answers.required",
                    $"Question '{questionTitle}' must have at least two possible answers.",
                    null
                );
            }

            public static Error AtLeastOneCorrectAnswerRequired(string questionTitle)
            {
                return Error.Validation(
                    "question.at.least.one.correct.answer.required",
                    $"Question '{questionTitle}' must have at least one correct answer.",
                    null
                );
            }
        }

        public static class Author {
            public static Error AuthorNotExist(Guid? id)
            {
                return Error.Validation("author.not.exist", $"author with id = {id} doesn't exist", null);
            }
        }

    }
}
