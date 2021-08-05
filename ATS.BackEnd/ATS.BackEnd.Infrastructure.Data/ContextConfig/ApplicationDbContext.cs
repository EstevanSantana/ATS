using ATS.BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ATS.BackEnd.Infrastructure.Data.ContextConfig
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Curriculo> Curriculos { get; set; }
        public DbSet<Vagas> Vagas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
