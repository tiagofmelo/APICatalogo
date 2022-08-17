using Domain.Models;
using Infrastructure.Context;
using Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {

        }

        public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return await Get().OrderBy(c => c.Preco).ToListAsync();
        }
    }
}
