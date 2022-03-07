#nullable disable
using Microsoft.AspNetCore.Mvc;
using BBDisplay.Models;
using BBDisplay.Classes;
using Microsoft.AspNetCore.Authorization;

namespace BBDisplay.Controllers
{
    public class UserInteractionsController : Controller
    {
        private HttpClient httpClient = new HttpClient(); // HttpClient used to communicate with the API
        private UserInteractionsClean dataCleaner = new UserInteractionsClean(); // Used to clean the incoming data from the API

        /// <summary>
        /// Returns all User Interactions from the database.
        /// </summary>
        /// <returns>ViewResult object based on the constructed model from the cleaned data returned by the API.</returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var dataFromAPI = await httpClient.GetStringAsync("http://52.168.32.232/BigBrotherRedux/UserInteraction/ReadAll"); // Read all of the User Interaction entries from the database

            dataFromAPI = dataCleaner.RemoveSquareBraces(dataFromAPI); // Remove the square brackets from the data returned by the API

            List<string> userInteractionsPrepped = dataCleaner.PreppedData(dataCleaner.CleanAPIResponse(dataFromAPI)); // Place the data returned from the API into a string for processing into the database

            var model = dataCleaner.IndexPrepUserInteractionsData(userInteractionsPrepped); // Construct a model from the cleaned data returned by the API

            return View(model); // Create a ViewResult object based on the constructed model from the cleaned data returned by the API
        }

        /// <summary>
        /// Returns the specified User Interactions from the database
        /// </summary>
        /// <param name="id">ID of the User Interaction to return.</param>
        /// <returns>iewResult object based on the constructed model from the cleaned data returned by the API.</returns>
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var dataFromAPI = await httpClient.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/UserInteraction/ReadInteraction/{id}"); // Read the specified User Interaction entry from the database

            dataFromAPI = dataCleaner.RemoveSquareBraces(dataFromAPI); // Remove the square brackets from the data returned by the API

            List<string> userInteractionsPrepped = dataCleaner.PreppedData(dataCleaner.CleanAPIResponse(dataFromAPI)); // Place the data returned from the API into a string for processing into the database

            var model = dataCleaner.IndexPrepUserInteractionsData(userInteractionsPrepped); // Construct a model from the cleaned data returned by the API

            return View(model[0]); // Create a ViewResult object based on the constructed model from the cleaned data returned by the API
        }

        // GET: UserInteractions/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a User Interaction within the database.
        /// </summary>
        /// <param name="userInteraction">User Interaction to create.</param>
        /// <returns>Result of the task.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(UserInteraction userInteraction)
        {
            var dataFromAPI = await httpClient.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/UserInteraction/PostInteraction/{userInteraction.DateTime}/{userInteraction.InteractionLength}/{userInteraction.UserSessionID.ToString()}/{userInteraction.CurrentPageID.ToString()}"); // Create a User Interaction within the database

            return RedirectToAction("Index"); // Redirect to the Index page once the task has completed
        }

        /// <summary>
        /// Edits a specified User Interaction within the database.
        /// </summary>
        /// <param name="id">ID of the User Interaction to edit.</param>
        /// <returns>Result of the task.</returns>
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var dataFromAPI = await httpClient.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/UserInteraction/ReadInteraction/{id}"); // Read the specified User Interaction entry from the database

            dataFromAPI = dataCleaner.RemoveSquareBraces(dataFromAPI); // Remove the square brackets from the data returned by the API

            List<string> userInteractionsPrepped = dataCleaner.PreppedData(dataCleaner.CleanAPIResponse(dataFromAPI)); // Place the data returned from the API into a string for processing into the database

            var model = dataCleaner.IndexPrepUserInteractionsData(userInteractionsPrepped); // Construct a model from the cleaned data returned by the API

            return View(model[0]); // Create a ViewResult object based on the constructed model from the cleaned data returned by the API
        }

        /// <summary>
        /// Edits a specified User Interaction within the database.
        /// </summary>
        /// <param name="userInteraction">User Interaction to edit.</param>
        /// <returns>Result of the task.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditPost(int id, [Bind("UserInteractionID,UserSessionID,DateTime,CurrentPageID,InteractionLength")] UserInteraction userInteraction)
        {
            var dataFromAPI = await httpClient.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/UserInteraction/EditInteraction/{userInteraction.DateTime}/{userInteraction.InteractionLength}/{userInteraction.UserSessionID.ToString()}/{userInteraction.CurrentPageID.ToString()}"); // Edit the specified User Interaction within the database

            return RedirectToAction("Index"); // Redirect to the Index page once the task has completed
        }

        /// <summary>
        /// Deletes a specified User Interaction within the database.
        /// </summary>
        /// <param name="id">ID of the User Interaction to delete.</param>
        /// <returns>Result of the task.</returns>
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var dataFromAPI = await httpClient.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/UserInteraction/ReadInteraction/{id}"); // Read the specified User Interaction entry from the database

            dataFromAPI = dataCleaner.RemoveSquareBraces(dataFromAPI); // Remove the square brackets from the data returned by the API

            List<string> userInteractionsPrepped = dataCleaner.PreppedData(dataCleaner.CleanAPIResponse(dataFromAPI)); // Place the data returned from the API into a string for processing into the database

            var model = dataCleaner.IndexPrepUserInteractionsData(userInteractionsPrepped); // Construct a model from the cleaned data returned by the API

            return View(model[0]); // Create a ViewResult object based on the constructed model from the cleaned data returned by the API
        }

        /// <summary>
        /// Deletes a specified User Interaction within the database.
        /// </summary>
        /// <param name="userInteraction">User Interaction to delete.</param>
        /// <returns>Result of the task.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataFromAPi = await httpClient.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/UserInteraction/DeleteInteraction/{id}"); // Delete the specified User Interaction within the database

            return RedirectToAction("Index"); // Redirect to the Index page once the task has completed
        }
    }
}