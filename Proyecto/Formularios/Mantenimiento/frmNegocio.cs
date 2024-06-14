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

namespace Proyecto.Formularios.Mantenimiento
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }

        private void fmrNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = DatoLogica.Instancia.ObtenerLogo(out obtenido);
            if (obtenido)
                picLogo.Image = ByteToImage(byteimage);


            Datos da = DatoLogica.Instancia.Obtener();
            txtrazonsocial.Text = da.RazonSocial;
            txtruc.Text = da.RUC;
            txtdireccion.Text = da.Direccion;

        }

        public Image ByteToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }


        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnguardarcambios_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (txtrazonsocial.Text == "")
            {
                MessageBox.Show("Debe ingresar Razon Social", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtruc.Text == "")
            {
                MessageBox.Show("Debe ingresar R.U.C", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtdireccion.Text == "")
            {
                MessageBox.Show("Debe ingresar direccion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            int nrooperacion = DatoLogica.Instancia.Guardar(new Datos()
            {
                RazonSocial = txtrazonsocial.Text,
                RUC = txtruc.Text,
                Direccion = txtdireccion.Text
            }, out mensaje);

            if (nrooperacion < 1)
            {
                MessageBox.Show("No se pudo guardar los cambios, intente más tarde", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteImagen = File.ReadAllBytes(oOpenFileDialog.FileName);
                int numerooperacion = DatoLogica.Instancia.ActualizarLogo(byteImagen, out mensaje);

                if (numerooperacion < 1)
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    picLogo.Image = ByteToImage(byteImagen);
                }
            }
        }

        
    }
}
