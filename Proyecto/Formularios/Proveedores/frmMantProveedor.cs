using Proyecto.Modelo;
using ProyectoVenta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto.Formularios.Proveedores
{
    public partial class frmMantProveedor : Form
    {
        public bool _modo_editar { get; set; }
        public Proveedor _Proveedor { get; set; }
        public frmMantProveedor()
        {
            InitializeComponent();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMantProveedor_Load(object sender, EventArgs e)
        {
            if (_modo_editar)
            {
                txtnombre.Text = _Proveedor.NombreCompleto;
                txtnumero.Text = _Proveedor.NumeroDocumento;
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            lblresultado.Visible = true;
            string mensaje = string.Empty;

            if (txtnumero.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el numero de documento";
                lblresultado.ForeColor = Color.Red;
                return;
            }
            if (txtnombre.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el nombre completo";
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (_Proveedor != null)
            {
                _Proveedor.NumeroDocumento = txtnumero.Text;
                _Proveedor.NombreCompleto = txtnombre.Text;
            }
            else
                _Proveedor = new Proveedor() { IdProveedor = 0, NumeroDocumento = txtnumero.Text, NombreCompleto = txtnombre.Text };

            int existe = ProveedorLogica.Instancia.Existe(_Proveedor.NumeroDocumento, _Proveedor.IdProveedor, out mensaje);
            if (existe > 0)
            {
                lblresultado.Text = mensaje;
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (!_modo_editar)
            {
                int idgenerado = ProveedorLogica.Instancia.Guardar(_Proveedor, out mensaje);
                if (idgenerado > 0)
                {
                    _Proveedor.IdProveedor = idgenerado;
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
                int afectados = ProveedorLogica.Instancia.Editar(_Proveedor, out mensaje);
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
