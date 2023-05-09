using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModel
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
        [Required(ErrorMessage = "User Name is required.")]
        [Display(Name = "User Name :")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "E-Mail is required.")]
        [Display(Name = "E-Mail :")]
        public string Email { get; set; }
        [Display(Name = "Phone Number :")]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password :")]
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage = "Passwords do not match!")]
        [Required(ErrorMessage = "Password Confirm is required.")]
        [Display(Name = "Password Confirm :")]
        public string PasswordConfirm { get; set; }
    }
}
