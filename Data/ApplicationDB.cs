using Microsoft.EntityFrameworkCore;
using Student_housing.Models;
using NexusThuisWeb.Models;

namespace Student_housing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Profiles> Profiles { get; set; }

        public DbSet<Cleaning_schedule> Cleaning_schedule { get; set; }

        public DbSet<Events> Events { get; set; }

        public DbSet<Garbage_schedule> Garbage_Schedule { get; set; }

        public DbSet<Report_messages> Report_Messages { get; set; }

        public DbSet<Reports> Reports { get; set; }

        public DbSet<Shared_chats> Shared_chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
