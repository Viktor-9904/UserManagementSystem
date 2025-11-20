using UserManagementSystem.ViewModels;

namespace UserManagementSystem.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<List<UserViewModel>> FetchUsersAsync(string url);
        public Task<bool> DeleteAllUsersAsync();
    }
}
