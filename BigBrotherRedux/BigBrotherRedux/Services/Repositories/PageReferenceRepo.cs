using BigBrotherRedux.Entities;
using BigBrotherRedux.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBrotherRedux.Services.Repositories;

public class PageReferenceRepo : IPageReferenceRepo
{

    private readonly BigBrotherReduxContext _db;

    /// <summary>
    /// Parameterized constructor for the InteractionRepo class.
    /// </summary>
    /// <param name="db"></param>
    public PageReferenceRepo(BigBrotherReduxContext db)
    {
        _db = db;
    }

    /// Reads all entries from the UserIPData Table and reurns them in a list.
    public ICollection<PageReference> ReadAll()
    {
        return _db.PageReference.AsNoTracking().ToList();
    }

    /// <summary>
    /// Creates a new entry in the page reference table
    /// </summary>
    /// <param name="pageReference">The object representing a page reference</param>
    public void CreateEntry(PageReference pageReference)
    {
        _db.PageReference.Add(pageReference);

        _db.SaveChanges();

    }


    /// <summary>
    /// Gets an entry in the database depending on the ID entered
    /// </summary>
    /// <param name="pageReferenceID">The id of the page reference</param>
    /// <returns></returns>
    public PageReference GetEntry(string pageReferenceID)
    {

        return _db.PageReference.AsNoTracking()
            .FirstOrDefault(i => i.PageId == Int32.Parse(pageReferenceID));

    }


    /// <summary>
    /// Updates an entry in the page reference table
    /// </summary>
    /// <param name="pageRef">The page reference which we are updating</param>
    public void UpdateEntry(PageReference pageRef)
    {
        var pageReferenceEntry = GetEntry(pageRef.PageId.ToString());
        pageReferenceEntry.DateAdded = pageRef.DateAdded;
        pageReferenceEntry.PageDescription = pageRef.PageDescription;
        _db.SaveChanges();

    }

    /// <summary>
    /// Deletes a page reference in the code 
    /// </summary>
    /// <param name="pageRefID">The page reference table</param>
    public void DeletePageRef(PageReference pageRefID)
    {
        var result = GetEntry(pageRefID.PageId.ToString());
        _db.PageReference.Remove(pageRefID);
        _db.SaveChanges();
    }

}


