namespace ExamenGrupo5
{
    partial class Principal
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
            this.btn_Ventas = new System.Windows.Forms.Button();
            this.btn_Compras = new System.Windows.Forms.Button();
            this.txt_Bienvienido = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Consumidores = new System.Windows.Forms.Button();
            this.btn_Cosmeticos = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pb_Salir = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pbSalir = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Salir)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSalir)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Ventas
            // 
            this.btn_Ventas.BackColor = System.Drawing.Color.MidnightBlue;
            this.btn_Ventas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Ventas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Ventas.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btn_Ventas.ForeColor = System.Drawing.Color.Snow;
            this.btn_Ventas.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Ventas.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Ventas.Location = new System.Drawing.Point(7, 294);
            this.btn_Ventas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Ventas.Name = "btn_Ventas";
            this.btn_Ventas.Size = new System.Drawing.Size(257, 80);
            this.btn_Ventas.TabIndex = 4;
            this.btn_Ventas.Text = "Ventas";
            this.btn_Ventas.UseVisualStyleBackColor = false;
            this.btn_Ventas.Click += new System.EventHandler(this.btn_Venta_Click);
            // 
            // btn_Compras
            // 
            this.btn_Compras.BackColor = System.Drawing.Color.MidnightBlue;
            this.btn_Compras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Compras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Compras.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btn_Compras.ForeColor = System.Drawing.Color.Snow;
            this.btn_Compras.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Compras.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Compras.Location = new System.Drawing.Point(2, 182);
            this.btn_Compras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Compras.Name = "btn_Compras";
            this.btn_Compras.Size = new System.Drawing.Size(259, 77);
            this.btn_Compras.TabIndex = 3;
            this.btn_Compras.Text = "Compras";
            this.btn_Compras.UseVisualStyleBackColor = false;
            this.btn_Compras.Click += new System.EventHandler(this.btn_Compras_Click);
            // 
            // txt_Bienvienido
            // 
            this.txt_Bienvienido.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txt_Bienvienido.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.txt_Bienvienido.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_Bienvienido.Location = new System.Drawing.Point(44, 52);
            this.txt_Bienvienido.Name = "txt_Bienvienido";
            this.txt_Bienvienido.Size = new System.Drawing.Size(203, 46);
            this.txt_Bienvienido.TabIndex = 0;
            this.txt_Bienvienido.Text = "Bienvenido";
            this.txt_Bienvienido.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Consumidores);
            this.groupBox1.Controls.Add(this.btn_Cosmeticos);
            this.groupBox1.Controls.Add(this.btn_Ventas);
            this.groupBox1.Controls.Add(this.btn_Compras);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.groupBox1.ForeColor = System.Drawing.Color.Snow;
            this.groupBox1.Location = new System.Drawing.Point(15, 126);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(270, 532);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accesos Directos";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Consumidores
            // 
            this.Consumidores.BackColor = System.Drawing.Color.MidnightBlue;
            this.Consumidores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Consumidores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Consumidores.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Consumidores.ForeColor = System.Drawing.Color.Snow;
            this.Consumidores.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Consumidores.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Consumidores.Location = new System.Drawing.Point(2, 415);
            this.Consumidores.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Consumidores.Name = "Consumidores";
            this.Consumidores.Size = new System.Drawing.Size(259, 77);
            this.Consumidores.TabIndex = 5;
            this.Consumidores.Text = "Consumidores";
            this.Consumidores.UseVisualStyleBackColor = false;
            this.Consumidores.Click += new System.EventHandler(this.btnConsumidores);
            // 
            // btn_Cosmeticos
            // 
            this.btn_Cosmeticos.BackColor = System.Drawing.Color.MidnightBlue;
            this.btn_Cosmeticos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Cosmeticos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cosmeticos.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btn_Cosmeticos.ForeColor = System.Drawing.Color.Snow;
            this.btn_Cosmeticos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Cosmeticos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Cosmeticos.Location = new System.Drawing.Point(6, 65);
            this.btn_Cosmeticos.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cosmeticos.Name = "btn_Cosmeticos";
            this.btn_Cosmeticos.Size = new System.Drawing.Size(253, 70);
            this.btn_Cosmeticos.TabIndex = 0;
            this.btn_Cosmeticos.Text = "Cosméticos";
            this.btn_Cosmeticos.UseVisualStyleBackColor = false;
            this.btn_Cosmeticos.Click += new System.EventHandler(this.btnCosmeticosCLick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel2.Controls.Add(this.pb_Salir);
            this.panel2.Controls.Add(this.menuStrip1);
            this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(304, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(811, 113);
            this.panel2.TabIndex = 7;
            // 
            // pb_Salir
            // 
            this.pb_Salir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pb_Salir.Location = new System.Drawing.Point(1006, 15);
            this.pb_Salir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pb_Salir.Name = "pb_Salir";
            this.pb_Salir.Size = new System.Drawing.Size(78, 69);
            this.pb_Salir.TabIndex = 0;
            this.pb_Salir.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.MidnightBlue;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(56, 56);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(811, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txt_Bienvienido);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.ForeColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(16, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 965);
            this.panel1.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Snow;
            this.panel3.Location = new System.Drawing.Point(329, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(809, 745);
            this.panel3.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pbSalir);
            this.panel4.Location = new System.Drawing.Point(326, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(815, 100);
            this.panel4.TabIndex = 0;
            // 
            // pbSalir
            // 
            this.pbSalir.Image = global::ExamenGrupo5.Properties.Resources.icons8_logout_48;
            this.pbSalir.Location = new System.Drawing.Point(711, 14);
            this.pbSalir.Name = "pbSalir";
            this.pbSalir.Size = new System.Drawing.Size(64, 70);
            this.pbSalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSalir.TabIndex = 0;
            this.pbSalir.TabStop = false;
            this.pbSalir.Click += new System.EventHandler(this.SalirClick);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1135, 739);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Principal";
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Salir)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSalir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Ventas;
        private System.Windows.Forms.Button btn_Compras;
        private System.Windows.Forms.Label txt_Bienvienido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Cosmeticos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pb_Salir;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button Consumidores;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pbSalir;
    }
}