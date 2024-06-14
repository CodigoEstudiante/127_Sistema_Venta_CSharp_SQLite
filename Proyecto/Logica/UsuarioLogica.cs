using Proyecto.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class UsuarioLogica
    {
        private static UsuarioLogica _instancia = null;

        public UsuarioLogica()
        {

        }

        public static UsuarioLogica Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new UsuarioLogica();
                return _instancia;
            }
        }

        public int resetear()
        {
            int respuesta = 0;
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update USUARIO set NombreUsuario = 'Admin', Clave = '123' where IdUsuario = 1;");
                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
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


        public List<Usuario> Listar(out string mensaje)
        {
            mensaje = string.Empty;
            List<Usuario> oLista = new List<Usuario>();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    

                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario,u.NombreCompleto,u.NombreUsuario,u.Clave,u.IdPermisos,p.Descripcion from USUARIO u");
                    query.AppendLine("inner join PERMISOS p on p.IdPermisos = u.IdPermisos;");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Usuario()
                            {
                                IdUsuario = int.Parse(dr["IdUsuario"].ToString()),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                IdPermisos = Convert.ToInt32(dr["IdPermisos"].ToString()),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Usuario>();
                mensaje = ex.Message;
            }
            return oLista;
        }

        public int Existe(string usuario, int defaultid, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*)[resultado] from USUARIO where upper(NombreUsuario) = upper(@pnombreusuario) and IdUsuario != @defaultid");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pnombreusuario", usuario));
                    cmd.Parameters.Add(new SQLiteParameter("@defaultid", defaultid));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta > 0)
                        mensaje = "El usuario ya existe";

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }

            }
            return respuesta;
        }

        public int Guardar(Usuario objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("insert into USUARIO(NombreCompleto,NombreUsuario,Clave,IdPermisos) values (@pnombrecompleto,@pnombreusuario,@pclave,@pidpermisos);");
                    query.AppendLine("select last_insert_rowid();");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pnombrecompleto", objeto.NombreCompleto));
                    cmd.Parameters.Add(new SQLiteParameter("@pnombreusuario", objeto.NombreUsuario));
                    cmd.Parameters.Add(new SQLiteParameter("@pclave", objeto.Clave));
                    cmd.Parameters.Add(new SQLiteParameter("@pidpermisos", objeto.IdPermisos));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta < 1)
                        mensaje = "No se pudo registrar el usuario";
                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }

            return respuesta;
        }

        public int Editar(Usuario objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update USUARIO set NombreCompleto = @pnombrecompleto,NombreUsuario = @pnombreusuario,Clave = @pclave,IdPermisos = @pidpermisos  where IdUsuario = @pid");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pnombrecompleto", objeto.NombreCompleto));
                    cmd.Parameters.Add(new SQLiteParameter("@pnombreusuario", objeto.NombreUsuario));
                    cmd.Parameters.Add(new SQLiteParameter("@pclave", objeto.Clave));
                    cmd.Parameters.Add(new SQLiteParameter("@pidpermisos", objeto.IdPermisos));
                    cmd.Parameters.Add(new SQLiteParameter("@pid", objeto.IdUsuario));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo editar el usuario";
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
                    query.AppendLine("delete from USUARIO where IdUsuario= @id;");
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
