using BigBrotherRedux.Entities;
namespace BigBrotherRedux.Services.Interfaces;

/// <summary>
/// Interface that contains various abstract methods that define
/// various actions to perform on the UserInteraction table within the
/// database.
/// </summary>
public interface IUserInteractionRepo
{
    /// <summary>
    /// Creates an Interaction entry within the database.
    /// </summary>
    /// <param name="interactionToCreate">Interaction to create an entry for.</param>
    public void CreateEntry(UserInteraction interactionToCreate);

    /// <summary>
    /// Updates an existing Interaction entry within the database.
    /// </summary>
    /// <param name="interactionToUpdate">Interaction entry to update.</param>
    public void UpdateEntry(UserInteraction interactionToUpdate);

    /// <summary>
    /// Gets an existing Interaction within the database.
    /// </summary>
    /// <param name="interactionID">ID of the Interaction to get.</param>
    /// <returns>Interaction within the database that matches the specified ID.</returns>
    public UserInteraction GetEntry(int interactionID);

    /// <summary>
    /// Deletes an existing Interaction within the database.
    /// </summary>
    /// <param name="interactionID">ID of the Interaction to get.</param>
    /// <returns>Interaction within the database that matches the specified ID.</returns>
    public void DeleteEntry(int interactionID);

    public ICollection<UserInteraction> ReadAll();
}

