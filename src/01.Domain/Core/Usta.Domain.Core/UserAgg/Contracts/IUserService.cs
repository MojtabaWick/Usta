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
        Task<IdentityResult> RegisterUserAsync(UserRegisterInputDto userDto, CancellationToken cancellationToken);

        Task<SignInResult> LoginUserAsync(string userName, string password);

        Task<IdentityResult> ChangePasswordWithAdmin(int userId, string password);

        Task<IdentityResult> ChangePasswordWithUser(int userId, string oldPassword, string newPassword);

        Task<UserDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken);

        Task<PagedResult<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize, string? search,
           UserType? userType, CancellationToken cancellationToken);

        Task<bool> UpdateUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken);

        Task<UserDto?> GetExpertUserWithServicesAsync(int userId, CancellationToken cancellationToken);

        Task<bool> UpdateExpertServices(int userId, List<int> newServiceIds, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<AdminUserEditDto> GetUserForAdminEditAsync(int userId, CancellationToken cancellationToken);

        Task<Result<bool>> AdminEditUserAsync(int userId, AdminUserEditDto input, CancellationToken ct);

        Task<bool> CheckUserProfile(int customerId, CancellationToken cancellationToken);

        Task<bool> CheckUserWalletBalance(int customerId, decimal price, CancellationToken cancellationToken);

        Task DecreaseWallet(int customerId, decimal price, CancellationToken cancellationToken);

        Task IncreaseWallet(int expertId, decimal price, CancellationToken cancellationToken);

        Task<List<int>> GetExpertProvidedServicesIds(int expertId, CancellationToken cancellationToken);

        Task<ExpertProfileSummeryDto?> GetExpertSummeryProfile(int expertId, CancellationToken cancellationToken);
    }
}