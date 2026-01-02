using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;
using Usta.Domain.Core.UserAgg.Entities;
using Usta.Domain.Core.UserAgg.Enums;
using Usta.Infrastructure.FileService.Contracts;

namespace Usta.Domain.Service.UserAgg
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFileService _fileService;
        private readonly IProvidedServiceService _providedServiceService;

        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IFileService fileService,
            IProvidedServiceService providedServiceService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _fileService = fileService;
            _providedServiceService = providedServiceService;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegisterInputDto userDto, CancellationToken cancellationToken)
        {
            ApplicationUser user = userDto.Role switch
            {
                RegisterRole.Customer => new Customer
                {
                    UserName = userDto.Email,
                    Email = userDto.Email,
                    WalletBalance = 10000
                },

                RegisterRole.Expert => new Expert
                {
                    UserName = userDto.Email,
                    Email = userDto.Email,
                    WalletBalance = 10000
                },

                _ => throw new Exception("Invalid user role")
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
                return result;

            switch (userDto.Role)
            {
                case RegisterRole.Customer:
                    await _userManager.AddToRoleAsync(user, "Customer");
                    break;

                case RegisterRole.Expert:
                    await _userManager.AddToRoleAsync(user, "Expert");
                    break;

                default:
                    throw new Exception("Can't add the user role.");
            }

            return IdentityResult.Success;
        }

        public async Task<SignInResult> LoginUserAsync(string userName, string password)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, true, false);
        }

        public async Task<IdentityResult> ChangePasswordWithAdmin(int userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                throw new Exception($"user with id:{userId} not found. ");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result;
        }

        public async Task<IdentityResult> ChangePasswordWithUser(int userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
            }
            return result;
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email!,
                    IsActive = u.IsActive,
                    WalletBalance = u.WalletBalance,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    ImageUrl = u.ImageUrl,
                    CityId = u.CityId,
                    CityName = u.City != null ? u.City.Name : null
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<UserDto?> GetExpertUserWithServicesAsync(int userId, CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .OfType<Expert>()
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email!,
                    IsActive = u.IsActive,
                    WalletBalance = u.WalletBalance,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    ImageUrl = u.ImageUrl,
                    CityId = u.CityId,
                    CityName = u.City != null ? u.City.Name : null,

                    Services = u.ProvidedServices
                        .Select(ps => new ProfileProvidedServiceDto()
                        {
                            Id = ps.Id,
                            Title = ps.Title
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = _userManager.Users
               .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u =>
                    (u.FirstName != null && u.FirstName.Contains(search)) ||
                    (u.LastName != null && u.LastName.Contains(search)) ||
                    (u.UserName != null && u.UserName.Contains(search)) ||
                    (u.PhoneNumber != null && u.PhoneNumber.Contains(search)) ||
                    (u.City != null && u.City.Name.Contains(search)));
            }

            var items = await query
           .OrderBy(u => u.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .Select(u => new UserDto
           {
               Id = u.Id,
               Email = u.Email!,
               IsActive = u.IsActive,
               WalletBalance = u.WalletBalance,
               FirstName = u.FirstName,
               LastName = u.LastName,
               PhoneNumber = u.PhoneNumber,
               Address = u.Address,
               ImageUrl = u.ImageUrl,
               CityName = u.City.Name
           })
           .ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            return new PagedResult<UserDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<bool> UpdateUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken)
        {
            if (userDto.ImageFile is not null)
            {
                if (!string.IsNullOrWhiteSpace(userDto.ImageUrl))
                {
                    await _fileService.DeleteByUrlAsync(userDto.ImageUrl, cancellationToken);
                }

                userDto.ImageUrl = await _fileService.Upload(
                    userDto.ImageFile, "UsersProfile", cancellationToken);
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return false;

            if (user.Email != userDto.Email)
            {
                await _userManager.SetEmailAsync(user, userDto.Email);
                await _userManager.SetUserNameAsync(user, userDto.Email);
            }

            if (user.PhoneNumber != userDto.PhoneNumber)
            {
                await _userManager.SetPhoneNumberAsync(user, userDto.PhoneNumber);
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Address = userDto.Address;
            user.CityId = userDto.CityId;
            user.ImageUrl = userDto.ImageUrl;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> UpdateExpertServices(int userId, List<int> newServiceIds, CancellationToken cancellationToken)
        {
            var expert = await _userManager.Users
                .OfType<Expert>()
                .Include(e => e.ProvidedServices)
                .FirstOrDefaultAsync(e => e.Id == userId, cancellationToken);

            if (expert is null)
                return false;

            newServiceIds ??= [];

            var services = await _providedServiceService.GetByListIdsAsync(newServiceIds, cancellationToken);

            expert.ProvidedServices.Clear();
            expert.ProvidedServices.AddRange(services);

            await _userManager.UpdateAsync(expert);
            return true;
        }

        public async Task<AdminUserEditDto> GetUserForAdminEditAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .Include(u => u.City)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user is null)
                throw new Exception($"user with id: {userId} not found.");

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var dto = new AdminUserEditDto
            {
                Id = user.Id,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CityId = user.CityId,
                Address = user.Address,
                IsActive = user.IsActive,
                Role = role ?? "User"
            };

            if (role == "Expert")
            {
                dto.ServiceIds = await GetServiceIdsByUserId(userId, cancellationToken);
            }

            return dto;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var affectedRow = await _userManager.Users.Where(u => u.Id == id)
                .ExecuteUpdateAsync(setter => setter
                        .SetProperty(u => u.IsDeleted, true)
                    , cancellationToken);

            return affectedRow > 0;
        }

        public async Task<Result<bool>> AdminEditUserAsync(int userId, AdminUserEditDto input, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Result<bool>.Failure("کاربر یافت نشد");

            if (user.Email != input.Email)
            {
                await _userManager.SetEmailAsync(user, input.Email);
                await _userManager.SetUserNameAsync(user, input.Email);
            }

            if (user.PhoneNumber != input.PhoneNumber)
            {
                await _userManager.SetPhoneNumberAsync(user, input.PhoneNumber);
            }

            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.CityId = input.CityId;
            user.Address = input.Address;
            user.IsActive = input.IsActive;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return Result<bool>.Failure("خطا در بروزرسانی کاربر");

            if (input.Role == "Expert")
            {
                await UpdateExpertServices(userId, input.ServiceIds, cancellationToken);
            }

            return Result<bool>.Success("کاربر با موفقیت بروزرسانی شد.");
        }

        private async Task<List<int>> GetServiceIdsByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .OfType<Expert>()
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.ProvidedServices.Select(ps => ps.Id))
                .ToListAsync(cancellationToken);
        }
    }
}