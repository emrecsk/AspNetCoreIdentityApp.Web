using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModel
{
    public class PasswordChangeViewModel
    {
        [Required(ErrorMessage = "Type old password")]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string PasswordOld { get; set; } = null!;


        [Required(ErrorMessage = "Type new password")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string PasswordNew { get; set; } = null!;

        [Required(ErrorMessage = "Confirm new password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("PasswordNew", ErrorMessage = "Passwords do not match")]
        public string PasswordNewConfirm { get; set; } = null!;
    }
}
