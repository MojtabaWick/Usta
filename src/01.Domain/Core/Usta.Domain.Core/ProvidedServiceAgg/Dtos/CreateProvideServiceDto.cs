using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Usta.Domain.Core.ProvidedServiceAgg.Dtos
{
    public class CreateProvideServiceDto
    {
        [Required(ErrorMessage = "عنوان سرویس الزامی است")]
        [StringLength(150, ErrorMessage = "عنوان نمی‌تواند بیشتر از 150 کاراکتر باشد")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "توضیحات نمی‌تواند بیشتر از 1000 کاراکتر باشد")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "حداقل قیمت الزامی است")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت وارد شده معتبر نیست")]
        public decimal MinPrice { get; set; }

        public string? ImageUrl { get; set; }

        [Display(Name = "تصویر سرویس")]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است")]
        [Range(1, int.MaxValue, ErrorMessage = "دسته‌بندی معتبر نیست")]
        public int CategoryId { get; set; }
    }
}