using ConsultorioApi.Data;
using ConsultorioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PessoasController(AppDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPaciente()
        {
            var paciente = await _context.Pacientes.ToListAsync();
            return paciente;
        }
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {


            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return NotFound();
            return paciente;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
