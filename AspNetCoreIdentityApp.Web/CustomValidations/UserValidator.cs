using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class UserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            if(user.UserName!.ToLower().Contains("admin"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNameContainsAdmin",
                    Description = "Username cannot contain admin."
                }));
            }
            var IsDigit = int.TryParse(user.UserName[0].ToString(), out _);
            if (IsDigit)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError 
                { 
                    Code = "UserNameStartsWithDigit", 
                    Description = "Username cannot start with digit." 
                }));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
