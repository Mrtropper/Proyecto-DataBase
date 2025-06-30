namespace modulo_seguridadBD
{
    partial class AdminitracionSistema
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRoleMagment = new System.Windows.Forms.Button();
            this.btnUserManagment = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvSistemas = new System.Windows.Forms.DataGridView();
            this.dtgUsers = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSistemas)).BeginInit();
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 46);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(683, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "User type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Security Model Desktop";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnRoleMagment);
            this.panel2.Controls.Add(this.btnUserManagment);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(135, 404);
            this.panel2.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Navigation";
            // 
            // btnRoleMagment
            // 
            this.btnRoleMagment.Location = new System.Drawing.Point(14, 299);
            this.btnRoleMagment.Name = "btnRoleMagment";
            this.btnRoleMagment.Size = new System.Drawing.Size(104, 46);
            this.btnRoleMagment.TabIndex = 3;
            this.btnRoleMagment.Text = "Role Managment";
            this.btnRoleMagment.Click += new System.EventHandler(this.btnRoleMagment_Click);
            // 
            // btnUserManagment
            // 
            this.btnUserManagment.Location = new System.Drawing.Point(15, 169);
            this.btnUserManagment.Name = "btnUserManagment";
            this.btnUserManagment.Size = new System.Drawing.Size(104, 43);
            this.btnUserManagment.TabIndex = 4;
            this.btnUserManagment.Text = "User Managment";
            this.btnUserManagment.Click += new System.EventHandler(this.btnUserManagment_Click_1);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.dgvSistemas);
            this.panel3.Controls.Add(this.dtgUsers);
            this.panel3.Location = new System.Drawing.Point(151, 92);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(623, 335);
            this.panel3.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(523, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(84, 37);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnCrearSistema_Click);
            // 
            // dgvSistemas
            // 
            this.dgvSistemas.AllowUserToAddRows = false;
            this.dgvSistemas.ColumnHeadersHeight = 29;
            this.dgvSistemas.Location = new System.Drawing.Point(13, 57);
            this.dgvSistemas.Name = "dgvSistemas";
            this.dgvSistemas.RowHeadersWidth = 51;
            this.dgvSistemas.Size = new System.Drawing.Size(594, 264);
            this.dgvSistemas.TabIndex = 1;
            this.dgvSistemas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellContentClick);
            // 
            // dtgUsers
            // 
            this.dtgUsers.ColumnHeadersHeight = 29;
            this.dtgUsers.Location = new System.Drawing.Point(13, 57);
            this.dtgUsers.Name = "dtgUsers";
            this.dtgUsers.RowHeadersWidth = 51;
            this.dtgUsers.Size = new System.Drawing.Size(594, 264);
            this.dtgUsers.TabIndex = 2;
            this.dtgUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgUsers_CellContentClick);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(147, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "User Managment";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(674, 54);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(67, 35);
            this.btnBack.TabIndex = 24;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // AdminitracionSistema
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AdminitracionSistema";
            this.Text = "VentanasSistema";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSistemas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRoleMagment;
        private System.Windows.Forms.Button btnUserManagment;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dtgUsers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvSistemas;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnBack;
    }
}
