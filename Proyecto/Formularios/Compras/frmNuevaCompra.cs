using Proyecto.Formularios.Modales;
using Proyecto.Modelo;
using ProyectoVenta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto.Formularios.Compras
{
    public partial class frmNuevaCompra : Form
    {
        public string _NombreUsuario { get; set; }
        private static Producto _producto = null;
        public frmNuevaCompra(string NombreUsuario)
        {
            _NombreUsuario = NombreUsuario;
            InitializeComponent();
        }

        private void frmNuevaCompra_Load(object sender, EventArgs e)
        {
            txtdocproveedor.Select();
            txtfecharegistro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txttotalpagar.Text = "0.00";
        }

        private void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnbuscarproveedor_Click(object sender, EventArgs e)
        {

            using (var Iform = new mdProveedores())
            {
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Proveedor objeto = Iform._Proveedor;
                    txtdocproveedor.Text = objeto.NumeroDocumento;
                    txtnombreproveedor.Text = objeto.NombreCompleto;
                    txtcodproducto.Select();
                }
                else {
                    txtdocproveedor.Select();
                }
                
            }
        }

        private void txtpreciocompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtpreciocompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtprecioventa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtprecioventa.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var Iform = new mdProductos())
            {
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _producto = Iform._Producto;
                    txtcodproducto.BackColor = Color.Honeydew;
                    txtcodproducto.Text = _producto.Codigo;
                    txtproducto.Text = _producto.Descripcion;
                    txtcantidad.Select();
                }
            }
        }

        private void txtcodproducto_KeyDown(object sender, KeyEventArgs e)
        {
            string mensaje = string.Empty;
            if (e.KeyData == Keys.Enter)
            {
                Producto pr = ProductoLogica.Instancia.Listar(out mensaje).Where(p => p.Codigo.ToUpper() == txtcodproducto.Text.Trim().ToUpper()).FirstOrDefault();
                if (pr != null)
                {
                    txtcodproducto.BackColor = Color.Honeydew;
                    txtcodproducto.Text = pr.Codigo;
                    txtproducto.Text = pr.Descripcion;
                    _producto = pr;txtprecioventa.Text = "";
                    txtpreciocompra.Text = "";
                    txtcantidad.Value = 1;
                    txtcantidad.Select();
                }
                else
                {
                    txtcodproducto.BackColor = Color.MistyRose;
                    _producto = null;
                    txtproducto.Text = "";
                    txtprecioventa.Text = "";
                    txtpreciocompra.Text = "";
                    txtcantidad.Value = 1;
                }

            }
        }

        private bool producto_agregado()
        {

            bool respuesta = false;
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvdata.Rows)
                {
                    if (fila.Cells["Id"].Value.ToString() == _producto.IdProducto.ToString())
                    {
                        respuesta = true;
                        break;
                    }
                }
            }

            return respuesta;
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
         
            if (_producto == null)
            {
                MessageBox.Show("Debe ingresar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtpreciocompra.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el precio de compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtprecioventa.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el precio de venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (producto_agregado())
            {

                MessageBox.Show("El producto ya está agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            decimal preciocompra = 0;
            decimal precioventa = 0;
            decimal subtotal = 0;

            if (!decimal.TryParse(txtpreciocompra.Text, out preciocompra))
            {
                MessageBox.Show("Error al convertir el tipo de moneda - Precio Compra\nEjemplo Formato ##.##", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpreciocompra.Focus();
                return;
            }

            if (!decimal.TryParse(txtprecioventa.Text, out precioventa))
            {
                MessageBox.Show("Error al convertir el tipo de moneda - Precio Venta\nEjemplo Formato ##.##", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecioventa.Focus();
                return;
            }

            subtotal = Convert.ToDecimal(txtcantidad.Value.ToString()) * Convert.ToDecimal(txtpreciocompra.Text);
            dgvdata.Rows.Add(new object[] {
                _producto.IdProducto.ToString(),
                _producto.Codigo,
                _producto.Descripcion,
                _producto.Categoria,
                _producto.Medida,
                txtcantidad.Value.ToString(),
                precioventa.ToString("0.00"),
                preciocompra.ToString("0.00"),
                subtotal.ToString("0.00"),
                ""
            });

            calcularTotal();

            _producto = null;
            txtcodproducto.Text = "";
            txtcodproducto.BackColor = Color.White;
            txtproducto.Text = "";
            txtpreciocompra.Text = "";
            txtprecioventa.Text = "";
            txtcantidad.Value = 1;
            txtcodproducto.Focus();
        }

        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
                }
            }
            txttotalpagar.Text = total.ToString("0.00");
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
                {
                    dgvdata.Rows.RemoveAt(index);
                    txtcodproducto.Select();
                    calcularTotal();
                }
            }
        }

        private void btnCrearCompra_Click(object sender, EventArgs e)
        {
            
            if (txtdocproveedor.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el documento del proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtnombreproveedor.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el nombre del proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string mensaje = string.Empty;
            int cantidad_productos = 0;
            int idcorrelativo = CompraLogica.Instancia.ObtenerCorrelativo(out mensaje);
            List<DetalleCompra> olista = new List<DetalleCompra>();

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                olista.Add(new DetalleCompra()
                {
                    IdProducto = Convert.ToInt32(row.Cells["Id"].Value.ToString()),
                    CodigoProducto = row.Cells["Codigo"].Value.ToString(),
                    DescripcionProducto = row.Cells["Producto"].Value.ToString(),
                    CategoriaProducto = row.Cells["Categoria"].Value.ToString(),
                    MedidaProducto = row.Cells["Medida"].Value.ToString(),
                    Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value.ToString()),
                    PrecioVenta = row.Cells["PrecioVenta"].Value.ToString(),
                    PrecioCompra = row.Cells["PrecioCompra"].Value.ToString(),
                    SubTotal = row.Cells["SubTotal"].Value.ToString()
                });

                cantidad_productos += Convert.ToInt32(row.Cells["Cantidad"].Value.ToString());
            }

            Compra oEntrada = new Compra()
            {
                NumeroDocumento = String.Format("{0:00000}", idcorrelativo),
                FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")),
                UsuarioRegistro = _NombreUsuario,
                DocumentoProveedor = txtdocproveedor.Text,
                NombreProveedor = txtnombreproveedor.Text,
                CantidadProductos = cantidad_productos,
                MontoTotal = txttotalpagar.Text,
                olistaDetalle = olista
            };

            int operaciones = CompraLogica.Instancia.Registrar(oEntrada, out mensaje);

            if (operaciones < 1)
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                txtdocproveedor.Text = "";
                txtnombreproveedor.Text = "";
                dgvdata.Rows.Clear();
                txttotalpagar.Text = "0.00";
                txtdocproveedor.Focus();

                mdCompraExitosa md = new mdCompraExitosa();
                md._numerodocumento = String.Format("{0:00000}", idcorrelativo);
                md.ShowDialog();
            }

        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 9)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete17.Width;
                var h = Properties.Resources.delete17.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete17, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}
