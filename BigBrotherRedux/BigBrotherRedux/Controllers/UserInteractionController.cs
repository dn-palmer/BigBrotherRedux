using BigBrotherRedux.Entities;
using BigBrotherRedux.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBrotherRedux.Controllers
{
    [Route("BigBrotherRedux/[controller]")]
    [ApiController]
    public class UserInteractionController : ControllerBase
    {
        private readonly IUserInteractionRepo _userInteractionRepo;

        /// <summary>
        /// Parameterized constructor for the InteractionController class.
        /// </summary>
        /// <param name="interactionsRepo"></param>
        public UserInteractionController(IUserInteractionRepo userInteractionRepo)
        {
            _userInteractionRepo = userInteractionRepo;

        }
        /// <summary>
        /// Method to handle a HTTP GET statement to get the ID of
        /// a specified interaction.
        /// </summary>
        /// <param name="interactionToGetID">ID of the Interaction to get.</param>
        /// <returns>Existing Interaction within the database.</returns>
        [HttpGet]
        [Route("ReadInteraction/{interactionToGetID}")]
        public string GetInteraction(int interactionToGetID)
        {
            return _userInteractionRepo.GetEntry(interactionToGetID).ToString(); // Get an existing Interaction within the database
        }

        /// Reads all entries in the UserIPData Table in a list format.
        /// <returns></returns>
        [HttpGet]
        [Route("ReadAll")]
        public List<UserInteraction> ReadAllInteractions()
        {


            var u = _userInteractionRepo.ReadAll();

            return u.ToList();


        }


        /// <summary>
        /// Method to handle a HTTP POST statement to create
        /// a new Interaction entry within the database.
        /// DP: Sorry to mess with your code but this wasnt doing anything so I added functionality 
        /// </summary>
        [HttpPost]
        [Route("PostInteraction/{date}/{lenthOfInteraction}/{sessionId}/{pageId:int}")]
        public void PostInteraction(string date, string lenthOfInteraction, int sessionId, int pageId)
        {
            UserInteraction interactionToPost = new UserInteraction();


            interactionToPost.DateTime =date.Replace("%2F", "/");
            interactionToPost.InteractionLength = lenthOfInteraction.Replace("%2F", "/");
            interactionToPost.UserSessionID = sessionId;
            interactionToPost.CurrentPageID = pageId;

            if (ModelState.IsValid)
            {
                _userInteractionRepo.CreateEntry(interactionToPost); // Create an Interaction entry within the database.
            }
        }

        /// <summary>
        /// Method to handle a HTTP PUT statement to update
        /// an existing Interaction entry within the database.
        /// DP: Fixed this one as well. The DataTime datatype was not working out. It kept
        /// sending numerous errors. It could be implemented with proper error checking but I swapped it
        /// to string to save myself time. 
        /// </summary>
        /// <param name="interactionID">ID of the Interaction to update.</param>
        /// <param name="dateTime">Updated date and time field.</param>
        /// <param name="interactionLength">Updated interaction length field.</param>
        [HttpPost]
        [Route("EditInteraction/{interactionID:int}/{dateTime}/{interactionLength}/{sessionId:int}/{pageId:int}")]
        public void PutInteraction(int interactionID, string dateTime, string interactionLength, int sessionId, int pageId)
        {
            UserInteraction interactionToUpdate = new UserInteraction();

            interactionToUpdate.UserInteractionID = interactionID;
            interactionToUpdate.DateTime = dateTime;
            interactionToUpdate.InteractionLength = interactionLength;
            interactionToUpdate.UserSessionID = sessionId;
            interactionToUpdate.CurrentPageID = pageId;

            _userInteractionRepo.UpdateEntry(interactionToUpdate); // Update an existing Interaction entry within the database
        }

        /// <summary>
        /// Method to handle a HTTP DELETE statement to delete
        /// an existing Interaction entry from the database.
        /// </summary>
        /// <param name="interactionToDeleteID">ID of the Interaction to delete</param>
        [HttpDelete]
        [Route("DeleteInteraction/{interactionToDeleteID:int}/")]
        public void DeleteInteraction(int interactionToDeleteID)
        {
            _userInteractionRepo.DeleteEntry(interactionToDeleteID); // Delete an existing Interaction from the database
        }


    }
}
