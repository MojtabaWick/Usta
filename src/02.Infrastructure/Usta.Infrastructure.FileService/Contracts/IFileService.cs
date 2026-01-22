using Microsoft.AspNetCore.Http;

namespace Usta.Infrastructure.FileService.Contracts
{
    public interface IFileService
    {
        public Task<string> Upload(IFormFile file, string folder, CancellationToken cancellationToken);

        public Task<List<string>> UploadMany(List<IFormFile> files, string folder, CancellationToken cancellationToken);

        public Task DeleteByUrlAsync(string url, CancellationToken cancellationToken);
    }
}