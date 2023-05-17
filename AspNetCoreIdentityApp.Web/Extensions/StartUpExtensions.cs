using AspNetCoreIdentityApp.Web.CustomValidations;
using AspNetCoreIdentityApp.Web.Localization;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Extensions
{
    public static class StartUpExtensions
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(1);
            });
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true; //Identity will check if the email address is used before.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.Password.RequiredLength = 6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //If the user enters the wrong password 3 times, the user will be locked out for 3 minutes.
                options.Lockout.MaxFailedAccessAttempts = 3; //If the user enters the wrong password 3 times, the user will be locked out.
            })
            .AddPasswordValidator<PasswordValidator>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserValidator<UserValidator>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>();
        }
    }
}
