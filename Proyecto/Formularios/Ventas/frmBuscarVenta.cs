using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Proyecto.Modelo;
using ProyectoVenta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto.Formularios.Ventas
{
    public partial class frmBuscarVenta : Form
    {
        private string textoVigente = "Vigente";
        private string textoCancelado = "Cancelado";
        int idVenta = 0;
        List<DetalleVenta> oDetalleVenta = null;
        public frmBuscarVenta()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBuscarVenta_Load(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            txtbusqueda.Select();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            buscarcompra();
        }

        private void buscarcompra()
        {
            if (txtbusqueda.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese el numero de documento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Venta obj = VentaLogica.Instancia.Obtener(txtbusqueda.Text);

            if (obj != null)
            {
                idVenta = obj.IdVenta;
                txtfecharegistro.Text = obj.FechaRegistro;
                txtnumerodocumento.Text = obj.NumeroDocumento;
                txtusuarioregistro.Text = obj.UsuarioRegistro;
                txtdoccliente.Text = obj.DocumentoCliente;
                txtnombrecliente.Text = obj.NombreCliente;
                lblEstado.Text = obj.Activo == 1 ? textoVigente : textoCancelado;
                lblEstado.ForeColor = obj.Activo == 1 ? Color.Green : Color.Red;

                List<DetalleVenta> olista = VentaLogica.Instancia.ListarDetalle(obj.IdVenta);
                oDetalleVenta = olista;
                dgvdata.Rows.Clear();
                foreach (DetalleVenta de in olista)
                {
                    dgvdata.Rows.Add(new object[] { de.DescripcionProducto, de.Cantidad, de.PrecioVenta, de.SubTotal });
                }

                lbltotal.Text = obj.MontoTotal;
                lblpagocon.Text = obj.PagoCon;
                lblcambio.Text = obj.Cambio;
            }
            else
            {
                limpiar();
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtbusqueda.Select();
            }
        }

        private void limpiar(bool busqueda = false)
        {
            if (busqueda)
                txtbusqueda.Text = "";

            txtnumerodocumento.Text = "";
            txtfecharegistro.Text = "";
            txtusuarioregistro.Text = "";
            txtdoccliente.Text = "";
            txtnombrecliente.Text = "";
            dgvdata.Rows.Clear();
            lbltotal.Text = "0.00";
            lblpagocon.Text = "0.00";
            lblcambio.Text = "0.00";
            txtbusqueda.Select();

            lblEstado.Text = "";
            idVenta = 0;
            oDetalleVenta = null;
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            limpiar(true);
        }

        private void txtbusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buscarcompra();
            }
        }

        private void btndescargarpdf_Click(object sender, EventArgs e)
        {
            if (txtnumerodocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            Datos odatos = DatoLogica.Instancia.Obtener();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.RazonSocial.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.Direccion);

            Texto_Html = Texto_Html.Replace("@numerodocumento", txtnumerodocumento.Text);

            Texto_Html = Texto_Html.Replace("@doccliente", txtdoccliente.Text);
            Texto_Html = Texto_Html.Replace("@nombrecliente", txtnombrecliente.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtfecharegistro.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtusuarioregistro.Text);


            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", lbltotal.Text);
            Texto_Html = Texto_Html.Replace("@pagocon", lblpagocon.Text);
            Texto_Html = Texto_Html.Replace("@cambio", lblcambio.Text);


            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Venta_{0}.pdf", txtnumerodocumento.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    //Creamos un nuevo documento y lo definimos como PDF
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteimage = DatoLogica.Instancia.ObtenerLogo(out obtenido);
                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteimage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btncancelarventa_Click(object sender, EventArgs e)
        {
            if (idVenta == 0)
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (lblEstado.Text == textoCancelado)
            {
                MessageBox.Show("La venta ya se encuentra cancelada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DateTime fechaActual = DateTime.Now;
            DateTime fechaRegistro = DateTime.ParseExact(txtfecharegistro.Text, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("es-PE"));
            TimeSpan resultado = fechaActual.Subtract(fechaRegistro);
            int dias = resultado.Days;

            if (dias > 5)
            {
                MessageBox.Show($"La venta fue registrada\nhace {dias}. Es por ello que\nno puede ser eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("¿Desea cancelar la venta?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string msg = string.Empty;
                int respuesta = VentaLogica.Instancia.cancelar_Venta(idVenta, oDetalleVenta, out msg);
                if (respuesta > 0)
                {
                    lblEstado.ForeColor = Color.Red;
                    lblEstado.Text = textoCancelado;
                    MessageBox.Show("La venta fue cancelada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(msg, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
