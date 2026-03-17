using ConsultorioAPI.Data;
using ConsultorioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioAPI.Controllers
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

        public async Task<ActionResult> PutPaciente(int id, Paciente paciente)
        {
            if (id != paciente.Id) return BadRequest("ID do paciente não encontrado ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var pacienteExistente = await _context.Pacientes.FindAsync(id);

            if (pacienteExistente == null) { return NotFound(); }

            if (await _context.Pacientes.AnyAsync(p => (p.Cpf == paciente.Cpf || p.Email == paciente.Email) && p.Id != id))
            {
                return BadRequest("CPF ou Email já existe para outro paciente.");
            }

            pacienteExistente.Nome = paciente.Nome;
            pacienteExistente.Email = paciente.Email;
            pacienteExistente.Cpf = paciente.Cpf;

            _context.Update(pacienteExistente);
            await _context.SaveChangesAsync();
            return Ok(pacienteExistente);
        }
        


            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePaciente(int id)
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
}
