using System.ComponentModel.DataAnnotations;

namespace ConsultorioAPI.Models
{
    public class Consultorio
    {
        public int Id { get; set; }
        [Required] public string Nome { get; set; }
        [Required] public int Numero { get; set; }

        [Required] public string? Cep { get; set; }

        [Required] public string? Logradouro { get; set; }

        [Required] public string? Bairro { get; set; }

        [Required] public string? Localidade { get; set; }

        [Required] public string? Uf { get; set; }


        //public ICollection<Medico> Medicos { get; set; }

    }
}
