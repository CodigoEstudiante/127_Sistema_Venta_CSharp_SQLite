using Proyecto.Formularios.Productos;
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
    public partial class IProductos : Form
    {
        public Form FormularioVista { get; set; }
        public string _NombreUsuario { get; set; }
        public IProductos()
        {
            InitializeComponent();
        }

        private void IProductos_Load(object sender, EventArgs e)
        {

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnnuevaproducto_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmProductos();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btncargamasiva_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmCargaMasiva();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btngenerarbarras_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmGenerarBarras();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
