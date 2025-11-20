using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

using static UserManagementSystem.Common.EntityValidationConstants.Address;

namespace UserManagementSystem.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        [MaxLength(StreetMaxLength)]
        public string Street { get; set; } = null!;

        [Required]
        [MaxLength(SuiteMaxLength)]
        public string Suite { get; set; } = null!;

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(ZipcodeMaxLength)]
        public string Zipcode { get; set; } = null!;

        [Required]
        public GeoViewModel Geo { get; set; } = null!;

    }

    public class GeoViewModel
    {
        [Required]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double Lat { get; set; }

        [Required]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double Lng { get; set; }
    }
}
