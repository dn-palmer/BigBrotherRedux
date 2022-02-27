using BigBrotherRedux.Entities;
using BigBrotherRedux.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBrotherRedux.Services.Repositories;


public class UserInteractionRepo : IUserInteractionRepo
{
    private readonly BigBrotherReduxContext _db;

    /// <summary>
    /// Parameterized constructor for the InteractionRepo class.
    /// </summary>
    /// <param name="db"></param>
    public UserInteractionRepo(BigBrotherReduxContext db)
    {
        _db = db;
    }

    /// Reads all entries from the UserInteraction Table and reurns them in a list.
    public ICollection<UserInteraction> ReadAll()
    {
        return _db.UserInteraction.AsNoTracking().ToList();
    }


    /// <summary>
    /// Creates an Interaction entry within the database.
    /// </summary>
    /// <param name="interactionToCreate">Interaction to create an entry for.</param>
    public void CreateEntry(UserInteraction interactionToCreate)
    {
        _db.UserInteraction.Add(interactionToCreate); // Add the new entry into the database

        _db.SaveChanges(); // Save all changes made in the database
    }

    /// <summary>
    /// Updates an existing Interaction entry within the database.
    /// </summary>
    /// <param name="interactionToUpdate">Interaction entry to update.</param>
    public void UpdateEntry(UserInteraction interactionToUpdate)
    {
        var entryToUpdate = GetEntry(interactionToUpdate.UserInteractionID);

        entryToUpdate.DateTime = interactionToUpdate.DateTime;
        entryToUpdate.InteractionLength = interactionToUpdate.InteractionLength;
        _db.SaveChanges();
    }

    /// <summary>
    /// Gets an existing Interaction within the database.
    /// </summary>
    /// <param name="interactionID">ID of the Interaction to get.</param>
    /// <returns>Interaction within the database that matches the specified ID.</returns>
    public UserInteraction GetEntry(int interactionID)
    {
        return _db.UserInteraction.AsNoTracking().
            FirstOrDefault(i => i.UserInteractionID == interactionID); // Get an existing Interaction within the database where the InteractionID matches interactionID
    }

    /// <summary>
    /// Deletes an existing Interaction from the database.
    /// </summary>
    /// <param name="interactionID">ID of the Interaction to delete.</param>
    public void DeleteEntry(int interactionID)
    {
        var entryToDelete = GetEntry(interactionID); // Get the specified Interaction within the database.

        _db.UserInteraction.Remove(entryToDelete); // Delete the specified Interaction from the database
        _db.SaveChanges();
    }


}

