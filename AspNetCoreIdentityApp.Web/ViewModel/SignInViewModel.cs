
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModel
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
            
        }
        public SignInViewModel(string email, string password) //I created this constructor because the properties below are string and nullable.
        {
            Email = email;
            Password = password;
        }
        [Required (ErrorMessage = "Email is required.")]
        [EmailAddress (ErrorMessage = "Please enter a valid email address.")]
        [Display (Name = "Email")]
        public string Email { get; set; }


        [Required (ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display (Name = "Password")]
        public string Password { get; set; }


        [Display (Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
