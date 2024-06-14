using Proyecto.Formularios.Compras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto.Intermedios
{
    public partial class ICompras : Form
    {
        public Form FormularioVista { get; set; }
        public string _NombreUsuario { get; set; }
        public ICompras()
        {
            InitializeComponent();
        }

        private void ICompras_Load(object sender, EventArgs e)
        {

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnnuevacompra_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmNuevaCompra(_NombreUsuario);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnbuscarcompra_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmBuscarCompra();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnlistacompras_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmListaCompras();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
