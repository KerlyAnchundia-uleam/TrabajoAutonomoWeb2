using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GestionarCompras.Models;
using System.Linq;

namespace GestionarCompras.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProveedorController : ControllerBase
    {
        private List<Proveedor> _proveedores = new List<Proveedor>();

        // GET: /Proveedor
        [HttpGet]
        public IEnumerable<Proveedor> ListarProveedores()
        {
            return _proveedores;
        }

        // GET: /Proveedor/{id}
        [HttpGet("{id}")]
        public ActionResult<Proveedor> DetallesProveedor(int id)
        {
            var proveedor = _proveedores.Find(p => p.IdProveedor == id);
            if (proveedor == null)
            {
                return NotFound(new { message = "No se encontró el proveedor con el ID proporcionado." });
            }
            return Ok(proveedor);
        }

        // POST: /Proveedor
        [HttpPost]
        public ActionResult<Proveedor> CrearProveedor([FromBody] Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return BadRequest();
            }

            // Simulación de asignación de ID único
            proveedor.IdProveedor = _proveedores.Count + 1; // Suponiendo que estás generando ID numéricos secuenciales

            _proveedores.Add(proveedor);

            return CreatedAtAction(nameof(DetallesProveedor), new { id = proveedor.IdProveedor }, proveedor);
        }

        // PUT: /Proveedor/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarProveedor(int id, [FromBody] Proveedor proveedor)
        {
            var proveedorExistente = _proveedores.Find(p => p.IdProveedor == id);
            if (proveedorExistente == null)
            {
                return NotFound();
            }

            // Actualizar los campos relevantes
            proveedorExistente.Nombre = proveedor.Nombre;
            proveedorExistente.Direccion = proveedor.Direccion;
            proveedorExistente.Telefono = proveedor.Telefono;
            proveedorExistente.Contacto = proveedor.Contacto;

            return NoContent();
        }

        // DELETE: /Proveedor/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarProveedor(int id)
        {
            var proveedor = _proveedores.Find(p => p.IdProveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _proveedores.Remove(proveedor);

            return NoContent();
        }
    }
}
