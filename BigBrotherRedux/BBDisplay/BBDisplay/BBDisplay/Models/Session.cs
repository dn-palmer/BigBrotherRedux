using System.ComponentModel.DataAnnotations;

namespace BBDisplay.Models;

public class Session
{
    [Key]
    public int SessionId { get; set; }
    public string UserIPAddress { get; set; } = "";
    public DateTime DateTime { get; set; }
    public string LoggedIn { get; set; }
    public string PurchaseMade { get; set; }
}
