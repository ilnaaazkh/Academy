namespace Academy.FilesService.Services.Models
{
    public class FileData
    {
        public FileData(string name, string bucket, Stream content)
        {
            Name = name;
            Bucket = bucket;
            Content = content;
        }

        public string Name { get; } = string.Empty;
        public string Bucket { get; } = string.Empty;
        public Stream Content { get; set; }
    }
}
