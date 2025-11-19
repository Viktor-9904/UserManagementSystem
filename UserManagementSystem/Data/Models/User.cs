namespace UserManagementSystem.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string? Note { get; set; }
        public byte IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Address> Addresses { get; set; }
            = new List<Address>();
    }
}
