using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportO.Data;
using SportO_SLMS.Models;

namespace SportO.Controllers
{
    public class RostersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RostersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rosters
        public async Task<IActionResult> Index(int id)
        {
            var roster = _context.Rosters.Include(r => r.Player).Include(r => r.Team).Where(r => r.TeamId == id);
            return View(roster);
        }

        // GET: Rosters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters
                .Include(r => r.Player)
                .Include(r => r.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roster == null)
            {
                return NotFound();
            }

            return View(roster);
        }

        // GET: Rosters/Create
        public IActionResult Create()
        {
            var roster = new Roster();
            var teams = _context.Teams.ToList();
            ViewBag.Teams = new SelectList(teams, "Id", "name");

            return View(roster);
        }

        // POST: Rosters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Roster roster)
        {
            var team = _context.Teams.Where(t => t.Id == roster.TeamId).SingleOrDefault();
            if (ModelState.IsValid && team.capacity != 0)
            {
                var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var dbUserId = _context.Players.Where(p => p.IdentityUserId == identityUserId).FirstOrDefault().Id;
                roster.PlayerId = dbUserId;
                team.capacity--;
                _context.Add(roster);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Players");
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "firstName", roster.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "name", roster.TeamId);
            return View(roster);
        }

        // GET: Rosters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters.FindAsync(id);
            if (roster == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "firstName", roster.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "name", roster.TeamId);
            return View(roster);
        }

        // POST: Rosters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,playingPosition,kitNumber,TeamId,PlayerId")] Roster roster)
        {
            if (id != roster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RosterExists(roster.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "TeamOwners");
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "firstName", roster.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "name", roster.TeamId);
            return View(roster);
        }

        // GET: Rosters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roster = await _context.Rosters
                .Include(r => r.Player)
                .Include(r => r.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roster == null)
            {
                return NotFound();
            }

            return View(roster);
        }

        // POST: Rosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roster = await _context.Rosters.FindAsync(id);
            _context.Rosters.Remove(roster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RosterExists(int id)
        {
            return _context.Rosters.Any(e => e.Id == id);
        }
    }
}
