using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModal
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {
            
        }
        public SignUpViewModel(string userName, string email, string phoneNumber, string password, string passwordConfirm)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }
        [Display(Name = "User Name :")]
        public string UserName { get; set; }
        [Display(Name = "E-Mail :")]
        public string Email { get; set; }
        [Display(Name = "Phone Number :")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Password :")]
        public string Password { get; set; }
        [Display(Name = "Password Confirm :")]
        public string PasswordConfirm { get; set; }
    }
}
