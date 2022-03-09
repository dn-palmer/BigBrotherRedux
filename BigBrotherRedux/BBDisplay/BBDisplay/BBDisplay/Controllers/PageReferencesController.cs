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
using Microsoft.AspNetCore.Authorization;

namespace BBDisplay.Controllers
{
    public class PageReferencesController : Controller
    {
        //Creating the client so that I can make calls to the API.
        private HttpClient client = new HttpClient();
        //Cleaner class. So that I can get ride of the Json formmating. JSon Serialzation would have worked bettter! 
        //Note to future me.
        private PageReferenceClean cleaner = new PageReferenceClean();

        private readonly BigBrotherReduxContext _context;

        public PageReferencesController(BigBrotherReduxContext context)
        {
            _context = context;
        }

        // GET: PageReferences
     
        public async Task<IActionResult> Index()
        {
            var data = await client.GetStringAsync("http://52.168.32.232/BigBrotherRedux/PageReference/ReadAll");
            data = cleaner.RemoveSquareBraces(data);
            List<string> pageInf = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrepPageReference(pageInf);

            return View(model);
        }

        // GET: PageReferences/Details/5
   
        public async Task<IActionResult> Details(string id)
        {
            var data = await client.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/PageReference/GetPageReference/{id}");
            data = cleaner.RemoveSquareBraces(data);
            List<string> pageRefIn = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrepPageReference(pageRefIn);
            return View(model[0]);
        }

        // GET: PageReferences/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageReferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("PageId,DateAdded,PageDescription")] PageReference pageReference)
        {
            if (ModelState.IsValid)
            {

                var data = await client.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/PageReference/CreatePageRefrence/{pageReference.DateAdded}/{pageReference.PageDescription}");

            }
            return RedirectToAction("Index");
        }

        // GET: PageReferences/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageReference = await _context.PageReference.FindAsync(id);
            if (pageReference == null)
            {
                return NotFound();
            }
            return View(pageReference);
        }

        // POST: PageReferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("PageId,DateAdded,PageDescription")] PageReference pageReference)
        {
            if (id != pageReference.PageId)
            {
                return NotFound();
            }
            string pageID, DateAdded, PageDescription;
            pageID = pageReference.PageId.ToString();
            DateAdded = pageReference.DateAdded.ToString();
            PageDescription= pageReference.PageDescription.ToString();

            var data = await client.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/PageReference/EditPageReference/{pageID}/{DateAdded}/{PageDescription}");
            data = cleaner.RemoveSquareBraces(data);
            List<string> pagerefIn = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrepPageReference(pagerefIn);
            return View(model[0]);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditPost(int id, [Bind("PageId,DateAdded,PageDescription")] PageReference pageReference)
        {
        
            string pageID, DateAdded, PageDescription;
            pageID = pageReference.PageId.ToString();
            DateAdded = pageReference.DateAdded.ToString();
            PageDescription = pageReference.PageDescription.ToString();

            var data = await client.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/PageReference/EditPageReference/{pageID}/{DateAdded}/{PageDescription}");
            return RedirectToAction("Index");
        }




        // GET: PageReferences/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await client.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/PageReference/GetPageReference/{id}");
            data = cleaner.RemoveSquareBraces(data);
            List<string> pagerefIn = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrepPageReference(pagerefIn);
            return View(model[0]);
        }

        // POST: PageReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await client.GetStringAsync($"http://52.168.32.232/BigBrotherRedux/PageReference/DeletePageReference/{id}");
            return RedirectToAction("Index");
        }



        private bool PageReferenceExists(int id)
        {
            return _context.PageReference.Any(e => e.PageId == id);
        }
    }
}
