using System;
using System.Collections.Generic;
using System.Text;

namespace Usta.Domain.Core.UserAgg.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public decimal WalletBalance { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? CityName { get; set; }
        public int? CityId { get; set; } = 0;
    }
}