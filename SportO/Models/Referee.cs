using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportO_SLMS.Models
{
    public class Referee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        public bool accountActive { get; set; }

        [ForeignKey("Phone")]
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}