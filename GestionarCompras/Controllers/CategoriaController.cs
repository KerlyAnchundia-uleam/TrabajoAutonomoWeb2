using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GestionarCompras.Models;
using System.Linq;
using GestionarCompras.Models.GestionarCompras.Models;

namespace GestionarCompras.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private List<Categoria> _categorias = new List<Categoria>();

        // GET: /Categoria
        [HttpGet]
        public IEnumerable<Categoria> ListarCategorias()
        {
            return _categorias;
        }

        // GET: /Categoria/{id}
        [HttpGet("{id}")]
        public ActionResult<Categoria> DetallesCategoria(int id)
        {
            var categoria = _categorias.Find(c => c.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound(new { message = "No se encontró la categoría con el ID proporcionado." });
            }
            return Ok(categoria);
        }

        // POST: /Categoria
        [HttpPost]
        public ActionResult<Categoria> CrearCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest();
            }

            // Simulación de asignación de ID único
            categoria.IdCategoria = _categorias.Count + 1; // Suponiendo que estás generando ID numéricos secuenciales

            _categorias.Add(categoria);

            return CreatedAtAction(nameof(DetallesCategoria), new { id = categoria.IdCategoria }, categoria);
        }

        // PUT: /Categoria/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarCategoria(int id, [FromBody] Categoria categoria)
        {
            var categoriaExistente = _categorias.Find(c => c.IdCategoria == id);
            if (categoriaExistente == null)
            {
                return NotFound();
            }

            // Actualizar los campos relevantes
            categoriaExistente.Nombre = categoria.Nombre;
            categoriaExistente.Descripcion = categoria.Descripcion;

            return NoContent();
        }

        // DELETE: /Categoria/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarCategoria(int id)
        {
            var categoria = _categorias.Find(c => c.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            _categorias.Remove(categoria);

            return NoContent();
        }
    }
}
