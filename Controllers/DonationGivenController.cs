using EhsaasHub.Models.ERP;
using EhsaasHub.Repositories.Donation;
using Microsoft.AspNetCore.Mvc;

namespace EhsaasHub.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DonationGivenController : ControllerBase
    {
        private readonly IDonationGivenRepository _repository;

        public DonationGivenController(IDonationGivenRepository repository)
        {
            _repository = repository;
        }

        // GET: api/v1/DonationGiven/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var donations = await _repository.GetAllAsync();
            return Ok(donations);
        }

        // GET: api/v1/DonationGiven/getbyid/5
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donation = await _repository.GetByIdAsync(id);
            if (donation == null)
                return NotFound();

            return Ok(donation);
        }

        // POST: api/v1/DonationGiven/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DonationGiven donation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(donation);
            return CreatedAtAction(nameof(GetById), new { id = donation.Id }, donation);
        }

        // PUT: api/v1/DonationGiven/update/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DonationGiven donation)
        {
            if (id != donation.Id)
                return BadRequest("ID mismatch");

            await _repository.UpdateAsync(donation);
            return NoContent();
        }

        // DELETE: api/v1/DonationGiven/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
