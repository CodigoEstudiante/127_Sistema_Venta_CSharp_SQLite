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

namespace Proyecto.Formularios.Mantenimiento
{
    public partial class frmPermisos : Form
    {
        public frmPermisos()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPermisos_Load(object sender, EventArgs e)
        {
            Modelo.Permisos padmin = PermisosLogica.Instancia.Obtener(1);
            Modelo.Permisos pemple = PermisosLogica.Instancia.Obtener(2);

            a_ventas.Checked = padmin.Ventas == 1 ? true : false;
            a_compras.Checked = padmin.Compras == 1 ? true : false;
            a_productos.Checked = padmin.Productos == 1 ? true : false;
            a_clientes.Checked = padmin.Clientes == 1 ? true : false;
            a_proveedores.Checked = padmin.Proveedores == 1 ? true : false;
            a_mantenimiento.Checked = padmin.Mantenimiento == 1 ? true : false;

            e_ventas.Checked = pemple.Ventas == 1 ? true : false;
            e_compras.Checked = pemple.Compras == 1 ? true : false;
            e_productos.Checked = pemple.Productos == 1 ? true : false;
            e_clientes.Checked = pemple.Clientes == 1 ? true : false;
            e_proveedores.Checked = pemple.Proveedores == 1 ? true : false;
            e_mantenimiento.Checked = pemple.Mantenimiento == 1 ? true : false;
        }

        private void btnguardaradministrador_Click(object sender, EventArgs e)
        {
            int _a_ventas = 0;
            int _a_compras = 0;
            int _a_productos = 0;
            int _a_clientes = 0;
            int _a_proveedores = 0;
            int _a_mantenimiento = 0;

            if (a_ventas.Checked)
                _a_ventas = 1;

            if (a_compras.Checked)
                _a_compras = 1;

            if (a_productos.Checked)
                _a_productos = 1;

            if (a_clientes.Checked)
                _a_clientes = 1;

            if (a_proveedores.Checked)
                _a_proveedores = 1;
            
            if (a_mantenimiento.Checked)
                _a_mantenimiento = 1;


            string mensaje = string.Empty;

            int operaciones = PermisosLogica.Instancia.Guardar(new Modelo.Permisos()
            {
                IdPermisos = 1,
                Ventas = _a_ventas,
                Compras = _a_compras,
                Productos = _a_productos,
                Clientes = _a_clientes,
                Proveedores = _a_proveedores,
                Mantenimiento = _a_mantenimiento
            }, out mensaje);

            if (operaciones < 1)
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Se guardaron los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnguardarempleados_Click(object sender, EventArgs e)
        {
            int _e_ventas = 0;
            int _e_compras = 0;
            int _e_productos = 0;
            int _e_clientes = 0;
            int _e_proveedores = 0;
            int _e_mantenimiento = 0;

            if (e_ventas.Checked)
                _e_ventas = 1;

            if (e_compras.Checked)
                _e_compras = 1;

            if (e_productos.Checked)
                _e_productos = 1;

            if (e_clientes.Checked)
                _e_clientes = 1;

            if (e_proveedores.Checked)
                _e_proveedores = 1;

            if (e_mantenimiento.Checked)
                _e_mantenimiento = 1;


            string mensaje = string.Empty;

            int operaciones = PermisosLogica.Instancia.Guardar(new Modelo.Permisos()
            {
                IdPermisos = 2,
                Ventas = _e_ventas,
                Compras = _e_compras,
                Productos = _e_productos,
                Clientes = _e_clientes,
                Proveedores = _e_proveedores,
                Mantenimiento = _e_mantenimiento
            }, out mensaje);

            if (operaciones < 1)
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Se guardaron los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
