using System.ComponentModel.DataAnnotations;

using static UserManagementSystem.Common.EntityValidationConstants.User;

namespace UserManagementSystem.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(PasswordMaxLength)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        public string Phone { get; set; } = null!;

        [Required]
        public string Website { get; set; } = null!;

        public string? Note { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public AddressViewModel Address { get; set; } = null!;
    }
}
