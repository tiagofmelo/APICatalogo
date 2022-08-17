using Domain.Models;
using System.Collections.Generic;

namespace Infrastructure.Repository.Interface
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosPorPreco();
    }
}
