using Proyecto.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class CompraLogica
    {

        private static CompraLogica _instancia = null;

        public CompraLogica()
        {

        }

        public static CompraLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new CompraLogica();
                return _instancia;
            }
        }


        public int Existe(string numerodocumento, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*)[resultado] from Compra where upper(NumeroDocumento) = upper(@pnumero)");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pnumero", numerodocumento));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta > 0)
                        mensaje = "El numero de documento ya existe";

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }

            }
            return respuesta;
        }

        public int ObtenerCorrelativo(out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from COMPRA");
                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    if (respuesta < 1)
                    {
                        mensaje = "No se pudo generar el correlativo";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                mensaje = "No se pudo generar el correlativo\nMayor Detalle:\n" + ex.Message;
            }

            return respuesta;
        }

        public int Registrar(Compra obj, out string mensaje)
        {

            mensaje = string.Empty;
            int respuesta = 0;
            SQLiteTransaction objTransaccion = null;

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    objTransaccion = conexion.BeginTransaction();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("CREATE TEMP TABLE _TEMP(id INTEGER);");
                    query.AppendLine(string.Format("Insert into Compra(NumeroDocumento,FechaRegistro,UsuarioRegistro,DocumentoProveedor,NombreProveedor,CantidadProductos,MontoTotal) values('{0}','{1}','{2}','{3}','{4}',{5},'{6}');",
                        obj.NumeroDocumento,
                        obj.FechaRegistro,
                        obj.UsuarioRegistro,
                        obj.DocumentoProveedor,
                        obj.NombreProveedor,
                        obj.CantidadProductos,
                        obj.MontoTotal));

                    query.AppendLine("INSERT INTO _TEMP (id) VALUES (last_insert_rowid());");

                    foreach (DetalleCompra de in obj.olistaDetalle)
                    {
                        query.AppendLine(string.Format("insert into DETALLE_COMPRA(IdCompra,IdProducto,CodigoProducto,DescripcionProducto,CategoriaProducto,MedidaProducto,PrecioCompra,PrecioVenta,Cantidad,SubTotal) values({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}');",
                            "(select id from _TEMP)",
                            de.IdProducto,
                            de.CodigoProducto,
                            de.DescripcionProducto,
                            de.CategoriaProducto,
                            de.MedidaProducto,
                            de.PrecioCompra,
                            de.PrecioVenta,
                            de.Cantidad,
                            de.SubTotal
                            ));

                        query.AppendLine(string.Format("UPDATE PRODUCTO set PrecioCompra = '{0}', PrecioVenta = '{1}', Stock = (Stock + {2}) where IdProducto = {3};",de.PrecioCompra,de.PrecioVenta,de.Cantidad,de.IdProducto));
                    }

                    query.AppendLine("DROP TABLE _TEMP;");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Transaction = objTransaccion;
                    respuesta = cmd.ExecuteNonQuery();


                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo registrar la Compra de los productos";
                    }

                    objTransaccion.Commit();

                }
                catch (Exception ex)
                {
                    objTransaccion.Rollback();
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }


            return respuesta;
        }

        public List<VistaCompra> Resumen(string fechainicio = "", string fechafin = "")
        {
            List<VistaCompra> oLista = new List<VistaCompra>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select e.NumeroDocumento,strftime('%d/%m/%Y', date(e.FechaRegistro))[FechaRegistro],e.UsuarioRegistro,");
                    query.AppendLine("e.DocumentoProveedor,e.NombreProveedor,e.MontoTotal,");
                    query.AppendLine("de.CodigoProducto,de.DescripcionProducto,de.CategoriaProducto,de.MedidaProducto,de.PrecioCompra,");
                    query.AppendLine("de.PrecioVenta,de.Cantidad,de.SubTotal");
                    query.AppendLine("from Compra e");
                    query.AppendLine("inner join DETALLE_Compra de on e.IdCompra = de.IdCompra");
                    query.AppendLine("where DATE(e.FechaRegistro) BETWEEN DATE(@pfechainicio) AND DATE(@pfechafin) and e.Activo = 1");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pfechainicio", fechainicio));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechafin", fechafin));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new VistaCompra()
                            {
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoProveedor = dr["DocumentoProveedor"].ToString(),
                                NombreProveedor = dr["NombreProveedor"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                DescripcionProducto = dr["DescripcionProducto"].ToString(),
                                CategoriaProducto = dr["CategoriaProducto"].ToString(),
                                MedidaProducto = dr["MedidaProducto"].ToString(),
                                PrecioCompra = dr["PrecioCompra"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<VistaCompra>();
            }
            return oLista;
        }


        public Compra Obtener(string numerodocumento)
        {
            Compra objeto = null;

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCompra,NumeroDocumento, strftime('%d/%m/%Y', date(FechaRegistro))[FechaRegistro],UsuarioRegistro,DocumentoProveedor,");
                    query.AppendLine("NombreProveedor,CantidadProductos,MontoTotal,Activo from Compra");
                    query.AppendLine("where NumeroDocumento = @pnumero");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pnumero", numerodocumento));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Compra()
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"].ToString()),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoProveedor = dr["DocumentoProveedor"].ToString(),
                                NombreProveedor = dr["NombreProveedor"].ToString(),
                                CantidadProductos = Convert.ToInt32(dr["CantidadProductos"].ToString()),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                Activo = Convert.ToInt32(dr["Activo"].ToString())
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objeto = null;
            }
            return objeto;
        }

        public List<DetalleCompra> ListarDetalle(int idCompra)
        {
            List<DetalleCompra> oLista = new List<DetalleCompra>();

            try
            {

                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProducto, CodigoProducto, DescripcionProducto, CategoriaProducto,");
                    query.AppendLine("MedidaProducto, PrecioCompra, PrecioVenta, Cantidad, SubTotal");
                    query.AppendLine("from DETALLE_COMPRA where IdCompra = @pidCompra");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pidCompra", idCompra));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new DetalleCompra()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                DescripcionProducto = dr["DescripcionProducto"].ToString(),
                                CategoriaProducto = dr["CategoriaProducto"].ToString(),
                                MedidaProducto = dr["MedidaProducto"].ToString(),
                                PrecioCompra = dr["PrecioCompra"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                SubTotal = dr["SubTotal"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<DetalleCompra>();
            }


            return oLista;
        }

        public int cancelar_Compra(int id, List<DetalleCompra> odetalle, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            SQLiteTransaction objTransaccion = null;

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    objTransaccion = conexion.BeginTransaction();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine($"update Compra set Activo = 0 where IdCompra = {id};");
                    foreach (DetalleCompra item in odetalle)
                    {
                        query.AppendLine(string.Format("update Producto set Stock = Stock - {0} where IdProducto = {1};", item.Cantidad, item.IdProducto));
                    }

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Transaction = objTransaccion;
                    respuesta = cmd.ExecuteNonQuery();

                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo cancelar la compra";
                    }

                    objTransaccion.Commit();
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                objTransaccion.Rollback();
                mensaje = "No se pudo cancelar la compra\nMayor Detalle:\n" + ex.Message;
            }

            return respuesta;
        }






    }
}
