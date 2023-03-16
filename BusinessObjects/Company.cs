using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }
        [Required]
        public string ContactName { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Renting> Rentings { get; } = new List<Renting>();
    }
}
