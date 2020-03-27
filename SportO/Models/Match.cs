using SportO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportO_SLMS.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Match Day")]
        public int matchDay { get; set; }

        [Display(Name = "Date/Time")]
        public DateTime date { get; set; }

        
        [Display(Name = "Location")]
        public string location { get; set; }

       
        [Display(Name = "Home Team Score")]
        public int homeTeamScore { get; set; }

        
        [Display(Name = "Away Team Score")]
        public int awayTeamScore { get; set; }

        [ForeignKey("HomeTeam")]
        public int? HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        [ForeignKey("AwayTeam")]
        public int? AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        [ForeignKey("Referee")]
        public int RefereeId { get; set; }
        public Referee Referee { get; set; }

        [ForeignKey("League")]
        public int? LeagueId { get; set; }
        public League League { get; set; }

    }
}