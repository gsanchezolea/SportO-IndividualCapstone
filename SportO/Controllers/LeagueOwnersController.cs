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
    public class LeagueOwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeagueOwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeagueOwners
        public async Task<IActionResult> Index()
        {
            var userLoggedInId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); //QUery to find the Id of the user logged in
            var dbLeagueOwner = _context.LeagueOwners.Where(s => s.IdentityUserId == userLoggedInId).FirstOrDefault(); //Query to find Where the Owners IdentityUserID matches with the the userID.  Returns an object with properties
            var leagueOwnerLeagues = _context.Leagues.Include(s=>s.Sport).Where(s => s.LeagueOwnerId == dbLeagueOwner.Id).ToList(); //query to find the leagues where the leagueowner matches with the leagueowner we found in the database, which we then to list to create a list of leagues where this is true.

            return View(leagueOwnerLeagues);
        }

        // GET: LeagueOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueOwner = await _context.LeagueOwners
                .Include(l => l.Phone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueOwner == null)
            {
                return NotFound();
            }

            return View(leagueOwner);
        }

        // GET: LeagueOwners/Create
        public IActionResult Create()
        {
            var leagueOwner = new LeagueOwner();
            leagueOwner = _context.LeagueOwners.Include(l => l.Phone).FirstOrDefault();
            return View(leagueOwner);
        }

        // POST: LeagueOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueOwner leagueOwner)
        {    
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                leagueOwner.IdentityUserId = userId;
                _context.Add(leagueOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "phoneNumber", leagueOwner.PhoneId);
            return View(leagueOwner);
        }

        // GET: LeagueOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueOwner = await _context.LeagueOwners.FindAsync(id);
            if (leagueOwner == null)
            {
                return NotFound();
            }
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "phoneNumber", leagueOwner.PhoneId);
            return View(leagueOwner);
        }

        // POST: LeagueOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,firstName,lastName,accountActive,PhoneId")] LeagueOwner leagueOwner)
        {
            if (id != leagueOwner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leagueOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueOwnerExists(leagueOwner.Id))
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
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "phoneNumber", leagueOwner.PhoneId);
            return View(leagueOwner);
        }

        // GET: LeagueOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueOwner = await _context.LeagueOwners
                .Include(l => l.Phone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueOwner == null)
            {
                return NotFound();
            }

            return View(leagueOwner);
        }

        // POST: LeagueOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leagueOwner = await _context.LeagueOwners.FindAsync(id);
            _context.LeagueOwners.Remove(leagueOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueOwnerExists(int id)
        {
            return _context.LeagueOwners.Any(e => e.Id == id);
        }
    }
}
