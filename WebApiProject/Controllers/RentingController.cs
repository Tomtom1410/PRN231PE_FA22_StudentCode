using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentingController : ControllerBase
    {
        private readonly PRN231PE_FA22_StudentCodeContext _context;

        public RentingController()
        {
            _context = new PRN231PE_FA22_StudentCodeContext();
        }
        [HttpGet]
        [Route("GetAllRenting")]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var response = await _context.Renting.Include(c => c.Company).ToListAsync();

            return Ok(response);
        }
    }
}
