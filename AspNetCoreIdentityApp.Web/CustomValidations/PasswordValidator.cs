using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string? password)
        {
            if(password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "PasswordContainsUserName",
                    Description = "Password cannot contains Username."
                }));
            }
            else { return Task.FromResult(IdentityResult.Success);}
        }
    }
}
