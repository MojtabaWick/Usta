using Microsoft.AspNetCore.Http;

namespace Usta.Infrastructure.FileService.Contracts
{
    public interface IFileService
    {
        public Task<string> Upload(IFormFile file, string folder, CancellationToken cancellationToken);

        public Task DeleteByUrlAsync(string url, CancellationToken cancellationToken);
    }
}