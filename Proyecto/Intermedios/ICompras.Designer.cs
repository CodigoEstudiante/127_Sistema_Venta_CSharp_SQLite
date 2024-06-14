namespace Proyecto.Intermedios
{
    partial class ICompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ICompras));
            this.label1 = new System.Windows.Forms.Label();
            this.btncerrar = new FontAwesome.Sharp.IconButton();
            this.btnbuscarcompra = new FontAwesome.Sharp.IconButton();
            this.btnlistacompras = new FontAwesome.Sharp.IconButton();
            this.btnnuevacompra = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 296);
            this.label1.TabIndex = 9;
            this.label1.Click += new System.EventHandler(this.label1_Click);
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
            this.btncerrar.Location = new System.Drawing.Point(41, 223);
            this.btncerrar.Name = "btncerrar";
            this.btncerrar.Padding = new System.Windows.Forms.Padding(60, 3, 0, 0);
            this.btncerrar.Size = new System.Drawing.Size(247, 55);
            this.btncerrar.TabIndex = 8;
            this.btncerrar.Text = "Cerrar";
            this.btncerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btncerrar.UseVisualStyleBackColor = false;
            this.btncerrar.Click += new System.EventHandler(this.btncerrar_Click);
            // 
            // btnbuscarcompra
            // 
            this.btnbuscarcompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(63)))), ((int)(((byte)(84)))));
            this.btnbuscarcompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnbuscarcompra.ForeColor = System.Drawing.Color.White;
            this.btnbuscarcompra.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnbuscarcompra.IconColor = System.Drawing.Color.White;
            this.btnbuscarcompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnbuscarcompra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnbuscarcompra.Location = new System.Drawing.Point(41, 100);
            this.btnbuscarcompra.Name = "btnbuscarcompra";
            this.btnbuscarcompra.Padding = new System.Windows.Forms.Padding(60, 3, 0, 0);
            this.btnbuscarcompra.Size = new System.Drawing.Size(247, 55);
            this.btnbuscarcompra.TabIndex = 7;
            this.btnbuscarcompra.Text = "Buscar Compra";
            this.btnbuscarcompra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnbuscarcompra.UseVisualStyleBackColor = false;
            this.btnbuscarcompra.Click += new System.EventHandler(this.btnbuscarcompra_Click);
            // 
            // btnlistacompras
            // 
            this.btnlistacompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(63)))), ((int)(((byte)(84)))));
            this.btnlistacompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlistacompras.ForeColor = System.Drawing.Color.White;
            this.btnlistacompras.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.btnlistacompras.IconColor = System.Drawing.Color.White;
            this.btnlistacompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnlistacompras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlistacompras.Location = new System.Drawing.Point(41, 161);
            this.btnlistacompras.Name = "btnlistacompras";
            this.btnlistacompras.Padding = new System.Windows.Forms.Padding(60, 3, 0, 0);
            this.btnlistacompras.Size = new System.Drawing.Size(247, 55);
            this.btnlistacompras.TabIndex = 6;
            this.btnlistacompras.Text = "Lista de Compras";
            this.btnlistacompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlistacompras.UseVisualStyleBackColor = false;
            this.btnlistacompras.Click += new System.EventHandler(this.btnlistacompras_Click);
            // 
            // btnnuevacompra
            // 
            this.btnnuevacompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(63)))), ((int)(((byte)(84)))));
            this.btnnuevacompra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnnuevacompra.ForeColor = System.Drawing.Color.White;
            this.btnnuevacompra.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnnuevacompra.IconColor = System.Drawing.Color.White;
            this.btnnuevacompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnnuevacompra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnnuevacompra.Location = new System.Drawing.Point(41, 39);
            this.btnnuevacompra.Name = "btnnuevacompra";
            this.btnnuevacompra.Padding = new System.Windows.Forms.Padding(60, 3, 0, 0);
            this.btnnuevacompra.Size = new System.Drawing.Size(247, 55);
            this.btnnuevacompra.TabIndex = 5;
            this.btnnuevacompra.Text = "Nueva Compra";
            this.btnnuevacompra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnnuevacompra.UseVisualStyleBackColor = false;
            this.btnnuevacompra.Click += new System.EventHandler(this.btnnuevacompra_Click);
            // 
            // ICompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(322, 314);
            this.Controls.Add(this.btncerrar);
            this.Controls.Add(this.btnbuscarcompra);
            this.Controls.Add(this.btnlistacompras);
            this.Controls.Add(this.btnnuevacompra);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(322, 314);
            this.MinimumSize = new System.Drawing.Size(322, 314);
            this.Name = "ICompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ICompras";
            this.Load += new System.EventHandler(this.ICompras_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton btncerrar;
        private FontAwesome.Sharp.IconButton btnbuscarcompra;
        private FontAwesome.Sharp.IconButton btnlistacompras;
        private FontAwesome.Sharp.IconButton btnnuevacompra;
        private System.Windows.Forms.Label label1;
    }
}