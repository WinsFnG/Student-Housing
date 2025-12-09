using Microsoft.EntityFrameworkCore;
using Student_housing.Models;

namespace Student_housing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base (options)
    {
    }

        public DbSet<User> Users { get; set;  } = default!;
        //public DbSet<Admin> Landlord { get; set; } = default!;
    }
}
