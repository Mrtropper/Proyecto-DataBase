namespace ExamenGrupo5
{
    partial class VentanaAgregarCosmetico
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.Label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.pb_salir = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbEstado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.spStock = new System.Windows.Forms.NumericUpDown();
            this.spPrecio = new System.Windows.Forms.NumericUpDown();
            this.cbCategoria = new System.Windows.Forms.ComboBox();
            this.label_Estado = new System.Windows.Forms.Label();
            this.label_Clave = new System.Windows.Forms.Label();
            this.btn_Aceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_salir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPrecio)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtNombre.ForeColor = System.Drawing.Color.Snow;
            this.txtNombre.Location = new System.Drawing.Point(14, 158);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(560, 39);
            this.txtNombre.TabIndex = 21;
            this.txtNombre.Text = "Nombre producto";
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(16, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 32);
            this.label3.TabIndex = 17;
            this.label3.Text = "Precio Unitario";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtMarca
            // 
            this.txtMarca.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.txtMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtMarca.ForeColor = System.Drawing.Color.Snow;
            this.txtMarca.Location = new System.Drawing.Point(20, 257);
            this.txtMarca.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(560, 39);
            this.txtMarca.TabIndex = 11;
            this.txtMarca.Text = "Nombre marca";
            this.txtMarca.TextChanged += new System.EventHandler(this.txtMarca_TextChanged);
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Label.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label.Location = new System.Drawing.Point(8, 114);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(297, 32);
            this.Label.TabIndex = 9;
            this.Label.Text = "Nombre del producto";
            this.Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Label.Click += new System.EventHandler(this.Label_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.groupBox1.ForeColor = System.Drawing.Color.Snow;
            this.groupBox1.Location = new System.Drawing.Point(14, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(642, 1092);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar cosmético";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.pbImagen);
            this.panel2.Controls.Add(this.pb_salir);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbEstado);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dtpFecha);
            this.panel2.Controls.Add(this.spStock);
            this.panel2.Controls.Add(this.spPrecio);
            this.panel2.Controls.Add(this.cbCategoria);
            this.panel2.Controls.Add(this.label_Estado);
            this.panel2.Controls.Add(this.label_Clave);
            this.panel2.Controls.Add(this.btn_Aceptar);
            this.panel2.Controls.Add(this.txtNombre);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtMarca);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Label);
            this.panel2.Location = new System.Drawing.Point(26, 49);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(596, 1028);
            this.panel2.TabIndex = 7;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // pbImagen
            // 
            this.pbImagen.Image = global::ExamenGrupo5.Properties.Resources.icons8_add_48;
            this.pbImagen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbImagen.Location = new System.Drawing.Point(231, 15);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(110, 86);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImagen.TabIndex = 43;
            this.pbImagen.TabStop = false;
            this.pbImagen.Click += new System.EventHandler(this.pbImagen_Click);
            // 
            // pb_salir
            // 
            this.pb_salir.Image = global::ExamenGrupo5.Properties.Resources.icons8_logout_48;
            this.pb_salir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pb_salir.Location = new System.Drawing.Point(506, 36);
            this.pb_salir.Name = "pb_salir";
            this.pb_salir.Size = new System.Drawing.Size(68, 65);
            this.pb_salir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_salir.TabIndex = 42;
            this.pb_salir.TabStop = false;
            this.pb_salir.Click += new System.EventHandler(this.pbSalir_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label5.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(16, 768);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 32);
            this.label5.TabIndex = 36;
            this.label5.Text = "Estado";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // cbEstado
            // 
            this.cbEstado.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbEstado.ForeColor = System.Drawing.Color.Snow;
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbEstado.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cbEstado.Location = new System.Drawing.Point(22, 817);
            this.cbEstado.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(548, 40);
            this.cbEstado.TabIndex = 35;
            this.cbEstado.SelectedIndexChanged += new System.EventHandler(this.cbEstado_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label4.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(16, 655);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 32);
            this.label4.TabIndex = 34;
            this.label4.Text = "Categoría";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dtpFecha
            // 
            this.dtpFecha.CalendarForeColor = System.Drawing.Color.DarkGoldenrod;
            this.dtpFecha.CalendarMonthBackground = System.Drawing.SystemColors.HighlightText;
            this.dtpFecha.CalendarTitleBackColor = System.Drawing.Color.Snow;
            this.dtpFecha.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpFecha.Location = new System.Drawing.Point(22, 585);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(548, 39);
            this.dtpFecha.TabIndex = 33;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // spStock
            // 
            this.spStock.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.spStock.ForeColor = System.Drawing.Color.Snow;
            this.spStock.Location = new System.Drawing.Point(20, 468);
            this.spStock.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.spStock.Name = "spStock";
            this.spStock.Size = new System.Drawing.Size(556, 39);
            this.spStock.TabIndex = 32;
            this.spStock.ValueChanged += new System.EventHandler(this.spStock_ValueChanged);
            // 
            // spPrecio
            // 
            this.spPrecio.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.spPrecio.ForeColor = System.Drawing.Color.Snow;
            this.spPrecio.Location = new System.Drawing.Point(22, 358);
            this.spPrecio.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.spPrecio.Name = "spPrecio";
            this.spPrecio.Size = new System.Drawing.Size(556, 39);
            this.spPrecio.TabIndex = 31;
            this.spPrecio.ValueChanged += new System.EventHandler(this.spPrecio_ValueChanged);
            // 
            // cbCategoria
            // 
            this.cbCategoria.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbCategoria.ForeColor = System.Drawing.Color.Snow;
            this.cbCategoria.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbCategoria.Items.AddRange(new object[] {
            "Maquillaje",
            "Cuidado de la piel",
            "Fragancias",
            "Cabello",
            "Uñas"});
            this.cbCategoria.Location = new System.Drawing.Point(22, 705);
            this.cbCategoria.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategoria.Name = "cbCategoria";
            this.cbCategoria.Size = new System.Drawing.Size(548, 40);
            this.cbCategoria.TabIndex = 30;
            this.cbCategoria.SelectedIndexChanged += new System.EventHandler(this.cbCategoria_SelectedIndexChanged);
            // 
            // label_Estado
            // 
            this.label_Estado.AutoSize = true;
            this.label_Estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label_Estado.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label_Estado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_Estado.Location = new System.Drawing.Point(16, 523);
            this.label_Estado.Name = "label_Estado";
            this.label_Estado.Size = new System.Drawing.Size(312, 32);
            this.label_Estado.TabIndex = 29;
            this.label_Estado.Text = "Fecha de vencimiento";
            this.label_Estado.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_Estado.Click += new System.EventHandler(this.label_Estado_Click);
            // 
            // label_Clave
            // 
            this.label_Clave.AutoSize = true;
            this.label_Clave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label_Clave.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label_Clave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_Clave.Location = new System.Drawing.Point(14, 418);
            this.label_Clave.Name = "label_Clave";
            this.label_Clave.Size = new System.Drawing.Size(239, 32);
            this.label_Clave.TabIndex = 26;
            this.label_Clave.Text = "Stock disponible";
            this.label_Clave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_Clave.Click += new System.EventHandler(this.label_Clave_Click);
            // 
            // btn_Aceptar
            // 
            this.btn_Aceptar.BackColor = System.Drawing.Color.MidnightBlue;
            this.btn_Aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btn_Aceptar.ForeColor = System.Drawing.Color.Snow;
            this.btn_Aceptar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Aceptar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Aceptar.Location = new System.Drawing.Point(191, 904);
            this.btn_Aceptar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Aceptar.Name = "btn_Aceptar";
            this.btn_Aceptar.Size = new System.Drawing.Size(180, 52);
            this.btn_Aceptar.TabIndex = 25;
            this.btn_Aceptar.Text = "Aceptar";
            this.btn_Aceptar.UseVisualStyleBackColor = false;
            this.btn_Aceptar.Click += new System.EventHandler(this.btnAgregar);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(16, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "Marca";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(18, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 38);
            this.label1.TabIndex = 9;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 1096);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // VentanaAgregarCosmetico
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(668, 740);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VentanaAgregarCosmetico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "VentanaAgregarCosmetico";
            this.Load += new System.EventHandler(this.VentanaAgregarCosmetico_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_salir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPrecio)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbCategoria;
        private System.Windows.Forms.Label label_Estado;
        private System.Windows.Forms.Label label_Clave;
        private System.Windows.Forms.Button btn_Aceptar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown spStock;
        private System.Windows.Forms.NumericUpDown spPrecio;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbEstado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pb_salir;
        private System.Windows.Forms.PictureBox pbImagen;
    }
}