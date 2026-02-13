using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CommentAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Domain.Core.UserAgg.Dtos
{
    public class ExpertProfileSummeryDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? CityName { get; set; }

        public List<ProfileProvidedServiceDto> Services { get; set; } = [];
        public List<CommentDto> Comments { get; set; } = [];
    }
}