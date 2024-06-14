using Proyecto.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class PermisosLogica
    {

        private static PermisosLogica _instancia = null;

        public PermisosLogica()
        {

        }

        public static PermisosLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new PermisosLogica();
                return _instancia;
            }
        }



        public Permisos Obtener(int idpermisos)
        {
            Permisos obj = new Permisos();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select IdPermisos,Descripcion,Ventas,Compras,Productos,Clientes,Proveedores,Mantenimiento from PERMISOS where IdPermisos = @pid";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pid", idpermisos));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Permisos()
                            {
                                IdPermisos = int.Parse(dr["IdPermisos"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                                Ventas = int.Parse(dr["Ventas"].ToString()),
                                Compras = int.Parse(dr["Compras"].ToString()),
                                Productos = int.Parse(dr["Productos"].ToString()),
                                Clientes = int.Parse(dr["Clientes"].ToString()),
                                Proveedores = int.Parse(dr["Proveedores"].ToString()),
                                Mantenimiento = int.Parse(dr["Mantenimiento"].ToString())
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj = new Permisos();
            }
            return obj;
        }

        public int Guardar(Permisos objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {

                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PERMISOS SET");
                    query.AppendLine("Ventas = @pventa,");
                    query.AppendLine("Compras = @pcompra,");
                    query.AppendLine("Productos = @pproducto,");
                    query.AppendLine("Clientes = @pcliente,");
                    query.AppendLine("Proveedores = @pproveedor,");
                    query.AppendLine("Mantenimiento = @pmantenimiento");
                    query.AppendLine("where IdPermisos = @pid;");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pventa", objeto.Ventas));
                    cmd.Parameters.Add(new SQLiteParameter("@pcompra", objeto.Compras));
                    cmd.Parameters.Add(new SQLiteParameter("@pproducto", objeto.Productos));
                    cmd.Parameters.Add(new SQLiteParameter("@pcliente", objeto.Clientes));
                    cmd.Parameters.Add(new SQLiteParameter("@pproveedor", objeto.Proveedores));
                    cmd.Parameters.Add(new SQLiteParameter("@pmantenimiento", objeto.Mantenimiento));
                    cmd.Parameters.Add(new SQLiteParameter("@pid", objeto.IdPermisos));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo actualizar los permisos";

                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }

    }
}
