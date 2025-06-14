using EhsaasHub.Models.ERP.HR;
using EhsaasHub.Repositories.HR;
using Microsoft.AspNetCore.Mvc;

namespace EhsaasHub.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StaffMemberController : ControllerBase
    {
        private readonly IStaffMemberRepository _repository;

        public StaffMemberController(IStaffMemberRepository repository)
        {
            _repository = repository;
        }

        // GET: api/v1/StaffMember/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var staff = await _repository.GetAllAsync();
            return Ok(staff);
        }

        // GET: api/v1/StaffMember/getbyid/5
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _repository.GetByIdAsync(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        // POST: api/v1/StaffMember/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] StaffMember staff)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(staff);
            return CreatedAtAction(nameof(GetById), new { id = staff.Id }, staff);
        }

        // PUT: api/v1/StaffMember/update/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StaffMember staff)
        {
            if (id != staff.Id)
                return BadRequest("ID mismatch");

            await _repository.UpdateAsync(staff);
            return NoContent();
        }

        // DELETE: api/v1/StaffMember/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
