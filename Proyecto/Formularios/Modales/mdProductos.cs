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

namespace Proyecto.Formularios.Modales
{
    public partial class mdProductos : Form
    {
        public Producto _Producto { get; set; }
        public mdProductos()
        {
            InitializeComponent();
        }

        private void mdProductos_Load(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            List<Producto> lista = ProductoLogica.Instancia.Listar(out mensaje);

            foreach (Producto pr in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    "",
                    pr.IdProducto,
                    pr.Codigo,
                    pr.Descripcion,
                    pr.Categoria,
                    pr.Medida,
                    pr.PrecioCompra,
                    pr.PrecioVenta,
                    pr.Stock
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
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check16.Width;
                var h = Properties.Resources.check16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
                {
                    _Producto = new Producto() {
                        IdProducto = Convert.ToInt32(dgvdata.Rows[index].Cells["Id"].Value.ToString()),
                        Codigo = dgvdata.Rows[index].Cells["Codigo"].Value.ToString(),
                        Descripcion = dgvdata.Rows[index].Cells["Producto"].Value.ToString(),
                        Categoria = dgvdata.Rows[index].Cells["Categoria"].Value.ToString(),
                        Medida = dgvdata.Rows[index].Cells["Medida"].Value.ToString(),
                        PrecioCompra = dgvdata.Rows[index].Cells["PrecioCompra"].Value.ToString(),
                        PrecioVenta = dgvdata.Rows[index].Cells["PrecioVenta"].Value.ToString(),
                        Stock = Convert.ToInt32(dgvdata.Rows[index].Cells["Stock"].Value.ToString())
                    };
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
