using ConsultorioAPI.Data;
using ConsultorioAPI.Models;
using ConsultorioAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultoriosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ViaCepServices _viaCepService;

        public ConsultoriosController(AppDbContext context, ViaCepServices viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsultorios()
        {
            var consultorios = await _context.Consultorios.ToListAsync();
            return Ok (consultorios);
        }
        [HttpPost]
        public async Task<IActionResult> PostConsultorio(Consultorio consultorio)
        {
            var endereco = await _viaCepService.BuscarEnderecoAsync(consultorio.Cep);
            if (endereco != null)
            {   
                consultorio.Logradouro = endereco.logradouro;
                consultorio.Bairro = endereco.bairro;
                consultorio.Localidade = endereco.localidade;
                consultorio.Uf = endereco.uf;
            }
            else
            {
                return NotFound("CEP inválido ou não encontrado.");
            }

            _context.Consultorios.Add(consultorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConsultorios), new { id = consultorio.Id }, consultorio);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Consultorio>> GetConsultorioID(int id)
        {
            var consultorio = await _context.Consultorios.FindAsync(id);
            if (consultorio == null) return NotFound();
            return consultorio;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutConsultorio(int id, Consultorio consultorio)
        {
            if (id != consultorio.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enderecoExistente = await _context.Consultorios.FindAsync(id);

            if (enderecoExistente == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(consultorio.Cep) || consultorio.Cep.Length != 8)
            {
                return BadRequest("O campo CEP é obrigatório.");
            }
            else
            {
                var enderecoViaCep = await _viaCepService.BuscarEnderecoAsync(consultorio.Cep);
                enderecoExistente.Logradouro = enderecoViaCep.logradouro;
                enderecoExistente.Bairro = enderecoViaCep.bairro;
                enderecoExistente.Localidade = enderecoViaCep.localidade;
                enderecoExistente.Uf = enderecoViaCep.uf;
            }
            enderecoExistente.Nome = consultorio.Nome;
            enderecoExistente.Numero = consultorio.Numero;

            _context.Update(enderecoExistente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<ActionResult> Deleteconsultorio(int id)
        {
            var consultorio = await _context.Consultorios.FindAsync(id);
            if (consultorio == null)
            {
                return NotFound();
            }
            _context.Consultorios.Remove(consultorio);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}
