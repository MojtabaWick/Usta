using Microsoft.AspNetCore.Http;

namespace Usta.Domain.Core.UserAgg.Dtos
{
    public class UserEditInputDto
    {
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? CityId { get; set; }

        public List<int> ServiceIds { get; set; } = [];
    }
}