using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Usta.Domain.Core.UserAgg.Enums;

namespace Usta.Domain.Core.UserAgg.Dtos
{
    public class UserRegisterInputDto
    {
        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست")]
        [MaxLength(256, ErrorMessage = "ایمیل نمی‌تواند بیشتر از ۲۵۶ کاراکتر باشد")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [MinLength(8, ErrorMessage = "رمز عبور باید حداقل ۸ کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "انتخاب نوع حساب الزامی است")]
        public RegisterRole Role { get; set; }
    }
}