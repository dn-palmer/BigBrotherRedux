#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBDisplay.Models;

namespace BBDisplay.Controllers
{
    public class PageReferencesController : Controller
    {
        private readonly BigBrotherReduxContext _context;

        public PageReferencesController(BigBrotherReduxContext context)
        {
            _context = context;
        }

        // GET: PageReferences
        public async Task<IActionResult> Index()
        {
            return View(await _context.PageReference.ToListAsync());
        }

        // GET: PageReferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageReference = await _context.PageReference
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (pageReference == null)
            {
                return NotFound();
            }

            return View(pageReference);
        }

        // GET: PageReferences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageReferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,DateAdded,PageDescription")] PageReference pageReference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageReference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageReference);
        }

        // GET: PageReferences/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("PageId,DateAdded,PageDescription")] PageReference pageReference)
        {
            if (id != pageReference.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageReference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageReferenceExists(pageReference.PageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pageReference);
        }

        // GET: PageReferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageReference = await _context.PageReference
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (pageReference == null)
            {
                return NotFound();
            }

            return View(pageReference);
        }

        // POST: PageReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageReference = await _context.PageReference.FindAsync(id);
            _context.PageReference.Remove(pageReference);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageReferenceExists(int id)
        {
            return _context.PageReference.Any(e => e.PageId == id);
        }
    }
}
