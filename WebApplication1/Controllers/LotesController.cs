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
public class LotesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LotesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Lotes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lote>>> GetLotes()
    {
        return await _context.Lotes
            .Include(l => l.Producto)
            .ToListAsync();
    }

    // GET: api/Lotes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Lote>> GetLote(int id)
    {
        var lote = await _context.Lotes
            .Include(l => l.Producto)
            .FirstOrDefaultAsync(l => l.IdLote == id);

        if (lote == null)
        {
            return NotFound();
        }

        return lote;
    }

    // PUT: api/Lotes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLote(int id, Lote lote)
    {
        if (id != lote.IdLote)
        {
            return BadRequest();
        }

        _context.Entry(lote).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LoteExists(id))
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

    // POST: api/Lotes
    [HttpPost]
    public async Task<ActionResult<Lote>> PostLote(Lote lote)
    {
        // Desactivar la validación para la propiedad 'Producto'
        ModelState.Remove("Producto");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Lotes.Add(lote);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLote), new { id = lote.IdLote }, lote);
    }

    // DELETE: api/Lotes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLote(int id)
    {
        var lote = await _context.Lotes.FindAsync(id);
        if (lote == null)
        {
            return NotFound();
        }

        _context.Lotes.Remove(lote);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LoteExists(int id)
    {
        return _context.Lotes.Any(e => e.IdLote == id);
    }
}
