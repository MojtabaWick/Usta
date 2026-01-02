using Microsoft.AspNetCore.Identity;
using Usta.Domain.Core._common;
using Usta.Domain.Core.UserAgg.Dtos;

namespace Usta.Domain.Core.UserAgg.Contracts
{
    public interface IUserAppService
    {
        public Task<IdentityResult> RegisterUserAsync(UserRegisterInputDto userDto, CancellationToken cancellationToken);

        public Task<SignInResult> LoginUserAsync(string userName, string password);

        public Task<IdentityResult> ChangePasswordWithAdmin(int userId, string password);

        public Task<IdentityResult> ChangePasswordWithUser(int userId, string oldPassword, string newPassword);

        public Task<PagedResult<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);

        public Task Logout();

        public Task<UserDto> GetExpertUserWithServicesAsync(int userId, CancellationToken cancellationToken);

        public Task<UserEditInputDto> GetExpertUserWithServicesForEdit(int userId, CancellationToken cancellationToken);

        public Task<UserEditInputDto> GetUserByIdForEdit(int userId, CancellationToken cancellationToken);

        public Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken);

        public Task<Result<bool>> EditUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken);

        public Task<bool> UpdateExpertServices(int userId, List<int> newServiceIds, CancellationToken cancellationToken);

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        public Task<AdminUserEditDto> GetUserForAdminEditAsync(int userId, CancellationToken cancellationToken);

        public Task<Result<bool>> AdminEditUserAsync(int userId, AdminUserEditDto input, CancellationToken ct);
    }
}