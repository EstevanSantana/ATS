using ATS.BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATS.BackEnd.Infrastructure.Data.Mapping
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Rua)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(e => e.Numero)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.Rua)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(e => e.Bairro)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(e => e.Cidade)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(e => e.Estado)
                   .HasMaxLength(2)
                   .IsRequired();
        }
    }
}
