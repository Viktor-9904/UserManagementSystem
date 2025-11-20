using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text.Json;
using UserManagementSystem.Data.Models;
using UserManagementSystem.Repository.Interfaces;
using UserManagementSystem.Services.Interfaces;
using UserManagementSystem.ViewModels;

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

        public async Task<List<UserViewModel>> FetchUsersAsync()
        {
            using var connection =
                new SqlConnection(this.config["ConnectionStrings:DefaultConnection"]!);

            await connection.OpenAsync();

            IEnumerable<User> users = await connection.QueryAsync<User>(
                @"SELECT * FROM Users");

            IEnumerable<Address> addresses = await connection.QueryAsync<Address>(
                @"SELECT * FROM Addresses");

            if (!users.Any() || !addresses.Any())
            {
                return new List<UserViewModel>();
            }

            List<UserViewModel> userList = users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Password = u.Password,
                    Email = u.Email,
                    Phone = u.Phone,
                    Website = u.Website,
                    Note = u.Note,
                    IsActive = u.IsActive == 1,
                    CreatedAt = u.CreatedAt,
                    Address = new AddressViewModel
                    {
                        Street = addresses.FirstOrDefault(a => a.UserId == u.Id)?.Street ?? "Not Found",
                        Suite = addresses.FirstOrDefault(a => a.UserId == u.Id)?.Suite ?? "Not Found",
                        City = addresses.FirstOrDefault(a => a.UserId == u.Id)?.City ?? "Not Found",
                        Zipcode = addresses.FirstOrDefault(a => a.UserId == u.Id)?.ZipCode ?? "Not Found",
                        Geo = new GeoViewModel
                        {
                            Lat = addresses.FirstOrDefault(a => a.UserId == u.Id)?.Lat ?? 0,
                            Lng = addresses.FirstOrDefault(a => a.UserId == u.Id)?.Lng ?? 0,
                        }
                    }
                })
                .ToList();

            return userList;
        }

        public async Task<List<UserViewModel>> FetchUsersByUrlAsync(string url)
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
