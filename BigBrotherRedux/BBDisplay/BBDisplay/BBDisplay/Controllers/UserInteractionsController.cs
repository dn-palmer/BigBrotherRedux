#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBDisplay.Models;

namespace BBDisplay.Controllers
{
    public class UserInteractionsController : Controller
    {
        private readonly BigBrotherReduxContext _context;

        public UserInteractionsController(BigBrotherReduxContext context)
        {
            _context = context;
        }

        // GET: UserInteractions
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserInteraction.ToListAsync());
        }

        // GET: UserInteractions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInteraction = await _context.UserInteraction
                .FirstOrDefaultAsync(m => m.UserInteractionID == id);
            if (userInteraction == null)
            {
                return NotFound();
            }

            return View(userInteraction);
        }

        // GET: UserInteractions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserInteractions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserInteractionID,UserSessionID,DateTime,CurrentPageID,InteractionLength")] UserInteraction userInteraction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInteraction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userInteraction);
        }

        // GET: UserInteractions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInteraction = await _context.UserInteraction.FindAsync(id);
            if (userInteraction == null)
            {
                return NotFound();
            }
            return View(userInteraction);
        }

        // POST: UserInteractions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserInteractionID,UserSessionID,DateTime,CurrentPageID,InteractionLength")] UserInteraction userInteraction)
        {
            if (id != userInteraction.UserInteractionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInteraction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInteractionExists(userInteraction.UserInteractionID))
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
            return View(userInteraction);
        }

        // GET: UserInteractions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInteraction = await _context.UserInteraction
                .FirstOrDefaultAsync(m => m.UserInteractionID == id);
            if (userInteraction == null)
            {
                return NotFound();
            }

            return View(userInteraction);
        }

        // POST: UserInteractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userInteraction = await _context.UserInteraction.FindAsync(id);
            _context.UserInteraction.Remove(userInteraction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInteractionExists(int id)
        {
            return _context.UserInteraction.Any(e => e.UserInteractionID == id);
        }
    }
}