namespace modulo_seguridadBD
{
    partial class VentanaAdministrador
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnSistemas = new System.Windows.Forms.Button();
            this.btnVentanaSistema = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUserPermissions = new System.Windows.Forms.Button();
            this.btnRoleAssignment = new System.Windows.Forms.Button();
            this.btnRoleMagment = new System.Windows.Forms.Button();
            this.btnUserManagment = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.botonAgregar = new System.Windows.Forms.Button();
            this.cmbSistemas = new System.Windows.Forms.ComboBox();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.dtgUsers = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 58);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(768, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "User type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Security Model Desktop";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnSistemas);
            this.panel2.Controls.Add(this.btnVentanaSistema);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnUserPermissions);
            this.panel2.Controls.Add(this.btnRoleAssignment);
            this.panel2.Controls.Add(this.btnRoleMagment);
            this.panel2.Controls.Add(this.btnUserManagment);
            this.panel2.Controls.Add(this.btnLogin);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 520);
            this.panel2.TabIndex = 1;
            // 
            // BtnSistemas
            // 
            this.BtnSistemas.Location = new System.Drawing.Point(18, 438);
            this.BtnSistemas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSistemas.Name = "BtnSistemas";
            this.BtnSistemas.Size = new System.Drawing.Size(107, 64);
            this.BtnSistemas.TabIndex = 8;
            this.BtnSistemas.Text = "Systems";
            this.BtnSistemas.UseVisualStyleBackColor = true;
            this.BtnSistemas.Click += new System.EventHandler(this.BtnSistemas_Click);
            // 
            // btnVentanaSistema
            // 
            this.btnVentanaSistema.Location = new System.Drawing.Point(18, 366);
            this.btnVentanaSistema.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnVentanaSistema.Name = "btnVentanaSistema";
            this.btnVentanaSistema.Size = new System.Drawing.Size(107, 64);
            this.btnVentanaSistema.TabIndex = 7;
            this.btnVentanaSistema.Text = "Screen System";
            this.btnVentanaSistema.UseVisualStyleBackColor = true;
            this.btnVentanaSistema.Click += new System.EventHandler(this.btnVentanaSistema_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(18, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 28);
            this.label5.TabIndex = 6;
            this.label5.Text = "Navigation";
            // 
            // btnUserPermissions
            // 
            this.btnUserPermissions.Location = new System.Drawing.Point(22, 296);
            this.btnUserPermissions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUserPermissions.Name = "btnUserPermissions";
            this.btnUserPermissions.Size = new System.Drawing.Size(107, 64);
            this.btnUserPermissions.TabIndex = 4;
            this.btnUserPermissions.Text = "User Permissions";
            this.btnUserPermissions.UseVisualStyleBackColor = true;
            this.btnUserPermissions.Click += new System.EventHandler(this.btnUserPermissions_Click);
            // 
            // btnRoleAssignment
            // 
            this.btnRoleAssignment.Location = new System.Drawing.Point(22, 225);
            this.btnRoleAssignment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRoleAssignment.Name = "btnRoleAssignment";
            this.btnRoleAssignment.Size = new System.Drawing.Size(107, 64);
            this.btnRoleAssignment.TabIndex = 3;
            this.btnRoleAssignment.Text = "Role Assignment";
            this.btnRoleAssignment.UseVisualStyleBackColor = true;
            this.btnRoleAssignment.Click += new System.EventHandler(this.btnRoleAssignment_Click);
            // 
            // btnRoleMagment
            // 
            this.btnRoleMagment.Location = new System.Drawing.Point(22, 154);
            this.btnRoleMagment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRoleMagment.Name = "btnRoleMagment";
            this.btnRoleMagment.Size = new System.Drawing.Size(107, 64);
            this.btnRoleMagment.TabIndex = 2;
            this.btnRoleMagment.Text = "Role Managment";
            this.btnRoleMagment.UseVisualStyleBackColor = true;
            this.btnRoleMagment.Click += new System.EventHandler(this.btnRoleMagment_Click);
            // 
            // btnUserManagment
            // 
            this.btnUserManagment.Location = new System.Drawing.Point(22, 82);
            this.btnUserManagment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUserManagment.Name = "btnUserManagment";
            this.btnUserManagment.Size = new System.Drawing.Size(107, 64);
            this.btnUserManagment.TabIndex = 1;
            this.btnUserManagment.Text = "User Managment";
            this.btnUserManagment.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(22, 46);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(107, 29);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click_1);
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtNombreUsuario.Location = new System.Drawing.Point(15, 19);
            this.txtNombreUsuario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(276, 26);
            this.txtNombreUsuario.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.botonAgregar);
            this.panel3.Controls.Add(this.cmbSistemas);
            this.panel3.Controls.Add(this.dgvUsuarios);
            this.panel3.Controls.Add(this.dtgUsers);
            this.panel3.Controls.Add(this.txtNombreUsuario);
            this.panel3.Location = new System.Drawing.Point(170, 115);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(701, 419);
            this.panel3.TabIndex = 3;
            // 
            // botonAgregar
            // 
            this.botonAgregar.Location = new System.Drawing.Point(580, 9);
            this.botonAgregar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.botonAgregar.Name = "botonAgregar";
            this.botonAgregar.Size = new System.Drawing.Size(102, 48);
            this.botonAgregar.TabIndex = 6;
            this.botonAgregar.Text = "ADD Button";
            this.botonAgregar.UseVisualStyleBackColor = true;
            this.botonAgregar.Click += new System.EventHandler(this.AgregarUsuario_Click);
            // 
            // cmbSistemas
            // 
            this.cmbSistemas.FormattingEnabled = true;
            this.cmbSistemas.Location = new System.Drawing.Point(418, 15);
            this.cmbSistemas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSistemas.Name = "cmbSistemas";
            this.cmbSistemas.Size = new System.Drawing.Size(136, 28);
            this.cmbSistemas.TabIndex = 5;
            this.cmbSistemas.SelectedIndexChanged += new System.EventHandler(this.cmbSistemas_SelectedIndexChanged);
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Location = new System.Drawing.Point(15, 71);
            this.dgvUsuarios.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.RowHeadersWidth = 51;
            this.dgvUsuarios.RowTemplate.Height = 24;
            this.dgvUsuarios.Size = new System.Drawing.Size(668, 330);
            this.dgvUsuarios.TabIndex = 4;
            this.dgvUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);
            this.dgvUsuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellContentClick);
            // 
            // dtgUsers
            // 
            this.dtgUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgUsers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtgUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgUsers.EnableHeadersVisualStyles = false;
            this.dtgUsers.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dtgUsers.Location = new System.Drawing.Point(15, 71);
            this.dtgUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtgUsers.Name = "dtgUsers";
            this.dtgUsers.RowHeadersWidth = 51;
            this.dtgUsers.RowTemplate.Height = 24;
            this.dtgUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgUsers.Size = new System.Drawing.Size(668, 330);
            this.dtgUsers.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(165, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Managment";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(804, 65);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(67, 35);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // VentanaAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 578);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VentanaAdministrador";
            this.Text = "VentanaAdministrador";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRoleMagment;
        private System.Windows.Forms.Button btnUserManagment;
        private System.Windows.Forms.Button btnUserPermissions;
        private System.Windows.Forms.Button btnRoleAssignment;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dtgUsers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.ComboBox cmbSistemas;
        private System.Windows.Forms.Button btnVentanaSistema;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button botonAgregar;
        private System.Windows.Forms.Button BtnSistemas;
        private System.Windows.Forms.Button btnBack;
    }
}