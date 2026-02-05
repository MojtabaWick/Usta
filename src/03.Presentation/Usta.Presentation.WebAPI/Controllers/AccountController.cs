using Microsoft.AspNetCore.Mvc;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;
using Usta.Presentation.WebAPI.Models;

namespace Usta.Presentation.WebAPI.Controllers
{
    // ReSharper disable once HollowTypeName

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAppService userAppService) : ControllerBase
    {
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] UserRegisterInputDto input, CancellationToken cancellationToken)
        {
            var result = await userAppService.RegisterUserAsync(input, cancellationToken);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("کاربر با موفقیت ثبت نام شد.");
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] UserLoginInputModel input)
        {
            var result = await userAppService.LoginUserAsync(input.Username, input.Password);
            if (!result.Succeeded)
            {
                return BadRequest("نام کاربری یا رمز عبور اشتباه است.");
            }
            return Ok(result);
        }
    }
}