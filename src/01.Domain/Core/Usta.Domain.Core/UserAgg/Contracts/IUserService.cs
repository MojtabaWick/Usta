using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.UserAgg.Dtos;

namespace Usta.Domain.Core.UserAgg.Contracts
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterUserAsync(UserRegisterInputDto userDto, CancellationToken cancellationToken);

        public Task<SignInResult> LoginUserAsync(string userName, string password);

        public Task<UserDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken);

        public Task<List<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);

        public Task<bool> UpdateUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken);

        public Task<UserDto?> GetExpertUserWithServicesAsync(int userId, CancellationToken cancellationToken);
    }
}