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
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teams.Include(t => t.League).Include(t => t.TeamOwner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.League)
                .Include(t => t.TeamOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            var team = new Team();
            var leagues = _context.Leagues.ToList();
            ViewBag.League = new SelectList(leagues, "Id", "leagueName");
            return View(team);
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            var dbLeague = _context.Leagues.Where(l => l.Id == team.LeagueId).SingleOrDefault();           
               
                if (ModelState.IsValid && dbLeague.teamCapacity != 0)
                {                    
                    var identityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var dbUserLoggedInId = _context.TeamOwners.Where(t => t.IdentityUserId == identityUserId).FirstOrDefault().Id;
                    team.TeamOwnerId = dbUserLoggedInId; 
                    _context.Add(team);
                    dbLeague.teamCapacity--;
                    await _context.SaveChangesAsync();                    
                    return RedirectToAction("Index", "TeamOwners");
                }
               
            
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "leagueName", team.LeagueId);
            ViewData["TeamOwnerId"] = new SelectList(_context.TeamOwners, "Id", "firstName", team.TeamOwnerId);
            return View(team);

        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "leagueName", team.LeagueId);
            ViewData["TeamOwnerId"] = new SelectList(_context.TeamOwners, "Id", "firstName", team.TeamOwnerId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,capacity,LeagueId,TeamOwnerId")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "leagueName", team.LeagueId);
            ViewData["TeamOwnerId"] = new SelectList(_context.TeamOwners, "Id", "firstName", team.TeamOwnerId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.League)
                .Include(t => t.TeamOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
