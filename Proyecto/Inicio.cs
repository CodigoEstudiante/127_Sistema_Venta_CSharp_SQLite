using Proyecto.Formularios.Clientes;
using Proyecto.Formularios.Proveedores;
using Proyecto.Intermedios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class Inicio : Form
    {
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string FechaHora { get; set; }
        public string Clave { get; set; }
        public Proyecto.Modelo.Permisos oPermisos { get; set; }
        public Inicio()
        {
            InitializeComponent();
        }
        private void Inicio_Load(object sender, EventArgs e)
        {
            lblstatus1.Text = string.Format("{0}", NombreUsuario);
            lblstatus2.Text = string.Format("{0}", FechaHora);

            if (oPermisos.Ventas == 0)
            {
                menuVentas.Enabled = false;
                menuVentas.Cursor = Cursors.No;
            }
            if (oPermisos.Compras == 0)
            {
                menuCompras.Enabled = false;
                menuCompras.Cursor = Cursors.No;
            }
            if (oPermisos.Productos == 0)
            {
                menuProductos.Enabled = false;
                menuProductos.Cursor = Cursors.No;
            }
            if (oPermisos.Clientes == 0)
            {
                menuClientes.Enabled = false;
                menuClientes.Cursor = Cursors.No;
            }
            if (oPermisos.Proveedores == 0)
            {
                menuProveedores.Enabled = false;
                menuProveedores.Cursor = Cursors.No;
            }
            
            if (oPermisos.Mantenimiento == 0)
            {
                menuMantenimiento.Enabled = false;
                menuMantenimiento.Cursor = Cursors.No;
            }
        }
        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void menuVentas_Click(object sender, EventArgs e)
        {
            using (var Iform = new IVentas())
            {
               
                Iform._NombreUsuario = NombreUsuario;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }

        private void menuCompras_Click(object sender, EventArgs e)
        {
            using (var Iform = new ICompras())
            {

                Iform._NombreUsuario = NombreUsuario;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }

        private void menuProductos_Click(object sender, EventArgs e)
        {
            using (var Iform = new IProductos())
            {

                Iform._NombreUsuario = NombreUsuario;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            frmClientes FormularioVista = new frmClientes();
            this.Hide();
            FormularioVista.Show();
            FormularioVista.FormClosing += Frm_Closing;
        }

        private void menuProveedores_Click(object sender, EventArgs e)
        {
            frmProveedores FormularioVista = new frmProveedores();
            this.Hide();
            FormularioVista.Show();
            FormularioVista.FormClosing += Frm_Closing;
        }

        private void menuMantenimiento_Click(object sender, EventArgs e)
        {
            using (var Iform = new IMantenimiento())
            {

                Iform._NombreUsuario = NombreUsuario;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            mdAcercaDe form = new mdAcercaDe();
            form.ShowDialog();
        }
    }
}
