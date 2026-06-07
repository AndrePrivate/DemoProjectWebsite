using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DemoProjectWebsite.Models;

namespace DemoProjectWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<MoreInfoModel> MoreInfos { get; set; }
    }
}
