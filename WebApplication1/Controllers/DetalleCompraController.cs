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
public class DetallesCompraController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DetallesCompraController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/DetallesCompra
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleCompra>>> GetDetallesCompra()
    {
        return await _context.DetallesCompra
            .Include(dc => dc.Compra)
            .Include(dc => dc.Producto)
            .ToListAsync();
    }

    // GET: api/DetallesCompra/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleCompra>> GetDetalleCompra(int id)
    {
        var detalleCompra = await _context.DetallesCompra
            .Include(dc => dc.Compra)
            .Include(dc => dc.Producto)
            .FirstOrDefaultAsync(dc => dc.IdDetalleCompra == id);

        if (detalleCompra == null)
        {
            return NotFound();
        }

        return detalleCompra;
    }

    // PUT: api/DetallesCompra/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDetalleCompra(int id, DetalleCompra detalleCompra)
    {
        if (id != detalleCompra.IdDetalleCompra)
        {
            return BadRequest();
        }

        _context.Entry(detalleCompra).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DetalleCompraExists(id))
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

    // POST: api/DetallesCompra
    [HttpPost]
    public async Task<ActionResult<DetalleCompra>> PostDetalleCompra(DetalleCompra detalleCompra)
    {
        _context.DetallesCompra.Add(detalleCompra);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDetalleCompra), new { id = detalleCompra.IdDetalleCompra }, detalleCompra);
    }

    // DELETE: api/DetallesCompra/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDetalleCompra(int id)
    {
        var detalleCompra = await _context.DetallesCompra.FindAsync(id);
        if (detalleCompra == null)
        {
            return NotFound();
        }

        _context.DetallesCompra.Remove(detalleCompra);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DetalleCompraExists(int id)
    {
        return _context.DetallesCompra.Any(e => e.IdDetalleCompra == id);
    }
}
