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
    public class UserIPDatasController : Controller
    {
        private readonly BigBrotherReduxContext _context;

        public UserIPDatasController(BigBrotherReduxContext context)
        {
            _context = context;
        }

        // GET: UserIPDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserIPData.ToListAsync());
        }

        // GET: UserIPDatas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIPData = await _context.UserIPData
                .FirstOrDefaultAsync(m => m.UserIP == id);
            if (userIPData == null)
            {
                return NotFound();
            }

            return View(userIPData);
        }

        // GET: UserIPDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserIPDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserIP,CountryCode,CountryName,StateOrRegion,City,ZipCode,VisitCount,DeviceType")] UserIPData userIPData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userIPData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userIPData);
        }

        // GET: UserIPDatas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIPData = await _context.UserIPData.FindAsync(id);
            if (userIPData == null)
            {
                return NotFound();
            }
            return View(userIPData);
        }

        // POST: UserIPDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserIP,CountryCode,CountryName,StateOrRegion,City,ZipCode,VisitCount,DeviceType")] UserIPData userIPData)
        {
            if (id != userIPData.UserIP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userIPData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserIPDataExists(userIPData.UserIP))
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
            return View(userIPData);
        }

        // GET: UserIPDatas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIPData = await _context.UserIPData
                .FirstOrDefaultAsync(m => m.UserIP == id);
            if (userIPData == null)
            {
                return NotFound();
            }

            return View(userIPData);
        }

        // POST: UserIPDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userIPData = await _context.UserIPData.FindAsync(id);
            _context.UserIPData.Remove(userIPData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserIPDataExists(string id)
        {
            return _context.UserIPData.Any(e => e.UserIP == id);
        }
    }
}
