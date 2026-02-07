using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Usta.Domain.Core.CommentAgg.Dtos
{
    public class CommentInputDto
    {
        [Required(ErrorMessage = "لطفاً نظر خود را بنویسید")]
        [MaxLength(500, ErrorMessage = "نظر نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد")]
        public string Text { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "امتیاز باید بین ۱ تا ۵ باشد")]
        public int Rating { get; set; }

        public int OrderId { get; set; }
    }
}