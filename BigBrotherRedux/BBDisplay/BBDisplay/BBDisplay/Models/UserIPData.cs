using System.ComponentModel.DataAnnotations;

namespace BBDisplay.Models;

public class UserIPData
{
    [Key]
    public string UserIP { get; set; } = "";
    public string? CountryCode { get; set; }
    public string? CountryName { get; set; }
    public string? StateOrRegion { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public int VisitCount { get; set; }
    public string? DeviceType { get; set; }

}

