using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentityApp.Web.Areas.Admin.Models;

namespace AspNetCoreIdentityApp.Web.Models
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<AspNetCoreIdentityApp.Web.Areas.Admin.Models.RoleUpdateViewModel> RoleUpdateViewModel { get; set; } = default!;
        public DbSet<AspNetCoreIdentityApp.Web.Areas.Admin.Models.RoleAssignmentViewModel> RoleAssignmentViewModel { get; set; } = default!;
    }
}
