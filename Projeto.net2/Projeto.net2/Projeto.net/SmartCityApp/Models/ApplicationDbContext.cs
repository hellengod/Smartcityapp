using Microsoft.EntityFrameworkCore;
using SmartCityApp.Models;

namespace SmartCityApp.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Defina os DbSets para suas entidades, por exemplo:
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
