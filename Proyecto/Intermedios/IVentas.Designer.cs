namespace Proyecto.Intermedios
{
    partial class IVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IVentas));
            this.label1 = new System.Windows.Forms.Label();
            this.btncerrar = new FontAwesome.Sharp.IconButton();
            this.btnbuscarventa = new FontAwesome.Sharp.IconButton();
            this.btnlistaventas = new FontAwesome.Sharp.IconButton();
            this.btnnuevaventa = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 296);
            this.label1.TabIndex = 4;
            // 
            // btncerrar
            // 
            this.btncerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(63)))), ((int)(((byte)(84)))));
            this.btncerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncerrar.ForeColor = System.Drawing.Color.White;
            this.btncerrar.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            this.btncerrar.IconColor = System.Drawing.Color.White;
            this.btncerrar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btncerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncerrar.Location = new System.Drawing.Point(40, 223);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Padding = new System.Windows.Forms.Padding(60, 3, 0, 0);
            this.btncerrar.Size = new System.Drawing.Size(247, 55);
            this.btncerrar.TabIndex = 3;
            this.btncerrar.Text = "Cerrar";
            this.btncerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btncerrar.UseVisualStyleBackColor = false;
            this.btncerrar.Click += new System.EventHandler(this.btncerrar_Click);
            // 
            // btnbuscarventa
            // 
            this.btnbuscarventa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(63)))), ((int)(((byte)(84)))));
            this.btnbuscarventa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnbuscarventa.ForeColor = System.Drawing.Color.White;
            this.btnbuscarventa.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnbuscarventa.IconColor = System.Drawing.Color.White;
            this.btnbuscarventa.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnbuscarventa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnbuscarventa.Location = new System.Drawing.Point(40, 100);
            this.btnbuscarventa.Name = "btnbuscarventa";
            this.btnbuscarventa.Padding = new System.Windows.Forms.Padding(60, 3, 0, 0);
            this.btnbuscarventa.Size = new System.Drawing.Size(247, 55);
            this.btnbuscarventa.TabIndex = 2;
            this.btnbuscarventa.Text = "Buscar Venta";
            this.btnbuscarventa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnbuscarventa.UseVisualStyleBackColor = false;
            this.btnbuscarventa.Click += new System.EventHandler(this.btnbuscarventa_Click);
            // 
            // btnlistaventas
            // 
            this.btnlistaventas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(63)))), ((int)(((byte)(84)))));
            this.btnlistaventas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlistaventas.ForeColor = System.Drawing.Color.White;
            this.btnlistaventas.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.btnlistaventas.IconColor = System.Drawing.Color.White;
            this.btnlistaventas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnlistaventas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlistaventas.Location = new System.Drawing.Point(40, 161);
            this.btnlistaventas.Name = "btnlistaventas";
            this.btnlistaventas.Padding = new System.Windows.Forms.Padding(60, 3, 0, 0);
            this.btnlistaventas.Size = new System.Drawing.Size(247, 55);
            this.btnlistaventas.TabIndex = 1;
            this.btnlistaventas.Text = "Lista de Ventas";
            this.btnlistaventas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlistaventas.UseVisualStyleBackColor = false;
            this.btnlistaventas.Click += new System.EventHandler(this.btnlistaventas_Click);
            // 
            // btnnuevaventa
            // 
            this.btnnuevaventa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(63)))), ((int)(((byte)(84)))));
            this.btnnuevaventa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnnuevaventa.ForeColor = System.Drawing.Color.White;
            this.btnnuevaventa.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnnuevaventa.IconColor = System.Drawing.Color.White;
            this.btnnuevaventa.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnnuevaventa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnnuevaventa.Location = new System.Drawing.Point(40, 39);
            this.btnnuevaventa.Name = "btnnuevaventa";
            this.btnnuevaventa.Padding = new System.Windows.Forms.Padding(60, 3, 0, 0);
            this.btnnuevaventa.Size = new System.Drawing.Size(247, 55);
            this.btnnuevaventa.TabIndex = 0;
            this.btnnuevaventa.Text = "Nueva Venta";
            this.btnnuevaventa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnnuevaventa.UseVisualStyleBackColor = false;
            this.btnnuevaventa.Click += new System.EventHandler(this.btnnuevaventa_Click);
            // 
            // IVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(322, 314);
            this.Controls.Add(this.btncerrar);
            this.Controls.Add(this.btnbuscarventa);
            this.Controls.Add(this.btnlistaventas);
            this.Controls.Add(this.btnnuevaventa);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IVentas";
            this.Load += new System.EventHandler(this.IVentas_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnnuevaventa;
        private FontAwesome.Sharp.IconButton btnlistaventas;
        private FontAwesome.Sharp.IconButton btnbuscarventa;
        private FontAwesome.Sharp.IconButton btncerrar;
        private System.Windows.Forms.Label label1;
    }
}