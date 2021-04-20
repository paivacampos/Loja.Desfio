using Business.Desafio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Desafio.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.HasMany(p => p.EstoqueList)
                .WithOne(e => e.Produto)
                .HasForeignKey(e => e.ProdutoId);

            builder.ToTable("Produtos");
        }
    }
}