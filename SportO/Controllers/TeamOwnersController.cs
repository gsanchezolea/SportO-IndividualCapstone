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
    public class TeamOwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamOwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamOwners
        public async Task<IActionResult> Index()
        {
            var userLoggedIn = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dbTeamOwner = _context.TeamOwners.Where(x => x.IdentityUserId == userLoggedIn).FirstOrDefault();
            var teamOwnerTeams = _context.Teams.Where(x => x.TeamOwnerId == dbTeamOwner.Id).ToList(); 
            return View(teamOwnerTeams);
        }

        // GET: TeamOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamOwner = await _context.TeamOwners
                .Include(t => t.Phone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamOwner == null)
            {
                return NotFound();
            }

            return View(teamOwner);
        }

        // GET: TeamOwners/Create
        public IActionResult Create()
        {
            var teamOwner = new TeamOwner();
            teamOwner = _context.TeamOwners.Include(l => l.Phone).FirstOrDefault();
            return View(teamOwner);
        }

        // POST: TeamOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamOwner teamOwner)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                teamOwner.IdentityUserId = userId;
                _context.Add(teamOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "phoneNumber", teamOwner.PhoneId);
            return View(teamOwner);
        }

        // GET: TeamOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamOwner = await _context.TeamOwners.FindAsync(id);
            if (teamOwner == null)
            {
                return NotFound();
            }
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "phoneNumber", teamOwner.PhoneId);
            return View(teamOwner);
        }

        // POST: TeamOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,firstName,lastName,accountActive,PhoneId")] TeamOwner teamOwner)
        {
            if (id != teamOwner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamOwnerExists(teamOwner.Id))
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
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "phoneNumber", teamOwner.PhoneId);
            return View(teamOwner);
        }

        // GET: TeamOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamOwner = await _context.TeamOwners
                .Include(t => t.Phone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamOwner == null)
            {
                return NotFound();
            }

            return View(teamOwner);
        }

        // POST: TeamOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamOwner = await _context.TeamOwners.FindAsync(id);
            _context.TeamOwners.Remove(teamOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamOwnerExists(int id)
        {
            return _context.TeamOwners.Any(e => e.Id == id);
        }
        
    }
}
