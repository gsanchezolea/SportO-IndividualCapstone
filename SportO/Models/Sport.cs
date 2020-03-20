using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportO_SLMS.Models
{
    public class Sport
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Sport")]
        public string type { get; set; }

    }
}
