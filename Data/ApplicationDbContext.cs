using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityWeb.Models;
namespace IdentityWeb.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationUserRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }
    }
}