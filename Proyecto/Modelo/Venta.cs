using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Modelo
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public int CantidadProductos { get; set; }
        public string MontoTotal { get; set; }
        public string PagoCon { get; set; }
        public string Cambio { get; set; }
        public int Activo { get; set; }
        public List<DetalleVenta> olistaDetalle { get; set; }

    }
}
