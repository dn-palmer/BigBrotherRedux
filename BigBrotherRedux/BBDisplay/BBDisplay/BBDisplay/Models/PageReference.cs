using System.ComponentModel.DataAnnotations;

namespace BBDisplay.Models;
public class PageReference
{

    [Key]
    public int PageId { get; set; }
    public DateTime DateAdded { get; set; }
    public string? PageDescription { get; set; }



    /// <summary>
    /// Override for the default ToString() method.
    /// </summary>
    /// <returns>Formatted string that represents an page reference entity.</returns>
    public override string ToString()
    {
        return ("ID: " + PageId.ToString() + DateAdded.ToString() + PageDescription); // Return a formatted string that represents an Interaction entity
    }

}