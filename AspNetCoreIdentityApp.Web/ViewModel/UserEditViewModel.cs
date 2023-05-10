using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModel
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "User Name is required.")]
        [Display(Name = "User Name :")]
        public string UserName { get; set; } = null!;


        [Required(ErrorMessage = "E-Mail is required.")]
        [EmailAddress(ErrorMessage = "E-Mail is not valid.")]
        [Display(Name = "E-Mail :")]
        public string Email { get; set; } = null!;


        [Display(Name = "Phone Number :")]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Birth Date :")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "City :")]
        public string? City { get; set; }

        [Display(Name ="Gender :")]
        public Gender? Gender { get; set; }

        [Display(Name = "Photo :")]
        public IFormFile? Photo { get; set; }
    }
}
