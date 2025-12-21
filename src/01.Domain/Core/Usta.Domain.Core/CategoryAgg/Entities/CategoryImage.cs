using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;

namespace Usta.Domain.Core.CategoryAgg.Entities
{
    public class CategoryImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}