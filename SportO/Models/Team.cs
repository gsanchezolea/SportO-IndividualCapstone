using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportO_SLMS.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Account Active")]
        public bool accountActive { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Available Spots")]
        public int capacity { get; set; }

        [ForeignKey("League")]
        public int LeagueId { get; set; }
        public League League { get; set; }

        [ForeignKey("TeamOwner")]
        public int? TeamOwnerId { get; set; }
        public TeamOwner TeamOwner { get; set; }

    }
}
