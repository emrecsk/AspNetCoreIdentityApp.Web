namespace AspNetCoreIdentityApp.Web.Areas.Admin.Models
{
    public class RoleAssignmentViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsAssigned { get; set; }
    }
}
