using Microsoft.EntityFrameworkCore;
using ConsultorioAPI.Models;

namespace ConsultorioApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Paciente> Pacientes { get; set; }


    }
}
