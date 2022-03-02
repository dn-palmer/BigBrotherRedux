using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBDisplay.Models;

public class Session
{
    [Key]
    [DisplayName("Session ID")]
    public int SessionId { get; set; }
    [DisplayName("User IP")]
    public string UserIPAddress { get; set; } = "";
    [DisplayName("Session Date")]
    public String DateTime { get; set; }
    [DisplayName("User Logged IN")]
    public string LoggedIn { get; set; }
    [DisplayName("Purchase Made")]
    public string PurchaseMade { get; set; }
}
