using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Proyecto.Modelo;
using ProyectoVenta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto.Formularios.Productos
{
    public partial class frmCargaMasiva : Form
    {
        private static bool permitir_carga = false;
        private static int total_productos = 0;
        DataTable table = new DataTable();


        public frmCargaMasiva()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCargaMasiva_Load(object sender, EventArgs e)
        {
            lblresumen.Text = "";
            table.Columns.Add("NroFila", typeof(string));
            table.Columns.Add("Codigo", typeof(string));
            table.Columns.Add("Mensaje", typeof(string));
            table.Columns.Add("Estado", typeof(string));
        }

        private void btncargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.Filter = "Excel Files|*.xlsx";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtarchivo.Text = oOpenFileDialog.FileName.ToString();

                try
                {
                    IWorkbook MiExcel = null;
                    FileStream fs = new FileStream(oOpenFileDialog.FileName, FileMode.Open, FileAccess.Read);

                    if (Path.GetExtension(oOpenFileDialog.FileName) == ".xlsx")
                    {
                        MiExcel = new XSSFWorkbook(fs);
                        ISheet hoja = MiExcel.GetSheetAt(0);

                        if (hoja != null)
                        {

                            string columnas = string.Empty;
                            if (hoja.GetRow(0).GetCell(0) != null)
                            {
                                if (hoja.GetRow(0).Cells[0].StringCellValue.ToString().ToLower() != "codigo")
                                {
                                    columnas += "No se encontró la columna \"Codigo\"\n";
                                }
                            }
                            else
                                columnas += "No se encontró la columna \"Codigo\"\n";

                            if (hoja.GetRow(0).GetCell(1) != null)
                            {
                                if (hoja.GetRow(0).Cells[1].StringCellValue.ToString().ToLower() != "descripcion")
                                {
                                    columnas += "No se encontró la columna \"Descripcion\"\n";
                                }
                            }
                            else
                                columnas += "No se encontró la columna \"Descripcion\"\n";


                            if (hoja.GetRow(0).GetCell(2) != null)
                            {
                                if (hoja.GetRow(0).Cells[2].StringCellValue.ToString().ToLower() != "categoria")
                                {
                                    columnas += "No se encontró la columna \"Categoria\"\n";
                                }
                            }
                            else
                                columnas += "No se encontró la columna \"Categoria\"\n";


                            if (hoja.GetRow(0).GetCell(3) != null)
                            {
                                if (hoja.GetRow(0).Cells[3].StringCellValue.ToString().ToLower() != "medida")
                                {
                                    columnas += "No se encontró la columna \"Medida\"\n";
                                }
                            }
                            else
                                columnas += "No se encontró la columna \"Medida\"\n";



                            if (string.IsNullOrEmpty(columnas))
                            {
                                int cantidadfilas = hoja.LastRowNum;
                                total_productos = cantidadfilas;
                                lblresumen.Text = string.Format("{0} Producto(s) encontrado(s)", cantidadfilas.ToString());
                                permitir_carga = true;
                            }
                            else
                            {
                                MessageBox.Show(columnas, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                permitir_carga = false;
                                lblresumen.Text = "";
                            }


                        }
                        else
                        {
                            MessageBox.Show("No se encontro una hoja", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            permitir_carga = false;
                            lblresumen.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Archivo incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        permitir_carga = false;
                        lblresumen.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    permitir_carga = false;
                    lblresumen.Text = "";
                    txtarchivo.Text = "";
                }
            }
        }

        private void btndescargar_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] array = Properties.Resources.PlantillaProducto;
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = "PlantillaProductos.xlsx";
                savefile.Filter = "Excel Files|*.xlsx";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(savefile.FileName, array.ToArray());
                    MessageBox.Show("Descarga Exitosa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Error al descargar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnprocesar_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                dgvdata.Rows.Clear();
                table.Rows.Clear();
                progressBar1.Maximum = total_productos;
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Cargue el archivo correcto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (permitir_carga)
            {
                if (txtarchivo.Text.Trim() != "")
                {

                    try
                    {
                        
                        List<Producto> oListaProducto = new List<Producto>();
                        IWorkbook MiExcel = null;
                        FileStream fs = new FileStream(txtarchivo.Text, FileMode.Open, FileAccess.Read);
                        MiExcel = new XSSFWorkbook(fs);
                        ISheet hoja = MiExcel.GetSheetAt(0);

                        for (int row = 1; row <= hoja.LastRowNum; row++)
                        {
                            if (hoja.GetRow(row) != null)
                            {
                                string msjExiste = string.Empty;
                                string msjGuardar = string.Empty;
                                IRow fila = hoja.GetRow(row);
                                Producto oProducto = new Producto();
                                try
                                {
                                    oProducto = new Producto()
                                    {
                                        Codigo = fila.GetCell(0, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(0).StringCellValue.ToString() : "",
                                        Descripcion = fila.GetCell(1, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(1).StringCellValue.ToString() : "",
                                        Categoria = fila.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(2).StringCellValue.ToString() : "",
                                        Medida = fila.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(3).StringCellValue.ToString() : "",
                                    };
                                }
                                catch
                                {
                                    oProducto = null;
                                }

                                if (oProducto != null)
                                {
                                    int existe = ProductoLogica.Instancia.Existe(oProducto.Codigo, 0, out msjExiste);
                                    if (existe > 0)
                                    {
                                        table.Rows.Add(row.ToString(), oProducto.Codigo, msjExiste, "x");
                                    }
                                    else
                                    {
                                        int idgenerado = ProductoLogica.Instancia.Guardar(oProducto, out msjGuardar);
                                        if (idgenerado < 1)
                                        {
                                            table.Rows.Add(row.ToString(), oProducto.Codigo, msjGuardar, "x");
                                        }
                                        else
                                        {
                                            table.Rows.Add(row.ToString(), oProducto.Codigo, "Registrado Correctamente", "");
                                        }
                                    }

                                }

                                backgroundWorker1.ReportProgress(row);
                            }
                        }


                    }
                    catch (Exception ex) {
                        MessageBox.Show($"Se encontró el siguiente conflicto:\n{ex.Message}", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    

                }

            }
            else
            {
                MessageBox.Show("Cargue el archivo correcto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int index = 0;
            foreach (DataRow row in table.Rows)
            {
                dgvdata.Rows.Add(new object[] {
                    row["NroFila"].ToString(),
                    row["Codigo"].ToString(),
                    row["Mensaje"].ToString(),
                    row["Estado"].ToString() == "x"? "No Procesado" : "Procesado"
                });

                if (row["Estado"].ToString() == "x")
                    dgvdata.Rows[index].Cells[3].Style.BackColor = Color.Red;
                else
                    dgvdata.Rows[index].Cells[3].Style.BackColor = Color.LimeGreen;

                dgvdata.Rows[index].Cells[3].Style.ForeColor = Color.White;
                index++;
            }

            dgvdata.ClearSelection();
        }
    }
}
