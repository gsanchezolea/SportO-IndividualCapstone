using SportO_SLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportO.Models
{
    public class Season
    {

        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Season Name")]
        public string seasonName { get; set; }

        [Display(Name = "Matches Played")]
        public int matchesPlayed { get; set; }

        [Display(Name = "Matches Won")]
        public int matchesWon { get; set; }        

        [Display(Name = "Matches Lost")]
        public int matchesLost { get; set; }

        [Display(Name = "Matches Tied")]
        public int matchesTied { get; set; }

        [Display(Name = "Match Points in Favor")]
        public int mPointsFavor { get; set; }

        [Display(Name = "Match Points Against")]
        public int mPointsAgainst { get; set; }

        [Display(Name = "Season Record")]
        public int seasonRecord { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
