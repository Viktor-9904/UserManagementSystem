using UserManagementSystem.ViewModels;

namespace UserManagementSystem.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<List<UserViewModel>> FetchUsersAsync();
        public Task<List<UserViewModel>> FetchUsersByUrlAsync(string url);
        public Task<bool> DeleteAllUsersAsync();
        public Task<bool> SaveAllUsersAsync(List<UserViewModel> users);
    }
}
