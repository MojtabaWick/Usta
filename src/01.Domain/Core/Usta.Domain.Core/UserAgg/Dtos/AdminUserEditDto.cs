using System;
using System.Collections.Generic;
using System.Text;

namespace Usta.Domain.Core.UserAgg.Dtos
{
    public class AdminUserEditDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;
        public decimal WalletBalance { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int? CityId { get; set; }
        public string? Address { get; set; }

        public bool IsActive { get; set; }

        // مهم 👇
        public string Role { get; set; } = null!;

        // فقط اگر Expert باشد
        public List<int> ServiceIds { get; set; } = [];
    }
}