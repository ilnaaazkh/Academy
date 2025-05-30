﻿using CSharpFunctionalExtensions;
using Newtonsoft.Json;


namespace Academy.SharedKernel.ValueObjects
{
    public class Attachment : ValueObject
    {
        public string FileName { get; }
        public string FileUrl { get; }

        [JsonConstructor]
        private Attachment(string fileName, string fileUrl)
        {
            FileName = fileName;
            FileUrl = fileUrl;
        }
        internal Attachment() { }

        public static Result<Attachment, Error> Create(string fileName, string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(fileUrl))
            {
                return Errors.General.ValueIsRequired("Attachment");
            }

            return new Attachment(fileName, fileUrl);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FileName;
            yield return FileUrl;
        }
    }
}
