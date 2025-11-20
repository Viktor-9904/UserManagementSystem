namespace UserManagementSystem.Repository.Interfaces
{
    public interface IRepository
    {
        Task DeleteAllUsersAsync();
        Task SaveChangesAsync();
    }
}
