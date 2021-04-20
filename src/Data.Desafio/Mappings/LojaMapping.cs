using Business.Desafio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Desafio.Mappings
{
    public class LojaMapping : IEntityTypeConfiguration<Loja>
    {
        public void Configure(EntityTypeBuilder<Loja> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Nome)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(l => l.NomeFantasia)
                .HasColumnType("varchar(255)");

            builder.Property(l => l.Cnpj)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(l => l.Cep)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(l => l.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(l => l.Numero)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(l => l.Complemento)
                .HasColumnType("varchar(30)");

            builder.Property(l => l.Bairro)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(l => l.Cidade)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(l => l.Uf)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.HasMany(l => l.EstoqueList)
                .WithOne(e => e.Loja)
                .HasForeignKey(e => e.LojaId);

            builder.ToTable("Lojas");
        }
    }

}