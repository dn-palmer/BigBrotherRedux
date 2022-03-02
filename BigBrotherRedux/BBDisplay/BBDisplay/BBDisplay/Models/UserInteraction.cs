using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBDisplay.Models;

/// <summary>
/// Class which represents an Interaction entity.
/// </summary>
public class UserInteraction
{
    [Key]
    [DisplayName("User Interaction ID")]
    public int UserInteractionID { get; set; } // Public key for the UserInteraction entity
    [DisplayName("User Session ID")]
    public int UserSessionID { get; set; } // Foreign key containing the UserSessionID from a UserSession entity
    [DisplayName("Date & Time")]
    public string? DateTime { get; set; } // Date and time when a page reference is removed
    [DisplayName("Page ID")]
    public int CurrentPageID { get; set; } // Page ID for the interaction
    [DisplayName("Interaction Length")]
    public string? InteractionLength { get; set; } // Time in which an interaction lasted 

    /// <summary>
    /// Override for the default ToString() method.
    /// </summary>
    /// <returns>Formatted string that represents an UserInteraction entity.</returns>
    public override string ToString()
    {
        return ("ID: " + UserInteractionID.ToString() + "\nDate & Time: " + DateTime + "\nInteraction Length: " + InteractionLength.ToString()); // Return a formatted string that represents an Interaction entity
    }
}