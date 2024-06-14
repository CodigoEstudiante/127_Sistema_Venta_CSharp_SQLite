using Proyecto.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class ProductoLogica
    {
        private static ProductoLogica _instancia = null;

        public ProductoLogica()
        {

        }

        public static ProductoLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new ProductoLogica();
                return _instancia;
            }
        }


        public List<Producto> Listar(out string mensaje)
        {
            mensaje = string.Empty;
            List<Producto> oLista = new List<Producto>();

            try
            {

                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string query = "select IdProducto,Codigo,Descripcion,Categoria,Medida,Stock,PrecioVenta,PrecioCompra from PRODUCTO;";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Producto()
                            {
                                IdProducto = int.Parse(dr["IdProducto"].ToString()),
                                Codigo = dr["Codigo"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                Medida = dr["Medida"].ToString(),
                                Stock = Convert.ToInt32(dr["Stock"].ToString()),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                PrecioCompra = dr["PrecioCompra"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Producto>();
                mensaje = ex.Message;
            }


            return oLista;
        }

        public int Existe(string codigo, int defaultid, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*)[resultado] from PRODUCTO where upper(Codigo) = upper(@pcodigo) and IdProducto != @defaultid");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pcodigo", codigo));
                    cmd.Parameters.Add(new SQLiteParameter("@defaultid", defaultid));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta > 0)
                        mensaje = "El codigo de producto ya existe";

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }

            }
            return respuesta;
        }

        public int Guardar(Producto objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            
            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("insert into PRODUCTO(Codigo,Descripcion,Categoria,Medida) values (@pcodigo,@pdescripcion,@pcategoria,@pmedida);");
                    query.AppendLine("select last_insert_rowid();");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pcodigo", objeto.Codigo));
                    cmd.Parameters.Add(new SQLiteParameter("@pdescripcion", objeto.Descripcion));
                    cmd.Parameters.Add(new SQLiteParameter("@pcategoria", objeto.Categoria));
                    cmd.Parameters.Add(new SQLiteParameter("@pmedida", objeto.Medida));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta < 1)
                        mensaje = "No se pudo registrar el producto";
                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }

            return respuesta;
        }

        public int Editar(Producto objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("update PRODUCTO set Codigo = @pcodigo,Descripcion = @pdescripcion,Categoria =@pcategoria ,Medida = @pmedida where IdProducto = @pidproducto");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pidproducto", objeto.IdProducto));
                    cmd.Parameters.Add(new SQLiteParameter("@pcodigo", objeto.Codigo));
                    cmd.Parameters.Add(new SQLiteParameter("@pdescripcion", objeto.Descripcion));
                    cmd.Parameters.Add(new SQLiteParameter("@pcategoria", objeto.Categoria));
                    cmd.Parameters.Add(new SQLiteParameter("@pmedida", objeto.Medida));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo editar el producto";
                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }

            return respuesta;
        }


        public int Eliminar(int id)
        {
            int respuesta = 0;
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from PRODUCTO where IdProducto= @id;");
                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@id", id));
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
            }

            return respuesta;
        }




    }
}
