using ClosedXML.Excel;
using EhsaasHub.Models.ERP.HR;
using EhsaasHub.Repositories.HR;
using Microsoft.AspNetCore.Mvc;

namespace EhsaasHub.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SalaryRecordController : ControllerBase
    {
        private readonly ISalaryRecordRepository _repository;

        public SalaryRecordController(ISalaryRecordRepository repository)
        {
            _repository = repository;
        }

        // GET: api/v1/SalaryRecord/getall
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var records = await _repository.GetAllAsync();
            return Ok(records);
        }

        // GET: api/v1/SalaryRecord/getbyid/5
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        // GET: api/v1/SalaryRecord/getbystaff/3
        [HttpGet("getbystaff/{staffId}")]
        public async Task<IActionResult> GetByStaffId(int staffId)
        {
            var records = await _repository.GetByStaffMemberIdAsync(staffId);
            return Ok(records);
        }

        // POST: api/v1/SalaryRecord/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SalaryRecord record)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(record);
            return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
        }

        // PUT: api/v1/SalaryRecord/update/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SalaryRecord record)
        {
            if (id != record.Id)
                return BadRequest("ID mismatch");

            await _repository.UpdateAsync(record);
            return NoContent();
        }

        // DELETE: api/v1/SalaryRecord/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/v1/SalaryRecord/export-excel
        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportAllToExcel()
        {
            var records = await _repository.GetAllAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Salary Records");

            // Headers
            worksheet.Cell(1, 1).Value = "Staff Name";
            worksheet.Cell(1, 2).Value = "Month";
            worksheet.Cell(1, 3).Value = "Amount";
            worksheet.Cell(1, 4).Value = "Paid";
            worksheet.Cell(1, 5).Value = "Paid On";
            worksheet.Cell(1, 6).Value = "Notes";

            int row = 2;
            foreach (var record in records)
            {
                worksheet.Cell(row, 1).Value = record.StaffMember?.FullName ?? "Unknown";
                worksheet.Cell(row, 2).Value = record.SalaryMonth.ToString("yyyy-MM");
                worksheet.Cell(row, 3).Value = record.Amount;
                worksheet.Cell(row, 4).Value = record.IsPaid ? "Yes" : "No";
                worksheet.Cell(row, 5).Value = record.PaidOn?.ToString("yyyy-MM-dd") ?? "-";
                worksheet.Cell(row, 6).Value = record.Notes ?? "";
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalaryRecords.xlsx");
        }

    }
}
