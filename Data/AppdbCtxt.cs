using BookWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookWebApp.Data
{
    public class AppdbCtxt:IdentityDbContext<ApplicationUser>
    {
    }
}
