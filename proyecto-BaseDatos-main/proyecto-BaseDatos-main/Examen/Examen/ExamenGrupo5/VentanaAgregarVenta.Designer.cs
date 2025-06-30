namespace ExamenGrupo5
{
    partial class VentanaAgregarVenta
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbIdConsumidor = new System.Windows.Forms.ComboBox();
            this.cbIdCosmetico = new System.Windows.Forms.ComboBox();
            this.txtPrecioTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pkCantidadVendidos = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.pkPuntosUsados = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaVenta = new System.Windows.Forms.DateTimePicker();
            this.pb_salir = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbEstadoVentas = new System.Windows.Forms.ComboBox();
            this.cbMetodoPago = new System.Windows.Forms.ComboBox();
            this.label_Clave = new System.Windows.Forms.Label();
            this.btn_Aceptar = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pkCantidadVendidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pkPuntosUsados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_salir)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(606, 1032);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.groupBox1.ForeColor = System.Drawing.Color.Snow;
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox1.Size = new System.Drawing.Size(567, 1008);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar Venta";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.cbIdConsumidor);
            this.panel2.Controls.Add(this.cbIdCosmetico);
            this.panel2.Controls.Add(this.txtPrecioTotal);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.pkCantidadVendidos);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pkPuntosUsados);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpFechaVenta);
            this.panel2.Controls.Add(this.pb_salir);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbEstadoVentas);
            this.panel2.Controls.Add(this.cbMetodoPago);
            this.panel2.Controls.Add(this.label_Clave);
            this.panel2.Controls.Add(this.btn_Aceptar);
            this.panel2.Controls.Add(this.Label);
            this.panel2.Location = new System.Drawing.Point(23, 39);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(529, 936);
            this.panel2.TabIndex = 7;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // cbIdConsumidor
            // 
            this.cbIdConsumidor.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbIdConsumidor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbIdConsumidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbIdConsumidor.ForeColor = System.Drawing.Color.Snow;
            this.cbIdConsumidor.FormattingEnabled = true;
            this.cbIdConsumidor.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbIdConsumidor.Items.AddRange(new object[] {
            "Efectivo, Tarjeta de Crédito, Tarjeta de Débito, Transferencia, Puntos"});
            this.cbIdConsumidor.Location = new System.Drawing.Point(10, 612);
            this.cbIdConsumidor.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.cbIdConsumidor.Name = "cbIdConsumidor";
            this.cbIdConsumidor.Size = new System.Drawing.Size(495, 37);
            this.cbIdConsumidor.TabIndex = 55;
            // 
            // cbIdCosmetico
            // 
            this.cbIdCosmetico.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbIdCosmetico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIdCosmetico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbIdCosmetico.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbIdCosmetico.ForeColor = System.Drawing.Color.Snow;
            this.cbIdCosmetico.FormattingEnabled = true;
            this.cbIdCosmetico.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbIdCosmetico.Items.AddRange(new object[] {
            "Efectivo, Tarjeta de Crédito, Tarjeta de Débito, Transferencia, Puntos"});
            this.cbIdCosmetico.Location = new System.Drawing.Point(13, 512);
            this.cbIdCosmetico.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.cbIdCosmetico.Name = "cbIdCosmetico";
            this.cbIdCosmetico.Size = new System.Drawing.Size(495, 37);
            this.cbIdCosmetico.TabIndex = 54;
            // 
            // txtPrecioTotal
            // 
            this.txtPrecioTotal.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.txtPrecioTotal.Enabled = false;
            this.txtPrecioTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txtPrecioTotal.ForeColor = System.Drawing.Color.Snow;
            this.txtPrecioTotal.Location = new System.Drawing.Point(12, 811);
            this.txtPrecioTotal.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.txtPrecioTotal.Name = "txtPrecioTotal";
            this.txtPrecioTotal.Size = new System.Drawing.Size(488, 34);
            this.txtPrecioTotal.TabIndex = 53;
            this.txtPrecioTotal.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(3, 770);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 29);
            this.label3.TabIndex = 52;
            this.label3.Text = "Total de la venta";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label8.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(12, 566);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(230, 29);
            this.label8.TabIndex = 50;
            this.label8.Text = "ID del Consumidor";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label7.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(7, 469);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(213, 29);
            this.label7.TabIndex = 50;
            this.label7.Text = "ID del Cosmetico";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pkCantidadVendidos
            // 
            this.pkCantidadVendidos.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.pkCantidadVendidos.ForeColor = System.Drawing.Color.Snow;
            this.pkCantidadVendidos.Location = new System.Drawing.Point(13, 421);
            this.pkCantidadVendidos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pkCantidadVendidos.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.pkCantidadVendidos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pkCantidadVendidos.Name = "pkCantidadVendidos";
            this.pkCantidadVendidos.Size = new System.Drawing.Size(487, 34);
            this.pkCantidadVendidos.TabIndex = 49;
            this.pkCantidadVendidos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pkCantidadVendidos.ValueChanged += new System.EventHandler(this.pkCantidadVendidos_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label4.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(8, 380);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(234, 29);
            this.label4.TabIndex = 48;
            this.label4.Text = "Cantidad Vendidos";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pkPuntosUsados
            // 
            this.pkPuntosUsados.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.pkPuntosUsados.ForeColor = System.Drawing.Color.Snow;
            this.pkPuntosUsados.Location = new System.Drawing.Point(13, 328);
            this.pkPuntosUsados.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pkPuntosUsados.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.pkPuntosUsados.Name = "pkPuntosUsados";
            this.pkPuntosUsados.Size = new System.Drawing.Size(487, 34);
            this.pkPuntosUsados.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(8, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 29);
            this.label2.TabIndex = 46;
            this.label2.Text = "Puntos Usados";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dtpFechaVenta
            // 
            this.dtpFechaVenta.CalendarForeColor = System.Drawing.Color.DarkGoldenrod;
            this.dtpFechaVenta.CalendarMonthBackground = System.Drawing.SystemColors.HighlightText;
            this.dtpFechaVenta.CalendarTitleBackColor = System.Drawing.Color.Snow;
            this.dtpFechaVenta.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpFechaVenta.Location = new System.Drawing.Point(12, 131);
            this.dtpFechaVenta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpFechaVenta.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpFechaVenta.Name = "dtpFechaVenta";
            this.dtpFechaVenta.Size = new System.Drawing.Size(493, 34);
            this.dtpFechaVenta.TabIndex = 45;
            // 
            // pb_salir
            // 
            this.pb_salir.Image = global::ExamenGrupo5.Properties.Resources.icons8_logout_48;
            this.pb_salir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pb_salir.Location = new System.Drawing.Point(472, 2);
            this.pb_salir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_salir.Name = "pb_salir";
            this.pb_salir.Size = new System.Drawing.Size(39, 40);
            this.pb_salir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_salir.TabIndex = 42;
            this.pb_salir.TabStop = false;
            this.pb_salir.Click += new System.EventHandler(this.pb_salir_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label5.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(12, 664);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(254, 29);
            this.label5.TabIndex = 36;
            this.label5.Text = "Estado de las ventas";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbEstadoVentas
            // 
            this.cbEstadoVentas.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbEstadoVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstadoVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEstadoVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbEstadoVentas.ForeColor = System.Drawing.Color.Snow;
            this.cbEstadoVentas.FormattingEnabled = true;
            this.cbEstadoVentas.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbEstadoVentas.Items.AddRange(new object[] {
            "Pendiente",
            "Completada",
            "Cancelada"});
            this.cbEstadoVentas.Location = new System.Drawing.Point(13, 712);
            this.cbEstadoVentas.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.cbEstadoVentas.Name = "cbEstadoVentas";
            this.cbEstadoVentas.Size = new System.Drawing.Size(488, 37);
            this.cbEstadoVentas.TabIndex = 35;
            // 
            // cbMetodoPago
            // 
            this.cbMetodoPago.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.cbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMetodoPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.cbMetodoPago.ForeColor = System.Drawing.Color.Snow;
            this.cbMetodoPago.FormattingEnabled = true;
            this.cbMetodoPago.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbMetodoPago.Items.AddRange(new object[] {
            "Efectivo ",
            "Tarjeta de Crédito ",
            "Tarjeta de Débito ",
            "Transferencia ",
            "Puntos"});
            this.cbMetodoPago.Location = new System.Drawing.Point(13, 224);
            this.cbMetodoPago.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.cbMetodoPago.Name = "cbMetodoPago";
            this.cbMetodoPago.Size = new System.Drawing.Size(495, 37);
            this.cbMetodoPago.TabIndex = 30;
            // 
            // label_Clave
            // 
            this.label_Clave.AutoSize = true;
            this.label_Clave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label_Clave.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label_Clave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_Clave.Location = new System.Drawing.Point(12, 182);
            this.label_Clave.Name = "label_Clave";
            this.label_Clave.Size = new System.Drawing.Size(169, 29);
            this.label_Clave.TabIndex = 26;
            this.label_Clave.Text = "Metodo Pago";
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
            this.btn_Aceptar.Location = new System.Drawing.Point(143, 869);
            this.btn_Aceptar.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btn_Aceptar.Name = "btn_Aceptar";
            this.btn_Aceptar.Size = new System.Drawing.Size(212, 42);
            this.btn_Aceptar.TabIndex = 25;
            this.btn_Aceptar.Text = "Aceptar";
            this.btn_Aceptar.UseVisualStyleBackColor = false;
            this.btn_Aceptar.Click += new System.EventHandler(this.btn_aceptar);
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Label.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.Label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label.Location = new System.Drawing.Point(7, 91);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(219, 29);
            this.Label.TabIndex = 9;
            this.Label.Text = "Fecha de la venta";
            this.Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(16, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 32);
            this.label1.TabIndex = 9;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VentanaAgregarVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(614, 888);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VentanaAgregarVenta";
            this.Load += new System.EventHandler(this.VentanaAgregarVenta_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pkCantidadVendidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pkPuntosUsados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_salir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pb_salir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbEstadoVentas;
        private System.Windows.Forms.ComboBox cbMetodoPago;
        private System.Windows.Forms.Label label_Clave;
        private System.Windows.Forms.Button btn_Aceptar;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaVenta;
        private System.Windows.Forms.NumericUpDown pkPuntosUsados;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown pkCantidadVendidos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPrecioTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbIdConsumidor;
        private System.Windows.Forms.ComboBox cbIdCosmetico;
    }
}
