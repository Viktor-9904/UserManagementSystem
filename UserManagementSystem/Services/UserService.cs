using System.Text.Json;

using UserManagementSystem.Services.Interfaces;
using UserManagementSystem.ViewModels;

namespace UserManagementSystem.Services
{
    public class UserService : IUsersService
    {
        public async Task<IEnumerable<UserViewModel>> FetchUsers(string url)
        {
            var obj = await JsonSerializer.DeserializeAsync<IEnumerable<UserViewModel>>(
                await new HttpClient().GetStreamAsync(url),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return obj;
        }
    }
}
