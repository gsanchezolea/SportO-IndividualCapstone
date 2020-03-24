using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportO_SLMS.Models;

namespace SportO.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Name = "League Owner",
                    NormalizedName = "LEAGUE OWNER",
                },
                new IdentityRole
                {
                    Name = "Team Owner",
                    NormalizedName = "TEAM OWNER",
                },
                new IdentityRole
                {
                    Name = "Referee",
                    NormalizedName = "REFEREE",
                },
                new IdentityRole
                {
                    Name = "Player",
                    NormalizedName = "PLAYER",
                }
                );
            builder.Entity<Sport>()
                .HasData(
                new Sport
                {
                    Id = 1,
                    type = "Soccer"
                },
                new Sport
                {
                    Id = 2,
                    type = "Football"
                },
                new Sport
                {
                    Id = 3,
                    type = "Basketball"
                },
                new Sport
                {
                    Id = 4,
                    type = "Hockey"
                },
                new Sport
                {
                    Id = 5,
                    type = "Rugby"
                }
                );
        }

        //DbSets to Database
        public DbSet<Phone> Phones { get; set; }
        public DbSet<LeagueOwner> LeagueOwners { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<TeamOwner> TeamOwners { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Roster> Rosters { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
