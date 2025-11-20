using System.Text.Json;

using Microsoft.Data.SqlClient;

using UserManagementSystem.Repository.Interfaces;
using UserManagementSystem.Services.Interfaces;
using UserManagementSystem.ViewModels;

using Dapper;
using UserManagementSystem.Data.Models;

namespace UserManagementSystem.Services
{
    public class UserService : IUsersService
    {
        private readonly IRepository repository;
        private readonly IConfiguration config;

        public UserService(
            IRepository repository,
            IConfiguration config)
        {
            this.repository = repository;
            this.config = config;
        }

        public async Task<List<UserViewModel>> FetchUsersAsync(string url)
        {
            var obj = await JsonSerializer.DeserializeAsync<List<UserViewModel>>(
                await new HttpClient().GetStreamAsync(url),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return obj;
        }
        public async Task<bool> DeleteAllUsersAsync()
        {
            try
            {
                await this.repository.DeleteAllUsersAsync();
                await this.repository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> SaveAllUsersAsync(List<UserViewModel> users)
        {
            try
            {
                using var connection = 
                    new SqlConnection(this.config["ConnectionStrings:DefaultConnection"]!);

                await connection.OpenAsync();

                foreach (var user in users)
                {
                    user.CreatedAt = DateTime.UtcNow;

                    var userId = await connection.ExecuteScalarAsync<int>(@"
                        INSERT INTO Users (Name, Username, Password, Email, Phone, Website, Note, IsActive, CreatedAt)
                        VALUES (@Name, @Username, @Password, @Email, @Phone, @Website, @Note, @IsActive, @CreatedAt);
                        SELECT CAST(SCOPE_IDENTITY() as int);", user);

                    Address address = new Address
                    {
                        Street = user.Address.Street,
                        Suite = user.Address.Suite,
                        City = user.Address.City,
                        ZipCode = user.Address.Zipcode,
                        Lat = user.Address.Geo.Lat,
                        Lng = user.Address.Geo.Lng,
                        UserId = userId
                    };

                    await connection.ExecuteAsync(@"
                        INSERT INTO Addresses (Street, Suite, City, Zipcode, Lat, Lng, UserId)
                        VALUES (@Street, @Suite, @City, @Zipcode, @Lat, @Lng, @UserId);", address);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }            
        }
    }
}
