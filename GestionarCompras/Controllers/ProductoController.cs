using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GestionarCompras.Models;
using System.Linq;

namespace GestionarCompras.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private List<Producto> _productos = new List<Producto>();

        // GET: /Producto
        [HttpGet]
        public IEnumerable<Producto> ListarProductos()
        {
            return _productos;
        }

        // GET: /Producto/{id}
        [HttpGet("{id}")]
        public ActionResult<Producto> DetallesProducto(int id)
        {
            var producto = _productos.Find(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound(new { message = "No se encontró el producto con el ID proporcionado." });
            }
            return Ok(producto);
        }

        // POST: /Producto
        [HttpPost]
        public ActionResult<Producto> CrearProducto([FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest();
            }

            // Simulación de asignación de ID único
            producto.IdProducto = _productos.Count + 1; // Suponiendo que estás generando ID numéricos secuenciales

            _productos.Add(producto);

            return CreatedAtAction(nameof(DetallesProducto), new { id = producto.IdProducto }, producto);
        }

        // PUT: /Producto/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(int id, [FromBody] Producto producto)
        {
            var productoExistente = _productos.Find(p => p.IdProducto == id);
            if (productoExistente == null)
            {
                return NotFound();
            }

            // Actualizar los campos relevantes
            productoExistente.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.PrecioCompra = producto.PrecioCompra;
            productoExistente.PrecioVenta = producto.PrecioVenta;
            productoExistente.Marca = producto.Marca;
            productoExistente.IdCategoria = producto.IdCategoria;

            return NoContent();
        }

        // DELETE: /Producto/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            var producto = _productos.Find(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            _productos.Remove(producto);

            return NoContent();
        }
    }
}
