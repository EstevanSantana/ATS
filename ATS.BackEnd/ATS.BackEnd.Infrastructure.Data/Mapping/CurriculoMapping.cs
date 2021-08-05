using ATS.BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATS.BackEnd.Infrastructure.Data.Mapping
{
    public class CurriculoMapping : IEntityTypeConfiguration<Curriculo>
    {
        public void Configure(EntityTypeBuilder<Curriculo> builder)
        {
            builder.ToTable("Curriculos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Faculdade)
                   .HasMaxLength(50)
                   .IsRequired();
        
            builder.Property(c => c.Curso)
                   .HasMaxLength(50)
                   .IsRequired();
            
            builder.Property(c => c.Conclusao)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(c => c.CurriculoImagem)
                   .HasMaxLength(100)
                   .IsRequired();
            
        }
    }
}
