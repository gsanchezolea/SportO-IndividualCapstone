using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportO_SLMS.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "League Name")]
        public string leagueName { get; set; }

        [Required]
        [Display(Name = "Team Capacity")]
        public int teamCapacity { get; set; }

        [ForeignKey("LeagueOwner")]
        public int LeagueOwnerId { get; set; }
        public LeagueOwner LeagueOwner { get; set; }

        [ForeignKey("Sport")]
        public int SportId { get; set; }
        public Sport Sport { get; set; }
    }
}