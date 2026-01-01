using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Usta.Domain.Core.CategoryAgg.Dtos
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "عنوان دسته بندی الزامی است")]
        [MaxLength(100, ErrorMessage = "عنوان دسته بندی نمی‌تواند بیشتر از 100 کاراکتر باشد")]
        public string Title { get; set; }

        public string? Description { get; set; }
        public string? ImagedUrl { get; set; }

        [Required(ErrorMessage = "عکس دسته بندی الزامی است")]
        public IFormFile ImagFile { get; set; }
    }
}