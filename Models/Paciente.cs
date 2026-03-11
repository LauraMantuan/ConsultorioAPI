using System.ComponentModel.DataAnnotations;

namespace ConsultorioAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Paciente
    {
        public int Id { get; set; }
        [Required]public string Nome { get; set; }

        [Required, EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string Email { get; set; }

        [Required, StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres")]
        public string Cpf { get; set; }
    }
}
