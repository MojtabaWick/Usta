using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Usta.Domain.AppService.UserAgg;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Entities;
using Usta.Domain.Service.UserAgg;
using Usta.Framework;
using Usta.Infrastructure.EFCore.Persistence;
using Usta.Infrastructure.FileService.Contracts;
using Usta.Infrastructure.FileService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

#region Logging

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()

    //console log
    .WriteTo.Console()
    // Info ? Seq
    .WriteTo.Logger(lc => lc
        .WriteTo.Seq("http://localhost:5341"))

    .CreateBootstrapLogger();

builder.Host.UseSerilog();

#endregion Logging

#region RegisterService

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Usta;Trusted_Connection=True;"));

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
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";          // وقتی لاگین نکرده
    options.AccessDeniedPath = "/Account/AccessDenied"; // وقتی نقش/پالیسی نداره
});

builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();

#endregion RegisterService

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();