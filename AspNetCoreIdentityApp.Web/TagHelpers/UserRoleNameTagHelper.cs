using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace AspNetCoreIdentityApp.Web.TagHelpers
{
    public class UserRoleNameTagHelper : TagHelper
    {
        private readonly UserManager<User> _userManager;

        public UserRoleNameTagHelper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string UserID { get; set; } = null!;
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserID);

            var user_roles = await _userManager.GetRolesAsync(user!);

            var stringBuilder = new StringBuilder();

            user_roles.ToList().ForEach(role =>
            {
                stringBuilder.Append($@"<span class='badge bg-secondary mx-1'>{role.ToLower()}</span>");
            });

            output.Content.SetHtmlContent(stringBuilder.ToString());
        }

    }
}
