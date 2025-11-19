using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UserManagementSystem.Data.Models;

using static UserManagementSystem.Common.EntityValidationConstants.Address;

namespace UserManagementSystem.Data.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(StreetMaxLength);

            builder
                .Property(a => a.Suite)
                .IsRequired()
                .HasMaxLength(SuiteMaxLength);

            builder
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(CityMaxLength);

            builder
                .Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(ZipcodeMaxLength);

            builder
                .Property(a => a.Lat)
                .IsRequired();

            builder
                .Property(a => a.Lng)
                .IsRequired();

            builder
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
