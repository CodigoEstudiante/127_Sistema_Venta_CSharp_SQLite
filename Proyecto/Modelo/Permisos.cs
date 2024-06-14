using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Modelo
{
    public class Permisos
    {
        public int IdPermisos { get; set; }
        public string Descripcion { get; set; }
        public int Ventas { get; set; }
        public int Compras { get; set; }
        public int Productos { get; set; }
        public int Clientes { get; set; }
        public int Proveedores { get; set; }
        public int Mantenimiento { get; set; }
    }
}
