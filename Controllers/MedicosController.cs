using ConsultorioAPI.Models;
using Medico.Data;
using Medico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MedicosController(AppDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicos>>> GetMedico()
        {
            var medico = await _context.medicos.ToListAsync();
            return medico;
        }
        [HttpPost]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {


            _context.medicos.Add(medico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Getmedico), new { id = medico.Id }, medico);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Medico>> GetMedico(int id)
        {
            var medico = await _context.medicos.FindAsync(id);
            if (medico == null) return NotFound();
            return medico;
        }

        public async Task<ActionResult> PutMedico(int id, Medico medico)
        {
            if (id != medico.Id) return BadRequest("ID do medico não encontrado ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var medicoExistente = await _context.medicos.FindAsync(id);

            if (medicoExistente == null) { return NotFound(); }

            if (await _context.medicos.AnyAsync(p => (p.Cpf == medico.Cpf || p.Email == medico.Email) && p.Id != id))
            {
                return BadRequest("CPF ou Email já existe para outro medico.");
            }

            medicoExistente.Nome = medico.Nome;
            medicoExistente.Cpf = medico.Crm;

            _context.Update(medicoExistente);
            await _context.SaveChangesAsync();
            return Ok(medicoExistente);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemedico(int id)
        {
            var medico = await _context.medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            _context.medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
}
