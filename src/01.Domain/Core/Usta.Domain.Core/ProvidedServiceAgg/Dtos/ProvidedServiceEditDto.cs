using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Usta.Domain.Core.ProvidedServiceAgg.Dtos
{
    public class ProvidedServiceEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal MinPrice { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImaFile { get; set; }
        public int CategoryId { get; set; }
    }
}