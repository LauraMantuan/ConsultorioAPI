namespace ConsultorioAPI.DTOs
{
    public class MedicoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Crm { get; set; }
        public int ConsultorioId { get; set; }
        public string ConsultorioNome { get; set; }
    }
}
