using BusinessObjects;
using BusinessObjects.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyProfileController : ControllerBase
    {
        private readonly PRN231PE_FA22_StudentCodeContext _context;

        public PropertyProfileController()
        {
            _context = new PRN231PE_FA22_StudentCodeContext();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] HRStaffDto model)
        {
            var response = await _context.HRStaffs
                .FirstOrDefaultAsync(x => x.EmailAddress.Equals(model.EmailAddress) && x.Password.Equals(model.Password));

            if (response == null)
            {
                return NotFound();
            }
            var role = Role.Administrator;

            switch (response.Role)
            {
                case 2:
                    role = Role.Manager;
                    break;
                case 3:
                    role = Role.Staff;
                    break;
            }
            var hr = new HRStaffDto()
            {
                EmailAddress = response.EmailAddress,
                Fullname = response.Fullname,
                Role = role
            };
            return Ok(hr);
        }

        [HttpGet]
        [Route("GetAllPropertyProfile")]
        [EnableQuery]
        public async Task<IActionResult> GetAllPropertyProfile()
        {
            var response = await _context.PropertyProfiles.ToListAsync();
            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] PropertyProfileDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = new PropertyProfile()
            {
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                Price = model.Price,
                Location = model.Location,
                PropertyDescription = model.PropertyDescription,
                Status = model.Status,
            };

            _context.PropertyProfiles.Add(entity);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(model);
            }

            return Conflict("Have an error in processing!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] PropertyProfileDto model)
        {
            if (model.PropertyProfileID == null || model.PropertyProfileID <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var profile = await _context.PropertyProfiles.AsNoTracking().FirstOrDefaultAsync(x => x.PropertyProfileID == model.PropertyProfileID);

            if (profile == null)
            {
                return NotFound("Not found profile!");
            }

            var entity = new PropertyProfile()
            {
                PropertyProfileID = (int)model.PropertyProfileID,
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                Price = model.Price,
                Location = model.Location,
                PropertyDescription = model.PropertyDescription,
                Status = model.Status,
            };
            try
            {
                _context.PropertyProfiles.Update(entity);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Conflict("Have an error in processing!");
        }

        [HttpDelete]
        [Route("Delete/{PropertyProfileID}")]
        public async Task<IActionResult> Delete([FromRoute] int PropertyProfileID)
        {

            var profile = await _context.PropertyProfiles.AsNoTracking().FirstOrDefaultAsync(x => x.PropertyProfileID == PropertyProfileID);

            if (profile == null)
            {
                return NotFound("Not found profile!");
            }
            try
            {
                _context.PropertyProfiles.Remove(profile);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok("Delete Success");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return Conflict("Have an error in processing!");
        }
    }
}
