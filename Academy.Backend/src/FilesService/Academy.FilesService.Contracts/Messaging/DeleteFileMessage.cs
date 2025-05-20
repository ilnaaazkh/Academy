namespace Academy.FilesService.Contracts.Messaging
{
    public record DeleteFileMessage(string Bucket, string fileUrl);
}
