using ConsultorioAPI.Data;
using ConsultorioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsultasController(AppDbContext context)
        {
            _context = context;

        }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultas()
    {
        var consulta = await _context.Consultas.ToListAsync();
        return consulta;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Consulta>> GetConsulta(int id)
    {
        var consulta = await _context.Consultas.FindAsync(id);
        if (consulta == null) return NotFound();
        return consulta;
    }

    [HttpPost]
    public async Task<ActionResult<Consulta>> PostConsulta(Consulta consulta)
    {

        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConsulta), new { id = consulta.Id }, consulta);

    }

        public async Task<ActionResult> PutConsulta(int id, Consulta consulta)
        {
            if (id != consulta.Id) return BadRequest("ID da consulta não encontrado ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var consultaExistente = await _context.Consultas.FindAsync(id);

            if (consultaExistente == null) return NotFound();

            consultaExistente.PacienteId = consulta.PacienteId;
            consultaExistente.DataHora = consulta.DataHora;
            consultaExistente.Observacoes = consulta.Observacoes;
            consultaExistente.MedicoId = consulta.MedicoId;

            _context.Update(consultaExistente);
            await _context.SaveChangesAsync();
            return Ok(consultaExistente);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound("Consulta não encontrada");
            }
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

};
