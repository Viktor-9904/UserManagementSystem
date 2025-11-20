using UserManagementSystem.ViewModels;

namespace UserManagementSystem.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<IEnumerable<UserViewModel>> FetchUsers(string url);
    }
}
