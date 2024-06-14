using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Modelo
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string Medida { get; set; }
        public int Stock { get; set; }
        public string PrecioCompra { get; set; }
        public string PrecioVenta { get; set; }
    }
}
