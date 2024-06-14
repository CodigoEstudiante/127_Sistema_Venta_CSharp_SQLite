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

namespace Proyecto.Formularios.Clientes
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmClientes_Load(object sender, EventArgs e)
        {
            
            string mensaje = string.Empty;
            List<Cliente> lista = ClienteLogica.Instancia.Listar(out mensaje);

            foreach (Cliente pr in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    pr.IdCliente,
                    pr.NumeroDocumento,
                    pr.NombreCompleto,
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
            btnnuevocliente.Focus();
        }


        private void btnnuevocliente_Click(object sender, EventArgs e)
        {
            using (var mdForm = new frmMantCliente())
            {
                mdForm._modo_editar = false;
                var result = mdForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dgvdata.Rows.Add(new object[] { mdForm._Cliente.IdCliente, mdForm._Cliente.NumeroDocumento, mdForm._Cliente.NombreCompleto, "", "" });
                    btnnuevocliente.Focus();
                }
            }
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 3)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.edit16.Width;
                var h = Properties.Resources.edit16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.edit16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
            if (e.ColumnIndex == 4)
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
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {

                if (index >= 0)
                {
                    if (MessageBox.Show("¿Desea eliminar el cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string mensaje = string.Empty;
                        int idcliente = int.Parse(dgvdata.Rows[index].Cells["Id"].Value.ToString());
                        int respuesta = ClienteLogica.Instancia.Eliminar(idcliente);
                        if (respuesta > 0)
                        {
                            dgvdata.Rows.RemoveAt(index);
                        }
                        else
                            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
            else if (dgvdata.Columns[e.ColumnIndex].Name == "btneditar")
            {
                if (index >= 0)
                {
                    Cliente ocliente = new Cliente()
                    {
                        IdCliente = int.Parse(dgvdata.Rows[index].Cells["Id"].Value.ToString()),
                        NumeroDocumento = dgvdata.Rows[index].Cells["NumeroDocumento"].Value.ToString(),
                        NombreCompleto = dgvdata.Rows[index].Cells["NombreCompleto"].Value.ToString()
                    };

                    using (var form = new frmMantCliente())
                    {
                        form._modo_editar = true;
                        form._Cliente = ocliente;
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            dgvdata.Rows[index].Cells["Id"].Value = form._Cliente.IdCliente;
                            dgvdata.Rows[index].Cells["NumeroDocumento"].Value = form._Cliente.NumeroDocumento;
                            dgvdata.Rows[index].Cells["NombreCompleto"].Value = form._Cliente.NombreCompleto;

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
