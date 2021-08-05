using ATS.BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATS.BackEnd.Infrastructure.Data.Mapping
{
    public class CandidatoMapping : IEntityTypeConfiguration<Candidato>
    {
        public void Configure(EntityTypeBuilder<Candidato> builder)
        {
            builder.ToTable("Candidato");

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Endereco)   
                   .WithOne(c => c.Candidato)
                   .OnDelete(DeleteBehavior.Cascade); ;
            
            builder.HasOne(c => c.Curriculo)   
                   .WithOne(c => c.Candidato);

            builder.Property(c => c.Nome)
                   .HasMaxLength(25)
                   .IsRequired();

            builder.Property(c => c.SobreNome)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(c => c.Telefone)
                   .HasMaxLength(20)
                   .IsRequired();
            
            builder.Property(c => c.DataNascimento)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(c => c.Cpf)
                   .HasMaxLength(20)
                   .IsRequired();
        }
    }
}
