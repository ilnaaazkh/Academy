using CSharpFunctionalExtensions;
using System.Runtime.InteropServices.Marshalling;

namespace Academy.SharedKernel.ValueObjects
{
    public class SocialLink : ValueObject
    {
        public const int MAX_LENGTH = 200;
        public string Link { get; }
        public string Platform { get; set; }

        private SocialLink(string link, string platform)
        {
            Link = link;
            Platform = platform;
        }

        public static Result<SocialLink, Error> Create(string link, string platfrom)
        {
            if (string.IsNullOrEmpty(link) || link.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid(nameof(link));
            }

            if (string.IsNullOrEmpty(platfrom) || platfrom.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid(nameof(platfrom));
            }

            return new SocialLink(link, platfrom);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Link;
            yield return Platform;
        }
    }
}
