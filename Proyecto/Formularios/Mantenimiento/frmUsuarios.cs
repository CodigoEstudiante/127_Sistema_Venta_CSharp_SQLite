using Proyecto.Herrarmientas;
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

namespace Proyecto.Formularios.Mantenimiento
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void btnnuevousuario_Click(object sender, EventArgs e)
        {
            using (var mdForm = new frmMantUsuario_())
            {
                mdForm._modo_editar = false;
                var result = mdForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dgvdata.Rows.Add(new object[] {
                        mdForm._Usuario.IdUsuario,
                        mdForm._Usuario.NombreUsuario,
                        mdForm._Usuario.NombreCompleto,
                        mdForm._Usuario.IdPermisos,
                        mdForm._Usuario.Descripcion,
                        mdForm._Usuario.Clave,
                        "", "" });
                    btnnuevousuario.Select();
                }
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {

            string mensaje = string.Empty;
            List<Usuario> lista = UsuarioLogica.Instancia.Listar(out mensaje);

            foreach (Usuario us in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    us.IdUsuario,
                    us.NombreUsuario,
                    us.NombreCompleto,
                    us.IdPermisos,
                    us.Descripcion,
                    us.Clave,
                    "",
                    ""
                });
            }

            foreach (DataGridViewColumn cl in dgvdata.Columns)
            {
                if (cl.Visible == true && cl.HeaderText != "")
                {
                    cbobuscar.Items.Add(new OpcionCombo() { Valor = cl.Name, Texto = cl.HeaderText });
                }
            }
            cbobuscar.DisplayMember = "Texto";
            cbobuscar.ValueMember = "Valor";
            cbobuscar.SelectedIndex = 0;
            btnnuevousuario.Select();
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 6)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.edit16.Width;
                var h = Properties.Resources.edit16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.edit16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
            if (e.ColumnIndex == 7)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete16.Width;
                var h = Properties.Resources.delete16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0)
            {
                if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
                {
                    if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string mensaje = string.Empty;
                        int id = int.Parse(dgvdata.Rows[index].Cells["Id"].Value.ToString());
                        int respuesta = UsuarioLogica.Instancia.Eliminar(id);
                        if (respuesta > 0)
                        {
                            dgvdata.Rows.RemoveAt(index);
                        }
                        else
                            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (dgvdata.Columns[e.ColumnIndex].Name == "btneditar")
                {
                    Usuario objeto = new Usuario()
                    {
                        IdUsuario = int.Parse(dgvdata.Rows[index].Cells["Id"].Value.ToString()),
                        NombreUsuario = dgvdata.Rows[index].Cells["Usuario"].Value.ToString(),
                        NombreCompleto = dgvdata.Rows[index].Cells["NombreCompleto"].Value.ToString(),
                        IdPermisos = Convert.ToInt32(dgvdata.Rows[index].Cells["IdRol"].Value.ToString()),
                        Clave = dgvdata.Rows[index].Cells["Clave"].Value.ToString()
                    };

                    using (var form = new frmMantUsuario_())
                    {
                        form._modo_editar = true;
                        form._Usuario = objeto;
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            dgvdata.Rows[index].Cells["Id"].Value = form._Usuario.IdUsuario;
                            dgvdata.Rows[index].Cells["Usuario"].Value = form._Usuario.NombreUsuario;
                            dgvdata.Rows[index].Cells["NombreCompleto"].Value = form._Usuario.NombreCompleto;
                            dgvdata.Rows[index].Cells["IdRol"].Value = form._Usuario.IdPermisos;
                            dgvdata.Rows[index].Cells["Rol"].Value = form._Usuario.Descripcion;
                            dgvdata.Rows[index].Cells["Clave"].Value = form._Usuario.Clave;

                        }
                    }
                }

            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {

            string columnaFiltro = ((OpcionCombo)cbobuscar.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbuscar.Text.ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            txtbuscar.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
            dgvdata.ClearSelection();
        }
    }
}
