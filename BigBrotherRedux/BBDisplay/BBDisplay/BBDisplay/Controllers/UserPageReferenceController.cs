using BBDisplay.Classes;
using Microsoft.AspNetCore.Mvc;

namespace BBDisplay.Controllers
{
    public class UserPageReferenceController : Controller
    {
        //Creating the client so that I can make calls to the API.
        private HttpClient client = new HttpClient();

        private PageReferenceDataClean cleaner = new PageReferenceDataClean();

        public async Task<IActionResult> Index()
        {
            var data = await client.GetStringAsync("http://52.168.32.232/BigBrotherRedux/PageReference/ReadAll");
            data = cleaner.RemoveSquareBraces(data);
            List<string> prInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrep(prInf);
            return View(model);
        }
    }
}
