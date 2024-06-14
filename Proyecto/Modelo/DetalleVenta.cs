using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Modelo
{
    public class DetalleVenta
    {

        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string CategoriaProducto { get; set; }
        public string MedidaProducto { get; set; }
        public string PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public string SubTotal { get; set; }
    }
}
