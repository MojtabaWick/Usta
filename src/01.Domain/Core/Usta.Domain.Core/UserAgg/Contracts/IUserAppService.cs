using Microsoft.AspNetCore.Identity;
using Usta.Domain.Core._common;
using Usta.Domain.Core.UserAgg.Dtos;

namespace Usta.Domain.Core.UserAgg.Contracts
{
    public interface IUserAppService
    {
        public Task<IdentityResult> RegisterUserAsync(UserRegisterInputDto userDto, CancellationToken cancellationToken);

        public Task<IdentityResult> LoginUserAsync(string userName, string password, CancellationToken cancellationToken);

        public Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken);

        public Task<List<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken);

        public Task<Result<bool>> EditUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken);
    }
}