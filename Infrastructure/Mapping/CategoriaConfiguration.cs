using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class CategoriaConfiguration
    {
        public CategoriaConfiguration(EntityTypeBuilder<Categoria> entity)
        {
            entity.HasKey(c => c.CategoriaId);
            entity.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            entity.Property(c => c.ImagemUrl).HasMaxLength(150).IsRequired();
        }
    }
}
