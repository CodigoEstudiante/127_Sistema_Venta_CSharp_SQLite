using Proyecto.Formularios.Mantenimiento;
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
    public partial class IMantenimiento : Form
    {
        public Form FormularioVista { get; set; }
        public string _NombreUsuario { get; set; }
        public IMantenimiento()
        {
            InitializeComponent();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IMantenimiento_Load(object sender, EventArgs e)
        {

        }

        private void btnnuevacompra_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmUsuarios();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnbuscarcompra_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmPermisos();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnlistacompras_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmNegocio();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
