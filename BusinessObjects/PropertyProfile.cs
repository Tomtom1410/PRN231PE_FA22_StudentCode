using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class PropertyProfile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropertyProfileID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string PropertyDescription { get; set; }
        [Required]
        public string Status { get; set; }
        public virtual ICollection<Renting> Rentings { get; } = new List<Renting>();
    }
}
