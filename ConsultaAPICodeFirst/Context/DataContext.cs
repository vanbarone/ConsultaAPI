using ConsultaAPICodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsultaAPICodeFirst.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TipoUsuario> TipoUsuario { get; set; }

        public DbSet<Especialidade> Especialidade { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Medico> Medico { get; set; }

        public DbSet<Paciente> Paciente { get; set; }

        public DbSet<Consulta> Consulta { get; set; }

    }
}
