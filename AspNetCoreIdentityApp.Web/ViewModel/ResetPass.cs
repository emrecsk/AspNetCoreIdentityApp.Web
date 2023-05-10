using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModel
{
    public class ResetPass
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "New Password :")]
        public string? Password { get; set; }


        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        [Required(ErrorMessage = "Password Confirm is required.")]
        [Display(Name = "New Password Confirm :")]
        public string? PasswordConfirm { get; set; }
    }
}
