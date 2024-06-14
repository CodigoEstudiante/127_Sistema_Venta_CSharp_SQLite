using ClosedXML.Excel;
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

namespace Proyecto.Formularios.Productos
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            List<Producto> lista = ProductoLogica.Instancia.Listar(out mensaje);

            foreach (Producto pr in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    pr.IdProducto,
                    pr.Codigo,
                    pr.Descripcion,
                    pr.Categoria,
                    pr.Medida,
                    pr.PrecioCompra,
                    pr.PrecioVenta,
                    pr.Stock,
                    "",
                    "",
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

        private void btnnuevoproducto_Click(object sender, EventArgs e)
        {
            using (var mdForm = new frmMantProducto())
            {
                mdForm._modo_editar = false;
                var result = mdForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dgvdata.Rows.Add(new object[] { mdForm._Producto.IdProducto,
                        mdForm._Producto.Codigo,
                        mdForm._Producto.Descripcion,
                        mdForm._Producto.Categoria,
                        mdForm._Producto.Medida,
                        "0",
                        "0",
                        "0",
                        "", "" });
                    btnnuevoproducto.Focus();
                }
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 8)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.edit16.Width;
                var h = Properties.Resources.edit16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.edit16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
            if (e.ColumnIndex == 9)
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
                    if (MessageBox.Show("¿Desea eliminar el producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string mensaje = string.Empty;
                        int id = int.Parse(dgvdata.Rows[index].Cells["Id"].Value.ToString());
                        int respuesta = ProductoLogica.Instancia.Eliminar(id);
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
                    Producto objeto = new Producto()
                    {
                        IdProducto = int.Parse(dgvdata.Rows[index].Cells["Id"].Value.ToString()),
                        Codigo = dgvdata.Rows[index].Cells["Codigo"].Value.ToString(),
                        Descripcion = dgvdata.Rows[index].Cells["Producto"].Value.ToString(),
                        Categoria = dgvdata.Rows[index].Cells["Categoria"].Value.ToString(),
                        Medida = dgvdata.Rows[index].Cells["Medida"].Value.ToString(),
                    };

                    using (var form = new frmMantProducto())
                    {
                        form._modo_editar = true;
                        form._Producto = objeto;
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            dgvdata.Rows[index].Cells["Id"].Value = form._Producto.IdProducto;
                            dgvdata.Rows[index].Cells["Codigo"].Value = form._Producto.Codigo;
                            dgvdata.Rows[index].Cells["Producto"].Value = form._Producto.Descripcion;
                            dgvdata.Rows[index].Cells["Categoria"].Value = form._Producto.Categoria;
                            dgvdata.Rows[index].Cells["Medida"].Value = form._Producto.Medida;
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

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {

                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn column in dgvdata.Columns) {
                    if(column.HeaderText != "" && column.Visible == true)
                        dt.Columns.Add(column.HeaderText, typeof(string));
                }
                    

                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if(row.Visible == true)
                        dt.Rows.Add(new object[] {
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString()
                        });
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("Reporte_Productos_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files|*.xlsx";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }


            }
        }
    }
}
