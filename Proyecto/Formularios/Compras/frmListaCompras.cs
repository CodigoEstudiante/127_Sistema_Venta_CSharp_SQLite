using ClosedXML.Excel;
using Proyecto.Herrarmientas;
using Proyecto.Modelo;
using ProyectoVenta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto.Formularios.Compras
{
    public partial class frmListaCompras : Form
    {
        public frmListaCompras()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListaCompras_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn cl in dgvdata.Columns)
            {
                if (cl.Visible == true && cl.Name != "btnseleccionar")
                {
                    cbobuscar.Items.Add(new OpcionCombo() { Valor = cl.Name, Texto = cl.HeaderText });
                }
            }
            cbobuscar.DisplayMember = "Texto";
            cbobuscar.ValueMember = "Valor";
            cbobuscar.SelectedIndex = 0;
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            dgvdata.Rows.Clear();

            DateTime dt1 = Convert.ToDateTime(txtfechainicio.Value.ToString("dd/MM/yyyy"));
            DateTime dt2 = Convert.ToDateTime(txtfechafin.Value.ToString("dd/MM/yyyy"));
            List<VistaCompra> lista = CompraLogica.Instancia.Resumen(dt1.ToString("yyyy-MM-dd", new CultureInfo("en-US")), dt2.ToString("yyyy-MM-dd", new CultureInfo("en-US")));

            foreach (VistaCompra vr in lista)
            {

                dgvdata.Rows.Add(new object[] {
                    vr.FechaRegistro,
                    vr.NumeroDocumento,
                    vr.UsuarioRegistro,
                    vr.NombreProveedor,
                    vr.MontoTotal,
                    vr.CodigoProducto,
                    vr.DescripcionProducto,
                    vr.CategoriaProducto,
                    vr.MedidaProducto,
                    vr.PrecioVenta,
                    vr.PrecioCompra,
                    vr.Cantidad,
                    vr.SubTotal
                });
            }
        }

        private void btnbusqueda_Click(object sender, EventArgs e)
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
                foreach (DataGridViewColumn column in dgvdata.Columns)
                    dt.Columns.Add(column.HeaderText, typeof(string));

                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if(row.Visible == true)
                        dt.Rows.Add(new object[] {
                            row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString()
                        });
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("Reporte_Compras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
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
