using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Full name fieled is required")]
       public string FullName { get; set; }
    }
}
