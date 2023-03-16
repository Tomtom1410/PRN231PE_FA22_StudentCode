using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTOs
{
    public class RentingDto
    {
        public int CompanyID { get; set; }
        public int PropertyProfileID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Company Company { get; set; }
        public PropertyProfile PropertyProfile { get; set; }
    }
}
