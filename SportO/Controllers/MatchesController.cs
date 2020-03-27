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
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Matches.Include(m => m.AwayTeam).Include(m => m.HomeTeam).Include(m => m.Referee).Include(m => m.League);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.Referee)
                .Include(m => m.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "name");
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "name");
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "firstName");
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "seasonName");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,matchDay,date,location,homeTeamScore,awayTeamScore,HomeTeamId,AwayTeamId,RefereeId,SeasonId")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "name", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "name", match.HomeTeamId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "firstName", match.RefereeId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "seasonName", match.LeagueId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "name", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "name", match.HomeTeamId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "firstName", match.RefereeId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "seasonName", match.LeagueId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,matchDay,date,location,homeTeamScore,awayTeamScore,HomeTeamId,AwayTeamId,RefereeId,SeasonId")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "name", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "name", match.HomeTeamId);
            ViewData["RefereeId"] = new SelectList(_context.Referees, "Id", "firstName", match.RefereeId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "seasonName", match.LeagueId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.Referee)
                .Include(m => m.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GenerateMatches(int id)
        {
            if (id == null)
            {
                return View();
            }
            var refereelist = _context.Referees.ToList();
            var teamList = _context.Teams.Where(s => s.LeagueId == id).ToList();

            var today = DateTime.Now;
            var initialDate = today.AddDays(7);

            List<Match> localList = new List<Match>();

            for (int i = 0; i < teamList.Count; i++)
            {
                
                for (int j = 0; j < teamList.Count; j++)
                {
                    
                    if(teamList[i] != teamList[j]) 
                    {
                        var matchCount1 = _context.Matches.Where(m => m.HomeTeamId == teamList[i].Id && m.AwayTeamId == teamList[j].Id).ToList();
                        if (matchCount1.Count < 1)
                        {
                            Match matchHome = new Match()
                            {
                                date = today,
                                HomeTeamId = teamList[i].Id,
                                AwayTeamId = teamList[j].Id,
                                RefereeId = refereelist[0].Id,
                                LeagueId = id


                            };
                            await _context.Matches.AddAsync(matchHome);
                            localList.Add(matchHome);
                        }
                    }                    
                    //var secondDate = initialDate.AddDays(3);                    
                    if(teamList[i] != teamList[j])
                    {
                        var matchCount2 = _context.Matches.Where(m => m.HomeTeamId == teamList[j].Id && m.AwayTeamId == teamList[i].Id).ToList();
                        if (matchCount2.Count < 1)
                        {
                            Match matchAway = new Match()
                            {
                                date = today,
                                HomeTeamId = teamList[j].Id,
                                AwayTeamId = teamList[i].Id,
                                RefereeId = refereelist[0].Id,
                                LeagueId = id
                            };
                            await _context.Matches.AddAsync(matchAway);
                            localList.Add(matchAway);
                        }
                    }
                                                
                    
                    
                    await _context.SaveChangesAsync();
                    //initialDate = secondDate.AddDays(4);

                }
            }

            return RedirectToAction("Index", "Matches");
        }

    }
}
