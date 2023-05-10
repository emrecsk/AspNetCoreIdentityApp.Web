namespace AspNetCoreIdentityApp.Web.ViewModel
{
    public class UserViewModel
    {
        public string? UserName { get; set; } //Can be null.
        public string? Email { get; set; } //Can be null.
        public string? PhoneNumber { get; set; } //Can be null.
        public string? PictureUrl { get; set; }
    }
}
