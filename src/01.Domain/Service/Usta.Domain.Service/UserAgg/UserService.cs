using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
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

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IFileService fileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _fileService = fileService;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegisterInputDto userDto, CancellationToken cancellationToken)
        {
            ApplicationUser user = userDto.Role switch
            {
                RegisterRole.Customer => new Customer
                {
                    UserName = userDto.Email,
                    Email = userDto.Email,
                },

                RegisterRole.Expert => new Expert
                {
                    UserName = userDto.Email,
                    Email = userDto.Email,
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
            return await _signInManager.PasswordSignInAsync(userName, password, false, false);
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

        public async Task<List<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
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

            return await query
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
        }

        public async Task<bool> UpdateUserAsync(int userId, UserEditInputDto userDto, CancellationToken cancellationToken)
        {
            if (userDto.ImageFile is not null)
            {
                if (userDto.ImageUrl is not null)
                {
                    await _fileService.DeleteByUrlAsync(userDto.ImageUrl, cancellationToken);
                }

                userDto.ImageUrl = await _fileService.Upload(userDto.ImageFile, "UsersProfile", cancellationToken);
            }

            var affectedRows = await _userManager.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(setters => setters
                        .SetProperty(u => u.Email, userDto.Email)
                        .SetProperty(u => u.NormalizedEmail, userDto.Email.ToUpper())
                        .SetProperty(u => u.FirstName, userDto.FirstName)
                        .SetProperty(u => u.LastName, userDto.LastName)
                        .SetProperty(u => u.PhoneNumber, userDto.PhoneNumber)
                        .SetProperty(u => u.Address, userDto.Address)
                        .SetProperty(u => u.ImageUrl, userDto.ImageUrl)
                        .SetProperty(u => u.CityId, userDto.CityId),
                    cancellationToken
                );

            return affectedRows > 0;
        }
    }
}