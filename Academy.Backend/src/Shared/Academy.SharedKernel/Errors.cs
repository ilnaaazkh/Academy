namespace Academy.SharedKernel
{
    public static class Errors
    {
        public static class User
        {
            public static Error InvalidCredentials()
            {
                return Error.Validation("user.invalid.credentials", "Invalid email or password");
            }
        }
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

            public static Error Failure()
            {
                return Error.Failure("failure", "Failure");
            }
        }

        public static class Tokens 
        {
            public static Error ExpiredToken()
            {
                return Error.Validation("token.is.expired", "Token is expired");
            }
            
            public static Error InvalidToken()
            {
                return Error.Validation("token.is.invalid", "Token is invalid");
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

        public static class Lesson 
        {
            public static Error CannotAddTestToNonTestLesson()
            {
                return Error.Validation(
                    "lesson.cannot.add.test",
                    "Cannot add a test to a lesson that is not of type 'TEST'.",
                    null
                );
            }

            public static Error CannotAddPracticeToNonPracticeLesson()
            {
                return Error.Validation(
                    "lesson.cannot.add.practice",
                    "Cannot add a practice to a lesson that is not of type 'PRACTICE'.",
                    null
                );
            }
        }
    }
}
