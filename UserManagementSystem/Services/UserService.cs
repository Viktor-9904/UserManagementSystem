using System.Text.Json;
using UserManagementSystem.Repository.Interfaces;
using UserManagementSystem.Services.Interfaces;
using UserManagementSystem.ViewModels;

namespace UserManagementSystem.Services
{
    public class UserService : IUsersService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
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
    }
}
