using BBDisplay.Models;

namespace BBDisplay.Classes
{
    /// <summary>
    /// Class that cleans the User Interactions data
    /// in order to remove the JSON formatting.
    /// </summary>
    public class UserInteractionsClean : IPDataClean
    {
        /// <summary>
        /// Prepares the specified User Interactions data.
        /// </summary>
        /// <param name="userInteractionsData">User Interactions data to prepare.</param>
        /// <returns>Prepared User Interactions data.</returns>
        public List<UserInteraction> IndexPrepUserInteractionsData(List<string> userInteractionsData)
        {
            int currentEntry = 0;
            int totalEntries = (userInteractionsData.Count / 5);
            List<UserInteraction> preparedData = new List<UserInteraction>(totalEntries);

            for (int i = 0; i < totalEntries; i++) // While the current index is less than the count of preparedData
            {
                preparedData.Add(new UserInteraction()); // Populate preparedData with a new UserInteraction at the current index
            }

            while (currentEntry < totalEntries) // While the value of currentEntry is less than the value of totalEntries
            {
                // Populate the current entry with data from userInteractionsData
                preparedData[currentEntry].UserInteractionID = Int32.Parse(userInteractionsData[(currentEntry * 5)]);
                preparedData[currentEntry].UserSessionID = Int32.Parse(userInteractionsData[((currentEntry * 5) + 1)]);
                preparedData[currentEntry].DateTime = userInteractionsData[((currentEntry * 5) + 2)];
                preparedData[currentEntry].CurrentPageID = Int32.Parse(userInteractionsData[((currentEntry * 5) + 3)]);
                preparedData[currentEntry].InteractionLength = userInteractionsData[((currentEntry * 5) + 4)];

                currentEntry++; // Increment the value of currentEntry
            }

            return preparedData; // Return the prepared User Interactions data
        }
    }
}