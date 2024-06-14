using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class Conexion
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}
