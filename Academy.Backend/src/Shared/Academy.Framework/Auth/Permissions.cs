using System.Runtime.InteropServices;

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
        }

        public static class Authorings 
        {
            public const string CreateAuthoring = "authorings.create";
            public const string SubmitAuthoring = "authorings.submit";
            public const string ApproveAuthoring = "authorings.approve";
            public const string RejectAuthoring = "authorings.reject";
        }
    }

}
