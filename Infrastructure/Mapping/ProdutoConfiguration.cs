using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class ProdutoConfiguration
    {
        public ProdutoConfiguration(EntityTypeBuilder<Produto> entity)
        {
            entity.HasKey(c => c.ProdutoId);
            entity.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            entity.Property(c => c.Descricao).HasMaxLength(150);
            entity.Property(c => c.ImagemUrl).HasMaxLength(100);
            entity.Property(c => c.Preco).HasPrecision(14, 2);

            entity.HasOne<Categoria>(c => c.Categoria)
                  .WithMany(p => p.Produtos)
                  .HasForeignKey(c => c.CategoriaId);
        }
    }
}
