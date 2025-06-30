namespace ExamenGrupo5
{
    partial class VentanaGestionCompras
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbProveedor = new System.Windows.Forms.ComboBox();
            this.txtIDCosmetico = new System.Windows.Forms.TextBox();
            this.numericUpDownCantidad = new System.Windows.Forms.NumericUpDown();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.pb_salir = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbEstado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaCompra = new System.Windows.Forms.DateTimePicker();
            this.spTotalCompra = new System.Windows.Forms.NumericUpDown();
            this.cbMetodoPago = new System.Windows.Forms.ComboBox();
            this.label_Estado = new System.Windows.Forms.Label();
            this.label_Clave = new System.Windows.Forms.Label();
            this.btn_Aceptar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_salir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTotalCompra)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(2, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 1096);
            this.panel1.TabIndex = 5;
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
            this.groupBox1.Text = "Agregar compra";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.cbProveedor);
            this.panel2.Controls.Add(this.txtIDCosmetico);
            this.panel2.Controls.Add(this.numericUpDownCantidad);
            this.panel2.Controls.Add(this.pbImagen);
            this.panel2.Controls.Add(this.pb_salir);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbEstado);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dtpFechaCompra);
            this.panel2.Controls.Add(this.spTotalCompra);
            this.panel2.Controls.Add(this.cbMetodoPago);
            this.panel2.Controls.Add(this.label_Estado);
            this.panel2.Controls.Add(this.label_Clave);
            this.panel2.Controls.Add(this.btn_Aceptar);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Label);
            this.panel2.Location = new System.Drawing.Point(26, 49);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(596, 1028);
            this.panel2.TabIndex = 7;
            // 
            // cbProveedor
            // 
            this.cbProveedor.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbProveedor.ForeColor = System.Drawing.Color.Snow;
            this.cbProveedor.FormattingEnabled = true;
            this.cbProveedor.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbProveedor.Items.AddRange(new object[] {
            "Internacional",
            "Nacional",
            "Distribuidor ",
            "Exclusivo",
            "Mayorista",
            "Minorista",
            "Fabricante"});
            this.cbProveedor.Location = new System.Drawing.Point(26, 457);
            this.cbProveedor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbProveedor.Name = "cbProveedor";
            this.cbProveedor.Size = new System.Drawing.Size(548, 40);
            this.cbProveedor.TabIndex = 46;
            this.cbProveedor.Text = "Seleccione";
            // 
            // txtIDCosmetico
            // 
            this.txtIDCosmetico.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.txtIDCosmetico.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtIDCosmetico.ForeColor = System.Drawing.Color.Snow;
            this.txtIDCosmetico.Location = new System.Drawing.Point(20, 797);
            this.txtIDCosmetico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIDCosmetico.Name = "txtIDCosmetico";
            this.txtIDCosmetico.Size = new System.Drawing.Size(560, 39);
            this.txtIDCosmetico.TabIndex = 45;
            // 
            // numericUpDownCantidad
            // 
            this.numericUpDownCantidad.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.numericUpDownCantidad.ForeColor = System.Drawing.Color.Snow;
            this.numericUpDownCantidad.Location = new System.Drawing.Point(26, 558);
            this.numericUpDownCantidad.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownCantidad.Name = "numericUpDownCantidad";
            this.numericUpDownCantidad.Size = new System.Drawing.Size(556, 39);
            this.numericUpDownCantidad.TabIndex = 44;
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
            this.pb_salir.Click += new System.EventHandler(this.btn_salir);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label5.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(14, 635);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 32);
            this.label5.TabIndex = 36;
            this.label5.Text = "Estado";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbEstado
            // 
            this.cbEstado.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbEstado.ForeColor = System.Drawing.Color.Snow;
            this.cbEstado.FormattingEnabled = true;
            this.cbEstado.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbEstado.Items.AddRange(new object[] {
            "Pendiente",
            "Completada",
            "Cancelada"});
            this.cbEstado.Location = new System.Drawing.Point(20, 684);
            this.cbEstado.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(548, 40);
            this.cbEstado.TabIndex = 35;
            this.cbEstado.Text = "Seleccione";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label4.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(20, 751);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 32);
            this.label4.TabIndex = 34;
            this.label4.Text = "ID cosmético";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dtpFechaCompra
            // 
            this.dtpFechaCompra.CalendarForeColor = System.Drawing.Color.DarkGoldenrod;
            this.dtpFechaCompra.CalendarMonthBackground = System.Drawing.SystemColors.HighlightText;
            this.dtpFechaCompra.CalendarTitleBackColor = System.Drawing.Color.Snow;
            this.dtpFechaCompra.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpFechaCompra.Location = new System.Drawing.Point(15, 149);
            this.dtpFechaCompra.Name = "dtpFechaCompra";
            this.dtpFechaCompra.Size = new System.Drawing.Size(548, 39);
            this.dtpFechaCompra.TabIndex = 33;
            // 
            // spTotalCompra
            // 
            this.spTotalCompra.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.spTotalCompra.ForeColor = System.Drawing.Color.Snow;
            this.spTotalCompra.Location = new System.Drawing.Point(22, 258);
            this.spTotalCompra.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.spTotalCompra.Name = "spTotalCompra";
            this.spTotalCompra.Size = new System.Drawing.Size(556, 39);
            this.spTotalCompra.TabIndex = 31;
            // 
            // cbMetodoPago
            // 
            this.cbMetodoPago.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbMetodoPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbMetodoPago.ForeColor = System.Drawing.Color.Snow;
            this.cbMetodoPago.FormattingEnabled = true;
            this.cbMetodoPago.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbMetodoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta de Crédito",
            "Tarjeta de Débito",
            "Transferencia"});
            this.cbMetodoPago.Location = new System.Drawing.Point(14, 352);
            this.cbMetodoPago.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbMetodoPago.Name = "cbMetodoPago";
            this.cbMetodoPago.Size = new System.Drawing.Size(548, 40);
            this.cbMetodoPago.TabIndex = 30;
            this.cbMetodoPago.Text = "Seleccione";
            // 
            // label_Estado
            // 
            this.label_Estado.AutoSize = true;
            this.label_Estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label_Estado.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label_Estado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_Estado.Location = new System.Drawing.Point(16, 523);
            this.label_Estado.Name = "label_Estado";
            this.label_Estado.Size = new System.Drawing.Size(321, 32);
            this.label_Estado.TabIndex = 29;
            this.label_Estado.Text = "Cantidad de productos";
            this.label_Estado.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_Clave
            // 
            this.label_Clave.AutoSize = true;
            this.label_Clave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label_Clave.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label_Clave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_Clave.Location = new System.Drawing.Point(20, 423);
            this.label_Clave.Name = "label_Clave";
            this.label_Clave.Size = new System.Drawing.Size(154, 32);
            this.label_Clave.TabIndex = 26;
            this.label_Clave.Text = "Proveedor";
            this.label_Clave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.btn_Aceptar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(9, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 32);
            this.label3.TabIndex = 17;
            this.label3.Text = "Método de pago";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(16, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "Total de compra";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Label.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label.Location = new System.Drawing.Point(8, 114);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(248, 32);
            this.Label.TabIndex = 9;
            this.Label.Text = "Fecha de compra";
            this.Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // 
            // VentanaGestionCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(659, 1106);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VentanaGestionCompras";
            this.Text = "VentanaGestionCompras";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_salir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTotalCompra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbImagen;
        private System.Windows.Forms.PictureBox pb_salir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbEstado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaCompra;
        private System.Windows.Forms.NumericUpDown spTotalCompra;
        private System.Windows.Forms.ComboBox cbMetodoPago;
        private System.Windows.Forms.Label label_Estado;
        private System.Windows.Forms.Label label_Clave;
        private System.Windows.Forms.Button btn_Aceptar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownCantidad;
        private System.Windows.Forms.TextBox txtIDCosmetico;
        private System.Windows.Forms.ComboBox cbProveedor;
    }
}