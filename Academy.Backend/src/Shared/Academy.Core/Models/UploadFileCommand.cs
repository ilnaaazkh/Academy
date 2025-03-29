namespace Academy.Core.Models
{
    public record UploadFileCommand(Stream Content, string FileName);
}
