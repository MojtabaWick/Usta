using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Usta.Domain.Core.OrderAgg.Dtos
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "توضیحات الزامی است")]
        [MinLength(10, ErrorMessage = "توضیحات حداقل ۱۰ کاراکتر باشد")]
        public string Description { get; set; }

        [Required(ErrorMessage = "تاریخ شروع الزامی است")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "سرویس الزامی است")]
        public int ProvidedServiceId { get; set; }

        public List<IFormFile> Images { get; set; } = [];
    }
}