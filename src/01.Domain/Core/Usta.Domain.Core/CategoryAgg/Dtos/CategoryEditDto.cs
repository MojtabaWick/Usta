using Microsoft.AspNetCore.Http;

namespace Usta.Domain.Core.CategoryAgg.Dtos
{
    public class CategoryEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ImagedUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}