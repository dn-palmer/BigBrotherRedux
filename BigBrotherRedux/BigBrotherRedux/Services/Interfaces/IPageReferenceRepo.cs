using BigBrotherRedux.Entities;

namespace BigBrotherRedux.Services.Interfaces;

public interface IPageReferenceRepo
{
    public ICollection<PageReference> ReadAll(); //reads all entries in the Page Reference table.
    public void CreateEntry(PageReference pageReference);  //creates an entry in the page reference table
    public PageReference GetEntry(string pageReference);   //gets an entry in the page reference table
    public void UpdateEntry(PageReference pageRef);        //updates an entry in a update entry
    public void DeletePageRef(PageReference pageRefID);    //deletes an entry in the page entry
}

