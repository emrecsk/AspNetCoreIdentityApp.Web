using AspNetCoreIdentityApp.Web.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Models
{
    public class User : IdentityUser
    {
        public string? City { get; set; }
        public string? Photo { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender? Gender { get; set; }
    }
}
