using Academy.SharedKernel.ValueObjects.Ids;

namespace Academy.CourseManagement.Domain
{
    public class Authorship
    {
        public Authorship(CourseId courseId, AuthorId authorId)
        {
            CourseId = courseId;
            AuthorId = authorId;
        }

        public CourseId CourseId { get; private set; }
        public AuthorId AuthorId { get; private set; }
    }
}
