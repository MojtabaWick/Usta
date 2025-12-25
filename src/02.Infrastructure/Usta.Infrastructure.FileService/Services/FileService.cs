using Microsoft.AspNetCore.Http;
using Usta.Infrastructure.FileService.Contracts;

namespace Usta.Infrastructure.FileService.Services
{
    public class FileService : IFileService
    {
        public async Task<string> Upload(IFormFile file, string folder, CancellationToken cancellationToken)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", folder);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return Path.Combine("/Images", folder, uniqueFileName);
        }

        public async Task DeleteByUrlAsync(string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            // تبدیل URL به مسیر محلی
            // فرض می‌کنیم URL مثل "/images/photo.jpg" است
            var relativePath = url.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath), cancellationToken);
            }
        }
    }
}