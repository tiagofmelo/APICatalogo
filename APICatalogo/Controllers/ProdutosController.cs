using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Repository.Interface;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public ProdutosController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            if (_context.ProdutoRepository == null)
            {
                return NotFound();
            }
            return await _context.ProdutoRepository.Get().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            if (_context.ProdutoRepository == null)
            {
                return NotFound();
            }
            var produto = await _context.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.ProdutoRepository.Update(produto);
            await _context.Commit();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            if (_context.ProdutoRepository == null)
            {
                return Problem("Entity set 'AppDbContext.Produtos'  is null.");
            }
            _context.ProdutoRepository.Add(produto);
            await _context.Commit();

            return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            if (_context.ProdutoRepository == null)
            {
                return NotFound();
            }
            var produto = await _context.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.ProdutoRepository.Delete(produto);
            await _context.Commit();

            return NoContent();
        }
    }
}
