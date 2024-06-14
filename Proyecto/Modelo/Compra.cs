using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Modelo
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public string DocumentoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public int CantidadProductos { get; set; }
        public string MontoTotal { get; set; }
        public int Activo { get; set; }
        public List<DetalleCompra> olistaDetalle { get; set; }
    }
}
