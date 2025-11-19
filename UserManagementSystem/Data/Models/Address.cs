namespace UserManagementSystem.Data.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public string Suite { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public double Lat {  get; set; }
        public double Lng { get; set; }
        public User User { get; set; } = null!;
        public int UserId { get; set; }
    }
}
