
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTOs
{
    public class HRStaffDto
    {
        [Required]
        public string EmailAddress { get; set; }
        public string? Fullname { get; set; }
        [Required]
        public string Password { get; set; }
        public Role? Role { get; set; }
    }
}
