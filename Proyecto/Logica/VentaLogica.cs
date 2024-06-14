using Proyecto.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class VentaLogica
    {

        private static VentaLogica _instancia = null;

        public VentaLogica()
        {

        }

        public static VentaLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new VentaLogica();
                return _instancia;
            }
        }


        public int reducirStock(int idproducto,int cantidad, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set Stock = Stock - @pcantidad where IdProducto = @pidproducto");
                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pcantidad", cantidad));
                    cmd.Parameters.Add(new SQLiteParameter("@pidproducto", idproducto));
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1) {
                        mensaje = "no se puede reducir stock";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public int aumentarStock(int idproducto, int cantidad, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set Stock = Stock + @pcantidad where IdProducto = @pidproducto");
                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pcantidad", cantidad));
                    cmd.Parameters.Add(new SQLiteParameter("@pidproducto", idproducto));
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                    {
                        mensaje = "no se puede aumentar stock";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                mensaje = ex.Message;
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
                    query.AppendLine("select count(*) + 1 from Venta");
                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    if (respuesta < 1) {
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


        public int Registrar(Venta obj, out string mensaje)
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
                    query.AppendLine(string.Format("Insert into Venta(NumeroDocumento,FechaRegistro,UsuarioRegistro,DocumentoCliente,NombreCliente,CantidadProductos,MontoTotal,PagoCon,Cambio) values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}');",
                        obj.NumeroDocumento,
                        obj.FechaRegistro,
                        obj.UsuarioRegistro,
                        obj.DocumentoCliente,
                        obj.NombreCliente,
                        obj.CantidadProductos,
                        obj.MontoTotal,
                        obj.PagoCon,
                        obj.Cambio));

                    query.AppendLine("INSERT INTO _TEMP (id) VALUES (last_insert_rowid());");

                    foreach (DetalleVenta de in obj.olistaDetalle)
                    {
                        query.AppendLine(string.Format("insert into DETALLE_VENTA(IdVenta,IdProducto,CodigoProducto,DescripcionProducto,CategoriaProducto,MedidaProducto,PrecioVenta,Cantidad,SubTotal) values({0},{1},'{2}','{3}','{4}','{5}','{6}',{7},'{8}');",
                            "(select id from _TEMP)",
                            de.IdProducto,
                            de.CodigoProducto,
                            de.DescripcionProducto,
                            de.CategoriaProducto,
                            de.MedidaProducto,
                            de.PrecioVenta,
                            de.Cantidad,
                            de.SubTotal
                            ));

                    }

                    query.AppendLine("DROP TABLE _TEMP;");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Transaction = objTransaccion;
                    respuesta = cmd.ExecuteNonQuery();


                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo registrar la Venta de los productos";
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

        public List<VistaVenta> Resumen(string fechainicio = "", string fechafin = "")
        {
            List<VistaVenta> oLista = new List<VistaVenta>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
               
                    query.AppendLine("select e.NumeroDocumento,strftime('%d/%m/%Y', date(e.FechaRegistro))[FechaRegistro],e.UsuarioRegistro,");
                    query.AppendLine("e.DocumentoCliente,e.NombreCliente,e.MontoTotal,e.PagoCon,e.Cambio,");
                    query.AppendLine("de.CodigoProducto,de.DescripcionProducto,de.CategoriaProducto,de.MedidaProducto,");
                    query.AppendLine("de.PrecioVenta,de.Cantidad,de.SubTotal");
                    query.AppendLine("from Venta e");
                    query.AppendLine("inner join DETALLE_Venta de on e.IdVenta = de.IdVenta");
                    query.AppendLine("where DATE(e.FechaRegistro) BETWEEN DATE(@pfechainicio) AND DATE(@pfechafin) and e.Activo = 1");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pfechainicio", fechainicio));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechafin", fechafin));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new VistaVenta()
                            {
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                PagoCon = dr["PagoCon"].ToString(),
                                Cambio = dr["Cambio"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                DescripcionProducto = dr["DescripcionProducto"].ToString(),
                                CategoriaProducto = dr["CategoriaProducto"].ToString(),
                                MediadProducto = dr["MedidaProducto"].ToString(),
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
                oLista = new List<VistaVenta>();
            }
            return oLista;
        }


        public Venta Obtener(string numerodocumento)
        {
            Venta objeto = null;

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdVenta,NumeroDocumento, strftime('%d/%m/%Y', date(FechaRegistro))[FechaRegistro],UsuarioRegistro,DocumentoCliente,");
                    query.AppendLine("NombreCliente,CantidadProductos,MontoTotal,PagoCon,Cambio,Activo from Venta");
                    query.AppendLine("where NumeroDocumento = @pnumero");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pnumero", numerodocumento));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"].ToString()),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                CantidadProductos = Convert.ToInt32(dr["CantidadProductos"].ToString()),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                PagoCon = dr["PagoCon"].ToString(),
                                Cambio = dr["Cambio"].ToString(),
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

        public List<DetalleVenta> ListarDetalle(int idVenta)
        {
            List<DetalleVenta> oLista = new List<DetalleVenta>();

            try
            {

                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProducto, CodigoProducto, DescripcionProducto, CategoriaProducto,");
                    query.AppendLine("MedidaProducto, PrecioVenta, Cantidad, SubTotal");
                    query.AppendLine("from DETALLE_Venta where IdVenta = @pidVenta");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pidVenta", idVenta));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new DetalleVenta()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                DescripcionProducto = dr["DescripcionProducto"].ToString(),
                                CategoriaProducto = dr["CategoriaProducto"].ToString(),
                                MedidaProducto = dr["MedidaProducto"].ToString(),
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
                oLista = new List<DetalleVenta>();
            }


            return oLista;
        }
        public int cancelar_Venta(int id, List<DetalleVenta> odetalle, out string mensaje)
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

                    query.AppendLine($"update VENTA set Activo = 0 where IdVenta = {id};");
                    foreach (DetalleVenta item in odetalle)
                    {
                        query.AppendLine(string.Format("update Producto set Stock = Stock + {0} where IdProducto = {1};", item.Cantidad, item.IdProducto));
                    }

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Transaction = objTransaccion;
                    respuesta = cmd.ExecuteNonQuery();

                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo cancelar la venta";
                    }

                    objTransaccion.Commit();
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                objTransaccion.Rollback();
                mensaje = "No se pudo cancelar la venta\nMayor Detalle:\n" + ex.Message;
            }

            return respuesta;
        }





    }
}
