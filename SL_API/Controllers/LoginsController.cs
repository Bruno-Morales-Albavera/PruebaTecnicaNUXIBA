using Microsoft.AspNetCore.Mvc;
using ML;
using BL;

namespace SL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginsController : Controller
    {
        private readonly LoginService _service;
        private readonly CsvService _csvService;

        // UN SOLO constructor con ambos servicios
        public LoginsController(LoginService service, CsvService csvService)
        {
            _service = service;
            _csvService = csvService;
        }

        // GET /logins
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // POST /logins
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CcLogLogin login)
        {
            var (success, message, data) = await _service.CreateAsync(login);
            if (!success) return BadRequest(new { message });
            return CreatedAtAction(nameof(GetAll), new { id = data!.Id },
                new { message, data });
        }

        // PUT /logins/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CcLogLogin login)
        {
            var (success, message) = await _service.UpdateAsync(id, login);
            if (!success) return BadRequest(new { message });
            return Ok(new { message });
        }

        // DELETE /logins/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, message) = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { message });
            return Ok(new { message });
        }

        // GET /logins/export
        [HttpGet("export")]
        public async Task<IActionResult> ExportCsv()
        {
            var csvBytes = await _csvService.GenerateCsvAsync();
            return File(csvBytes, "text/csv", "reporte_horas.csv");
        }

    }
}
