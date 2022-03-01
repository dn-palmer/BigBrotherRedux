#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBDisplay.Models;
using BBDisplay.Classes;

namespace BBDisplay.Controllers
{
    public class UserIPDatasController : Controller
    {

        //Creating the client so that I can make calls to the API.
       private HttpClient client = new HttpClient();
        //Cleaner class. So that I can get ride of the Json formmating. JSon Serialzation would have worked bettter! 
        //Note to future me.
       private IPDataClean cleaner = new IPDataClean();

    // GET: UserIPDatas
    public async Task<IActionResult> Index()
        {           
            var data = await client.GetStringAsync("http://52.168.32.232/BigBrotherRedux/UserIPData/ReadAll");
            data = cleaner.RemoveSquareBraces(data);
            List<string> ipInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrep(ipInf);
            return View(model);
        }
        
        // GET: UserIPDatas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var data = await client.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/UserIPData/ReadUser/{id}");
            data = cleaner.RemoveSquareBraces(data);
            List<string> ipInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            //var model = cleaner.IndexPrep(ipInf);

            return Content(data);
        }

        // GET: UserIPDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserIPData ip)
        {

            return RedirectToAction("Index");
        }

        // GET: UserIPDatas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            return RedirectToAction("Index");
        }

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    _db.DeleteRestaurant(id);
        //    return RedirectToAction("Index");
        //}

    }
}
