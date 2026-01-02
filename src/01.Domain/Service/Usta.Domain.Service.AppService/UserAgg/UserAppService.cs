using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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

        public async Task<IdentityResult> ChangePasswordWithAdmin(int userId, string password)
        {
            return await userService.ChangePasswordWithAdmin(userId, password);
        }

        public async Task<IdentityResult> ChangePasswordWithUser(int userId, string oldPassword, string newPassword)
        {
            return await userService.ChangePasswordWithUser(userId, oldPassword, newPassword);
        }

        public async Task<PagedResult<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await userService.GetAllUsersAsync(pageNumber, pageSize, search, cancellationToken);
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<UserDto> GetExpertUserWithServicesAsync(int userId, CancellationToken cancellationToken)
        {
            var userDto = await userService.GetExpertUserWithServicesAsync(userId, cancellationToken);

            return userDto ?? throw new Exception($"user with id {userId} not found.");
        }

        public async Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var userDto = await userService.GetUserByIdAsync(userId, cancellationToken);

            return userDto ?? throw new Exception($"user with id {userId} not found.");
        }

        public async Task<UserEditInputDto> GetExpertUserWithServicesForEdit(int userId, CancellationToken cancellationToken)
        {
            var userDto = await userService.GetExpertUserWithServicesAsync(userId, cancellationToken);
            if (userDto is null)
            {
                throw new Exception($"user with id {userId} not found.");
            }

            var userEditDto = new UserEditInputDto
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address,
                ImageUrl = userDto.ImageUrl,
                CityId = userDto.CityId,
                ServiceIds = userDto.Services.Select(s => s.Id).ToList()
            };

            return userEditDto;
        }

        public async Task<UserEditInputDto> GetUserByIdForEdit(int userId, CancellationToken cancellationToken)
        {
            var userDto = await userService.GetUserByIdAsync(userId, cancellationToken);
            if (userDto is null)
            {
                throw new Exception($"user with id {userId} not found.");
            }
            var userEditDto = new UserEditInputDto
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address,
                ImageUrl = userDto.ImageUrl,
                CityId = userDto.CityId,
            };

            return userEditDto;
        }

        public async Task<Result<bool>> EditUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken)
        {
            var update = await userService.UpdateUserAsync(userId, userDto, cancellationToken);

            return update ? Result<bool>.Success("اطلاعات کاربر با موفقیت بروزرسانی شد.")
                : Result<bool>.Failure("بروزرسانی اطلاعات کاربر با مشکل مواجه شده است.");
        }

        public async Task<bool> UpdateExpertServices(int userId, List<int> newServiceIds, CancellationToken cancellationToken)
        {
            return await userService.UpdateExpertServices(userId, newServiceIds, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await userService.DeleteAsync(id, cancellationToken);
        }

        public async Task<AdminUserEditDto> GetUserForAdminEditAsync(int userId, CancellationToken cancellationToken)
        {
            return await userService.GetUserForAdminEditAsync(userId, cancellationToken);
        }

        public async Task<Result<bool>> AdminEditUserAsync(int userId, AdminUserEditDto input, CancellationToken cancellationToken)
        {
            return await userService.AdminEditUserAsync(userId, input, cancellationToken);
        }
    }
}