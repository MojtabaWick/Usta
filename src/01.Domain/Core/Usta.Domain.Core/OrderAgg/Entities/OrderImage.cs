using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;

namespace Usta.Domain.Core.OrderAgg.Entities
{
    public class OrderImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}