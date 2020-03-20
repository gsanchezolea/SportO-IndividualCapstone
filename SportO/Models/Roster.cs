using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportO_SLMS.Models
{
    public class Roster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string playingPosition { get; set; }

        [Required]
        [Display(Name = "Kit Number")]
        public int kitNumber { get; set; }

        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public Team Team { get; set; }

        [ForeignKey("Player")]
        public int? PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
