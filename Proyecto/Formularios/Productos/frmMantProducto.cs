using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.Modelo;
using ProyectoVenta.Logica;

namespace Proyecto.Formularios.Productos
{
    public partial class frmMantProducto : Form
    {
        public bool _modo_editar { get; set; }
        public Producto _Producto{ get; set; }
        public frmMantProducto()
        {
            InitializeComponent();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMantProducto_Load(object sender, EventArgs e)
        {

            if (_modo_editar)
            {
                txtcodigo.Text = _Producto.Codigo;
                txtproducto.Text = _Producto.Descripcion;
                txtcategoria.Text = _Producto.Categoria;
                txtmedida.Text = _Producto.Medida;
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            lblresultado.Visible = true;
            string mensaje = string.Empty;

            if (txtcodigo.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el código del producto";
                lblresultado.ForeColor = Color.Red;
                return;
            }
            if (txtproducto.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el nombre del producto";
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (_Producto != null)
            {
                _Producto.Codigo = txtcodigo.Text;
                _Producto.Descripcion = txtproducto.Text;
                _Producto.Categoria = txtcategoria.Text;
                _Producto.Medida = txtmedida.Text;
            }
            else
                _Producto = new Producto() { IdProducto = 0, Codigo = txtcodigo.Text,
                    Descripcion = txtproducto.Text,
                    Categoria = txtcategoria.Text,
                    Medida = txtmedida.Text
                };

            int existe = ProductoLogica.Instancia.Existe(_Producto.Codigo, _Producto.IdProducto, out mensaje);
            if (existe > 0)
            {
                lblresultado.Text = mensaje;
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (!_modo_editar)
            {
                int idgenerado = ProductoLogica.Instancia.Guardar(_Producto, out mensaje);
                if (idgenerado > 0)
                {
                    _Producto.IdProducto = idgenerado;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblresultado.Text = mensaje;
                    lblresultado.ForeColor = Color.Red;
                }
            }
            else
            {
                int afectados = ProductoLogica.Instancia.Editar(_Producto, out mensaje);
                if (afectados > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblresultado.Text = mensaje;
                    lblresultado.ForeColor = Color.Red;
                }

            }
        }
    }
}
