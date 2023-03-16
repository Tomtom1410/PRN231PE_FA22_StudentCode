using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Renting
    {
        [Key]
        public int CompanyID { get; set; }
        [Key]
        public int PropertyProfileID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual Company Company { get; set; } = null!;
        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual PropertyProfile PropertyProfile { get; set; } = null!;
    }
}
