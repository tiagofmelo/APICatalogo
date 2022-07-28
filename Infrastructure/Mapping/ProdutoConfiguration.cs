using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class ProdutoConfiguration
    {
        public ProdutoConfiguration(EntityTypeBuilder<Produto> entity)
        {
            entity.HasKey(p => p.ProdutoId);
            entity.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Descricao).HasMaxLength(150);
            entity.Property(p => p.Preco).HasPrecision(14,2);
            entity.Property(p => p.ImagemUrl).HasMaxLength(100);
            entity.Property(p => p.Estoque);
            entity.Property(p => p.DataCadastro);

            entity.HasOne<Categoria>(c => c.Categoria)
                .WithMany(p => p.Produtos)
                .HasForeignKey(c => c.CategoriaId);
        }
    }
}
