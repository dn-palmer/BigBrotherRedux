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
        //Cleaner Clase
       private IPDataClean cleaner = new IPDataClean();

    // GET: UserIPDatas
    public async Task<IActionResult> Index()
        {
            //Every 7 entries is a singel IP Table row
           
            var data = await client.GetStringAsync("http://52.168.32.232/BigBrotherRedux/UserIPData/ReadAll");
            data = cleaner.RemoveSquareBraces(data);
            List<string> ipInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrep(ipInf);


            foreach (string ip in ipInf) { 
                data += ip + "\n";
            }

            return View(model);
        }
        
        // GET: UserIPDatas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var data = await client.GetAsync("http://52.168.32.232/BigBrotherRedux/UserIPData/ReadAll");


            return View();
        }

        // GET: UserIPDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: UserIPDatas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            return RedirectToAction("Index");
        }

    }
}
