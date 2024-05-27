using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeonTechAspNetCore.Data;
using NeonTechAspNetCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ComprasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ComprasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Compras
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
    {
        return await _context.Compras
            .Include(c => c.Proveedor)
            .Include(c => c.DetallesCompra)
            .ToListAsync();
    }

    // GET: api/Compras/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Compra>> GetCompra(int id)
    {
        var compra = await _context.Compras
            .Include(c => c.Proveedor)
            .Include(c => c.DetallesCompra)
            .FirstOrDefaultAsync(c => c.IdCompra == id);

        if (compra == null)
        {
            return NotFound();
        }

        return compra;
    }

    // PUT: api/Compras/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompra(int id, Compra compra)
    {
        if (id != compra.IdCompra)
        {
            return BadRequest();
        }

        _context.Entry(compra).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CompraExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Compras
    [HttpPost]
    public async Task<ActionResult<Compra>> PostCompra(Compra compra)
    {
        _context.Compras.Add(compra);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCompra), new { id = compra.IdCompra }, compra);
    }

    // DELETE: api/Compras/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompra(int id)
    {
        var compra = await _context.Compras.FindAsync(id);
        if (compra == null)
        {
            return NotFound();
        }

        _context.Compras.Remove(compra);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CompraExists(int id)
    {
        return _context.Compras.Any(e => e.IdCompra == id);
    }
}
