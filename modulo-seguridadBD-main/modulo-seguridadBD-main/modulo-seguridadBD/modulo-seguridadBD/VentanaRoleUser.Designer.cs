namespace modulo_seguridadBD
{
    partial class VentanaRoleUser
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
            this.button11 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.btnRoleAssignment = new System.Windows.Forms.Button();
            this.btnRoleMagment = new System.Windows.Forms.Button();
            this.btnUserManagment = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnViewStory = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNombreUsuario = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvCurrentRole = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvAvailableRole = new System.Windows.Forms.DataGridView();
            this.label20 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentRole)).BeginInit();
            this.panel8.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableRole)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(767, 58);
            this.panel1.TabIndex = 2;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button11.ForeColor = System.Drawing.SystemColors.Control;
            this.button11.Location = new System.Drawing.Point(644, 15);
            this.button11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(118, 29);
            this.button11.TabIndex = 11;
            this.button11.Text = "New user";
            this.button11.UseVisualStyleBackColor = false;
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
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.btnRoleAssignment);
            this.panel2.Controls.Add(this.btnRoleMagment);
            this.panel2.Controls.Add(this.btnUserManagment);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 824);
            this.panel2.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(18, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 28);
            this.label5.TabIndex = 5;
            this.label5.Text = "Navigation";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(22, 412);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(107, 64);
            this.button5.TabIndex = 4;
            this.button5.Text = "User Permissions";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnRoleAssignment
            // 
            this.btnRoleAssignment.Location = new System.Drawing.Point(22, 319);
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
            this.btnRoleMagment.Location = new System.Drawing.Point(22, 221);
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
            this.btnUserManagment.Location = new System.Drawing.Point(22, 130);
            this.btnUserManagment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUserManagment.Name = "btnUserManagment";
            this.btnUserManagment.Size = new System.Drawing.Size(107, 64);
            this.btnUserManagment.TabIndex = 1;
            this.btnUserManagment.Text = "User Managment";
            this.btnUserManagment.UseVisualStyleBackColor = true;
            this.btnUserManagment.Click += new System.EventHandler(this.btnUserManagment_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(159, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "User Roles Assignment";
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSaveChanges.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSaveChanges.Location = new System.Drawing.Point(767, 82);
            this.btnSaveChanges.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(118, 29);
            this.btnSaveChanges.TabIndex = 9;
            this.btnSaveChanges.Text = "Manage Role";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            // 
            // btnViewStory
            // 
            this.btnViewStory.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnViewStory.Location = new System.Drawing.Point(628, 82);
            this.btnViewStory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnViewStory.Name = "btnViewStory";
            this.btnViewStory.Size = new System.Drawing.Size(118, 29);
            this.btnViewStory.TabIndex = 8;
            this.btnViewStory.Text = "Back To Users";
            this.btnViewStory.UseVisualStyleBackColor = false;
            this.btnViewStory.Click += new System.EventHandler(this.btnViewStory_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.txtNombreUsuario);
            this.panel3.Location = new System.Drawing.Point(281, 134);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(332, 70);
            this.panel3.TabIndex = 10;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.AutoSize = true;
            this.txtNombreUsuario.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreUsuario.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtNombreUsuario.Location = new System.Drawing.Point(93, 21);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(178, 28);
            this.txtNombreUsuario.TabIndex = 9;
            this.txtNombreUsuario.Text = "Elmer Rodriguez";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel4.Controls.Add(this.flowLayoutPanel1);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(202, 529);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(520, 306);
            this.panel4.TabIndex = 11;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.dgvCurrentRole);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 58);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(513, 235);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // dgvCurrentRole
            // 
            this.dgvCurrentRole.AllowUserToAddRows = false;
            this.dgvCurrentRole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentRole.Location = new System.Drawing.Point(3, 4);
            this.dgvCurrentRole.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvCurrentRole.Name = "dgvCurrentRole";
            this.dgvCurrentRole.RowHeadersWidth = 51;
            this.dgvCurrentRole.RowTemplate.Height = 24;
            this.dgvCurrentRole.Size = new System.Drawing.Size(510, 235);
            this.dgvCurrentRole.TabIndex = 1;
            this.dgvCurrentRole.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCurrentRole_CellContentClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(10, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 28);
            this.label4.TabIndex = 9;
            this.label4.Text = "Current Roles";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel8.Controls.Add(this.flowLayoutPanel2);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Location = new System.Drawing.Point(202, 215);
            this.panel8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(520, 306);
            this.panel8.TabIndex = 12;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.dgvAvailableRole);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 58);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(513, 235);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // dgvAvailableRole
            // 
            this.dgvAvailableRole.AllowUserToAddRows = false;
            this.dgvAvailableRole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableRole.Location = new System.Drawing.Point(3, 4);
            this.dgvAvailableRole.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvAvailableRole.Name = "dgvAvailableRole";
            this.dgvAvailableRole.RowHeadersWidth = 51;
            this.dgvAvailableRole.RowTemplate.Height = 24;
            this.dgvAvailableRole.Size = new System.Drawing.Size(510, 235);
            this.dgvAvailableRole.TabIndex = 0;
            this.dgvAvailableRole.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAvailableRole_CellContentClick);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label20.Location = new System.Drawing.Point(10, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(166, 28);
            this.label20.TabIndex = 9;
            this.label20.Text = "Available Roles";
            // 
            // VentanaRoleUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 882);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnViewStory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VentanaRoleUser";
            this.Text = "VentanaRoleUser";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentRole)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableRole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnRoleAssignment;
        private System.Windows.Forms.Button btnRoleMagment;
        private System.Windows.Forms.Button btnUserManagment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnViewStory;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label txtNombreUsuario;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.DataGridView dgvCurrentRole;
        private System.Windows.Forms.DataGridView dgvAvailableRole;
    }
}