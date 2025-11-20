using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Data;
using UserManagementSystem.Data.Models;
using UserManagementSystem.Repository.Interfaces;

namespace UserManagementSystem.Repository
{
    public class Repository : IRepository
    {
        private readonly UserManagementSystemDbContext dbContext;

        public Repository(UserManagementSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteAllUsersAsync()
        {
            List<User> currentUsers = await this.dbContext.Users.ToListAsync();

            if (!currentUsers.Any())
            {
                return;
            }

            this.dbContext.RemoveRange(currentUsers);
        }

        public async Task SaveChangesAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
