using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Usta.Domain.Core.OfferAgg.Dtos
{
    public class CreateOfferDto
    {
        [Required(ErrorMessage = "قیمت الزامی است.")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت باید مقدار مثبتی باشد.")]
        public decimal Price { get; set; }

        [StringLength(1000, ErrorMessage = "توضیحات نباید بیش از ۱۰۰۰ کاراکتر باشد.")]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? File { get; set; }

        [Required(ErrorMessage = "تاریخ و زمان شروع الزامی است.")]
        public string StartDateTime { get; set; }

        public int OrderId { get; set; }
        public int ExpertId { get; set; }
    }
}