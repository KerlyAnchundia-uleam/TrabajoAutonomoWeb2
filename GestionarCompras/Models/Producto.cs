using GestionarCompras.Models.GestionarCompras.Models;

namespace GestionarCompras.Models
{
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

        // Propiedad de navegación
        public Categoria Categoria { get; set; }
    }
}
