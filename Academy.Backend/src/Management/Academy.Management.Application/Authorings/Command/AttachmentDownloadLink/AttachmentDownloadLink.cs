using Academy.Core.Abstractions;
using Academy.FilesService.Contracts;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;


namespace Academy.Management.Application.Authorings.Command.AttachmentDownloadLink
{
    public record GetAttachmentDownloadLinkCommand(string FileUrl) : ICommand;

    public class GetAttachmentDownloadLinkCommandHandler : ICommandHandler<string, GetAttachmentDownloadLinkCommand>
    {
        private readonly IFilesServiceContract _filesServiceContract;
        public GetAttachmentDownloadLinkCommandHandler(IFilesServiceContract filesServiceContract)
        {
            _filesServiceContract = filesServiceContract;
        }

        public Task<Result<string, ErrorList>> Handle(GetAttachmentDownloadLinkCommand command, CancellationToken cancellationToken = default)
        {
            return _filesServiceContract.GetDownloadLink(command.FileUrl, Constants.BUCKET, cancellationToken);
        }
    }
}
