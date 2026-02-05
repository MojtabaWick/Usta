using System.ComponentModel.DataAnnotations;

namespace Usta.Presentation.WebAPI.Models
{
    public class UserLoginInputModel
    {
        [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "وارد کردن رمز عبور الزامی است.")]
        public string Password { get; set; }
    }
}