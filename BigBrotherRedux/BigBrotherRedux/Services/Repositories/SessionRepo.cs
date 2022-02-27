using BigBrotherRedux.Entities;
using BigBrotherRedux.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBrotherRedux.Services.Repositories;

public class SessionRepo : ISessionRepo
{


    private readonly BigBrotherReduxContext _db;

    /// <summary>
    /// Parameterized constructor for the InteractionRepo class.
    /// </summary>
    /// <param name="db"></param>
    public SessionRepo(BigBrotherReduxContext db)
    {
        _db = db;
    }

    /// Reads all entries from the Session Table and reurns them in a list.
    public ICollection<Session> ReadAll()
    {
        return _db.Session.AsNoTracking().ToList();
    }

    /// <summary>
    /// Creates a new entry session in the database
    /// </summary>
    /// <param name="session">the session entry we are updating</param>
    public void CreateEntry(Session session)
    {
        _db.Session.Add(session);

        _db.SaveChanges();
    }

    /// <summary>
    /// Deletes a session in the database
    /// </summary>
    /// <param name="sessionID">Deletes a session ID</param>
    public void DeleteSessionRef(Session session)
    {
        _db.Session.Remove(session);
        _db.SaveChanges();
    }

    /// <summary>
    /// Gets an entry in the database from an ID
    /// </summary>
    /// <param name="sessionID">the id of a particular session on the pet store</param>
    /// <returns></returns>
    public Session GetEntry(int sessionID)
    {
        return _db.Session.AsNoTracking()
            .FirstOrDefault(i => i.SessionId == sessionID);
    }

    /// <summary>
    /// Updates an entry in the session controller
    /// </summary>
    /// <param name="session"></param>
    public void UpdateEntry(Session session)
    {
        var sessionEntry = GetEntry(session.SessionId);
        sessionEntry.DateTime = session.DateTime;
        sessionEntry.LoggedIn = session.LoggedIn;
        sessionEntry.PurchaseMade = session.PurchaseMade;
        _db.SaveChanges();
    }
}

