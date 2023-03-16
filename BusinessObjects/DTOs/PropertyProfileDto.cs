using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTOs
{
    public class PropertyProfileDto
    {
        public int? PropertyProfileID { get; set; }
        [Required]
        [MinLength(8)]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9/ ]+$")]
        public string Location { get; set; }
        [Required]
        public string PropertyDescription { get; set; }
        [Required]
        public string Status { get; set; }
        public List<Renting>? Rentings { get; set;}
    }
}
