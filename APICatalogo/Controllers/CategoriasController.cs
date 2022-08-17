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
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public CategoriasController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            if (_context.CategoriaRepository == null)
            {
                return NotFound();
            }
            return await _context.CategoriaRepository.Get().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            if (_context.CategoriaRepository == null)
            {
                return NotFound();
            }
            var categoria = await _context.CategoriaRepository.GetById(c => c.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.CategoriaRepository.Update(categoria);
            await _context.Commit();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            if (_context.CategoriaRepository == null)
            {
                return Problem("Entity set 'AppDbContext.Categorias'  is null.");
            }
            _context.CategoriaRepository.Add(categoria);
            await _context.Commit();

            return CreatedAtAction("GetCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            if (_context.CategoriaRepository == null)
            {
                return NotFound();
            }
            var categoria = await _context.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.CategoriaRepository.Delete(categoria);
            await _context.Commit();

            return NoContent();
        }
    }
}
