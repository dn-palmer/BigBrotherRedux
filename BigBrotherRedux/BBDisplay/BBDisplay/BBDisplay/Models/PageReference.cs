using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBDisplay.Models;
public class PageReference
{

    [Key]
    [DisplayName("Page IP")]
    public int PageId { get; set; }
    [DisplayName("Date Added")]
    public DateTime DateAdded { get; set; }
    [DisplayName("Page Description")]
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