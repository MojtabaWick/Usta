using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Usta.Domain.AppService.CategoryAgg;
using Usta.Domain.AppService.CityAgg;
using Usta.Domain.AppService.CommentAgg;
using Usta.Domain.AppService.OfferAgg;
using Usta.Domain.AppService.OrderAgg;
using Usta.Domain.AppService.ProvidedServiceAgg;
using Usta.Domain.AppService.UserAgg;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CommentAgg.Contracts;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Entities;
using Usta.Domain.Service.CategoryAgg;
using Usta.Domain.Service.CityAgg;
using Usta.Domain.Service.CommentAgg;
using Usta.Domain.Service.OfferAgg;
using Usta.Domain.Service.OrderAgg;
using Usta.Domain.Service.ProvidedServiceAgg;
using Usta.Domain.Service.UserAgg;
using Usta.Framework;
using Usta.Infrastructure.Cache.Contracts;
using Usta.Infrastructure.Cache.InMemoryCache;
using Usta.Infrastructure.Dapper.Persistence.Contracts;
using Usta.Infrastructure.Dapper.Persistence.SqlServer;
using Usta.Infrastructure.EFCore.Persistence;
using Usta.Infrastructure.EFCore.Repositories.CategoryAgg;
using Usta.Infrastructure.EFCore.Repositories.CityAgg;
using Usta.Infrastructure.EFCore.Repositories.CommentAgg;
using Usta.Infrastructure.EFCore.Repositories.OfferAgg;
using Usta.Infrastructure.EFCore.Repositories.OrderAgg;
using Usta.Infrastructure.EFCore.Repositories.ProvidedServiceAgg;
using Usta.Infrastructure.FileService.Contracts;
using Usta.Infrastructure.FileService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region RegisterService

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.RequireUniqueEmail = false;
    options.User.AllowedUserNameCharacters = "0123456789";
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddErrorDescriber<PersianIdentityErrorDescriber>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";          // وقتی لاگین نکرده
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied"; // وقتی نقش/پالیسی نداره

    options.ExpireTimeSpan = TimeSpan.FromDays(7); // ⭐ ماندگاری لاگین
    options.SlidingExpiration = true;               // تمدید خودکار
    options.Cookie.HttpOnly = true; //امنیت در برابر جاوا اسکریپت
    options.Cookie.IsEssential = true;// بدون رضایت قبول کردن کوکی میتوان کوکی ذخیره کرد
});

builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICityAppService, CityAppService>();

builder.Services.AddScoped<IProvidedServiceRepository, ProvidedServiceRepository>();
builder.Services.AddScoped<IProvidedServiceService, ProvidedServiceService>();
builder.Services.AddScoped<IProvidedServiceAppService, ProvidedServiceAppService>();

builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();

builder.Services.AddScoped<IOrderAppService, OrderAppService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderImagesRepository, OrderImagesRepository>();

builder.Services.AddScoped<IOfferAppService, OfferAppService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();

builder.Services.AddScoped<ICacheService, InMemoryCacheService>();

#endregion RegisterService

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();