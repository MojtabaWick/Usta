using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Usta.Domain.Core._common;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Domain.AppService.UserAgg
{
    public class UserAppService(IUserService userService, SignInManager<ApplicationUser> signInManager) : IUserAppService
    {
        public async Task<IdentityResult> RegisterUserAsync(UserRegisterInputDto userDto, CancellationToken cancellationToken)
        {
            return await userService.RegisterUserAsync(userDto, cancellationToken);
        }

        public async Task<SignInResult> LoginUserAsync(string userName, string password)
        {
            return await userService.LoginUserAsync(userName, password);
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var userDto = await userService.GetUserByIdAsync(userId, cancellationToken);

            return userDto ?? throw new Exception($"user with id {userId} not found.");
        }

        public async Task<Result<bool>> EditUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken)
        {
            var update = await userService.UpdateUserAsync(userId, userDto, cancellationToken);

            return update ? Result<bool>.Success("اطلاعات کاربر با موفقیت بروزرسانی شد.")
                : Result<bool>.Failure("بروزرسانی اطلاعات کاربر با مشکل مواجه شده است.");
        }
    }
}