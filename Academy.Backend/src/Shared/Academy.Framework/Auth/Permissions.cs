namespace Academy.Framework.Auth
{
    public static class Permissions
    {
        public static class Courses
        {
            public const string Create = "courses.create";
            public const string Update = "courses.update";
            public const string Read = "courses.read";
            public const string Delete = "courses.delete";
            public const string Publish = "courses.publish";
        }

        public static class Authorings 
        {
            public const string CreateAuthoring = "authorings.create";
            public const string SubmitAuthoring = "authorings.submit";
            public const string ApproveAuthoring = "authorings.approve";
            public const string RejectAuthoring = "authorings.reject";
        }

        public static class Authors 
        {
            public const string Create = "authors.create";
            public const string Delete = "authors.delete";
            public const string Read = "authors.read";
        }
    }

}
