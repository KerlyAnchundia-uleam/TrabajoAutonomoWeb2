using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeonTechAspNetCore.Data;
using NeonTechAspNetCore.Models;



[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProveedoresController : ControllerBase
    {
         private readonly ApplicationDbContext _context;

        public ProveedoresController(ApplicationDbContext context)
        {
            _context = context;
    }
    [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedor()
        {
            return await _context.Proveedores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Proveedor usuario)
        {
            if (id != usuario.IdProveedor)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExist(id))
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

        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostUsuario(Proveedor Proveedor)
        {
            _context.Proveedores.Add(Proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProveedor), new { id = Proveedor.IdProveedor }, Proveedor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExist(int id)
        {
            return _context.Proveedores.Any(e => e.IdProveedor == id);
        }
    }