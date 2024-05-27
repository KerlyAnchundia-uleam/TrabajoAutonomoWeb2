using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NeonTechAspNetCore.Models
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
    }

    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Marca { get; set; }

        // Clave foránea
        public int IdCategoria { get; set; }

        // Relación con la categoría
       public Categoria Categoria { get; set; }
    }




    public class Categoria
    {
        public int IdCategoria { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        // Propiedad de navegación
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }



    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime Fecha { get; set; }

        // Clave foránea
        public int IdProveedor { get; set; }

        // Propiedad de navegación
        public Proveedor Proveedor { get; set; }
        [JsonIgnore]
        public ICollection<DetalleCompra> DetallesCompra { get; set; }
    }

    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }

        // Claves foráneas
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }

        // Propiedades adicionales
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }

        // Propiedades de navegación
        public Compra Compra { get; set; }
        public Producto Producto { get; set; }
    }

    public class Lote
    {
        public int IdLote { get; set; }
        public DateTime Fecha { get; set; }

        // Clave foránea
        public int IdProducto { get; set; }

        // Propiedad de navegación
        public Producto Producto { get; set; }
    }

    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // En una aplicación real, nunca almacenes contraseñas en texto plano.
    }
}
