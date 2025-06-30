using Microsoft.EntityFrameworkCore;
using SaludToTal_APIRest.Models;


namespace SaludTotal_APIRest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RecuperarContra> RecuperarContra { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



        }
    }
}