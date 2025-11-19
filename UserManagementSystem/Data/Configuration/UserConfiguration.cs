using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UserManagementSystem.Data.Models;

using static UserManagementSystem.Common.EntityValidationConstants.User;

namespace UserManagementSystem.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(UsernameMaxLength);

            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(PasswordMaxLength);

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(EmailMaxLength);

            builder
                .Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(PhoneMaxLength);

            builder
                .Property(u => u.Website)
                .IsRequired();

            builder
                .Property(u => u.Note);

            builder
                .Property(u => u.IsActive)
                .IsRequired();

            builder
                .Property(u => u.CreatedAt)
                .IsRequired();
        }
    }
}
