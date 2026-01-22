using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Usta.Domain.Core._common;
using Usta.Domain.Core.UserAgg.Dtos;
using Usta.Domain.Core.UserAgg.Entities;
using Usta.Domain.Core.UserAgg.Enums;

namespace Usta.Domain.Core.UserAgg.Contracts
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterUserAsync(UserRegisterInputDto userDto, CancellationToken cancellationToken);

        public Task<SignInResult> LoginUserAsync(string userName, string password);

        public Task<IdentityResult> ChangePasswordWithAdmin(int userId, string password);

        public Task<IdentityResult> ChangePasswordWithUser(int userId, string oldPassword, string newPassword);

        public Task<UserDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken);

        public Task<PagedResult<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize, string? search,
            UserType? userType, CancellationToken cancellationToken);

        public Task<bool> UpdateUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken);

        public Task<UserDto?> GetExpertUserWithServicesAsync(int userId, CancellationToken cancellationToken);

        public Task<bool> UpdateExpertServices(int userId, List<int> newServiceIds, CancellationToken cancellationToken);

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        public Task<AdminUserEditDto> GetUserForAdminEditAsync(int userId, CancellationToken cancellationToken);

        public Task<Result<bool>> AdminEditUserAsync(int userId, AdminUserEditDto input, CancellationToken ct);

        public Task<bool> CheckUserProfile(int customerId, CancellationToken cancellationToken);
    }
}