using System.ComponentModel.DataAnnotations;

namespace BookWebApp.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirm password does not match the password")]
        public string CheckPassword { get; set; }
    }
}
