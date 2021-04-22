using Business.Desafio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Desafio.Mappings
{
    public class EstoqueMapping : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Estoques");
        }
    }
}