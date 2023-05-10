using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModel
{
    public class PasswordResetModel
    {
        [Required(ErrorMessage = "E-Mail is required.")]
        [EmailAddress(ErrorMessage = "E-Mail is not valid.")]
        [Display(Name = "E-Mail :")]
        public string? Email { get; set; }
    }
}
