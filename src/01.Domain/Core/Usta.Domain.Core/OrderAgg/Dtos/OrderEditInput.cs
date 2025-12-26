using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Domain.Core.OrderAgg.Dtos
{
    public class OrderEditInput
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<IFormFile> ImagesFiles { get; set; } = [];
    }
}