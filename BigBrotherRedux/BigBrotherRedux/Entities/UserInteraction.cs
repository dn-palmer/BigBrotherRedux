using System.ComponentModel.DataAnnotations;

namespace BigBrotherRedux.Entities;

public class UserInteraction
{
    /// <summary>
    /// Class which represents an Interaction entity.
    /// </summary>
    [Key]
    public int UserInteractionID { get; set; } // Public key for the UserInteraction entity
    public int UserSessionID { get; set; } // Foreign key containing the UserSessionID from a UserSession entity
    public string? DateTime { get; set; } // Date and time when a page reference is removed
    public int CurrentPageID { get; set; } // Page ID for the interaction
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
