using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBDisplay.Models;

public class UserIPData
{
    [Key]
    [DisplayName("User IP")]
    public string UserIP { get; set; } = "";

    [DisplayName("Country Code")]
    public string? CountryCode { get; set; }

    [DisplayName("Country Name")]
    public string? CountryName { get; set; }

    [DisplayName("State or Region")]
    public string? StateOrRegion { get; set; }

    [DisplayName("City")]
    public string? City { get; set; }

    [DisplayName("Zip Code")]
    public string? ZipCode { get; set; }

    [DisplayName("Vist Count")]
    public int VisitCount { get; set; }

    [DisplayName("Device Type")]
    public string? DeviceType { get; set; }

}

