using Domain.Models;
using System.Collections.Generic;

namespace Infrastructure.Repository.Interface
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
