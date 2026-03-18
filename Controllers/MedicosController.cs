using ConsultorioAPI.Data;
using ConsultorioAPI.DTOs;
using ConsultorioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioAPI.Controllers
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
        public async Task<IActionResult> GetMedicos()
        {
            var medicos = await _context.Medicos.ToListAsync();
            return Ok(medicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoResponseDto>> GetMedico(int id)
        {
            var medico = await _context.Medicos
                .Include(m => m.Consultorio)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (medico == null) return NotFound("Médico não encontrado");

            var medicoDto = new MedicoResponseDto
            {
                Id = medico.Id,
                Nome = medico.Nome,
                Crm = medico.Crm,
                ConsultorioId = medico.ConsultorioId,
                ConsultorioNome = medico.Consultorio.Nome
            };
            return Ok(medicoDto);
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {
            //var consutorioEx= await _context.Consultorios.FindAsync(medico.ConsultorioId);
            //if (consutorioEx == null) {

            var consultorioEx = await _context.Consultorios.AnyAsync(c => c.Id == medico.ConsultorioId);

            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(medico), new { id = medico.Id }, medico);

        }

        public async Task<ActionResult> PutMedico(int id, Medico medico)
        {
            if (id != medico.Id) return BadRequest("ID do medico não encontrado ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var medicoExistente = await _context.Medicos.FindAsync(id);

            if (medicoExistente == null) return NotFound();

            medicoExistente.Nome = medico.Nome;
            medicoExistente.Crm = medico.Crm;
            medicoExistente.ConsultorioId = medico.ConsultorioId;

            _context.Update(medicoExistente);
            await _context.SaveChangesAsync();
            return Ok(medicoExistente);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound("Medico nao encontrado");
            }
            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

