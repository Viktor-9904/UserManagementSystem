using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserManagementSystem.Data.Models;

namespace UserManagementSystem.Data
{
    public class UserManagementSystemDbContext : DbContext
    {
        public UserManagementSystemDbContext(DbContextOptions<UserManagementSystemDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
