using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project.Security.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<SecurityModels.Resource> Resources { get; set; }
        public DbSet<SecurityModels.Permission> Permissions { get; set; }
    }
}