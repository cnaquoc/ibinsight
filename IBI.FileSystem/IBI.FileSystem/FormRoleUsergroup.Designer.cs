namespace IBI.FileSystem
{
    partial class FormRoleUsergroup
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
            this.cbbRole = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbUsergroup = new System.Windows.Forms.ComboBox();
            this.btnMapping = new System.Windows.Forms.Button();
            this.dataGridViewMapping = new System.Windows.Forms.DataGridView();
            this.groupBoxMapping = new System.Windows.Forms.GroupBox();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMapping)).BeginInit();
            this.groupBoxMapping.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbRole
            // 
            this.cbbRole.FormattingEnabled = true;
            this.cbbRole.Location = new System.Drawing.Point(52, 31);
            this.cbbRole.Name = "cbbRole";
            this.cbbRole.Size = new System.Drawing.Size(291, 21);
            this.cbbRole.TabIndex = 0;
            this.cbbRole.SelectedIndexChanged += new System.EventHandler(this.cbbRole_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Role:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(388, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "User group:";
            // 
            // cbbUsergroup
            // 
            this.cbbUsergroup.FormattingEnabled = true;
            this.cbbUsergroup.Location = new System.Drawing.Point(452, 31);
            this.cbbUsergroup.Name = "cbbUsergroup";
            this.cbbUsergroup.Size = new System.Drawing.Size(291, 21);
            this.cbbUsergroup.TabIndex = 2;
            // 
            // btnMapping
            // 
            this.btnMapping.Location = new System.Drawing.Point(346, 64);
            this.btnMapping.Name = "btnMapping";
            this.btnMapping.Size = new System.Drawing.Size(102, 36);
            this.btnMapping.TabIndex = 4;
            this.btnMapping.Text = "Mapping";
            this.btnMapping.UseVisualStyleBackColor = true;
            this.btnMapping.Click += new System.EventHandler(this.btnMapping_Click);
            // 
            // dataGridViewMapping
            // 
            this.dataGridViewMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMapping.Location = new System.Drawing.Point(17, 19);
            this.dataGridViewMapping.Name = "dataGridViewMapping";
            this.dataGridViewMapping.Size = new System.Drawing.Size(726, 378);
            this.dataGridViewMapping.TabIndex = 5;
            this.dataGridViewMapping.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMapping_CellContentClick);
            // 
            // groupBoxMapping
            // 
            this.groupBoxMapping.Controls.Add(this.btnMapping);
            this.groupBoxMapping.Controls.Add(this.cbbRole);
            this.groupBoxMapping.Controls.Add(this.label1);
            this.groupBoxMapping.Controls.Add(this.label2);
            this.groupBoxMapping.Controls.Add(this.cbbUsergroup);
            this.groupBoxMapping.Location = new System.Drawing.Point(18, 35);
            this.groupBoxMapping.Name = "groupBoxMapping";
            this.groupBoxMapping.Size = new System.Drawing.Size(759, 108);
            this.groupBoxMapping.TabIndex = 6;
            this.groupBoxMapping.TabStop = false;
            this.groupBoxMapping.Text = "Mapping Role - Usergroup";
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Controls.Add(this.dataGridViewMapping);
            this.groupBoxDetails.Location = new System.Drawing.Point(18, 154);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(759, 403);
            this.groupBoxDetails.TabIndex = 7;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Mapping list";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::IBI.FileSystem.Properties.Resources.Close;
            this.btnClose.Location = new System.Drawing.Point(677, 564);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 36);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormRoleUsergroup
            // 
            this.AcceptButton = this.btnMapping;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 602);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(this.groupBoxMapping);
            this.Name = "FormRoleUsergroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Role Usergroup";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMapping)).EndInit();
            this.groupBoxMapping.ResumeLayout(false);
            this.groupBoxMapping.PerformLayout();
            this.groupBoxDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbRole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbUsergroup;
        private System.Windows.Forms.Button btnMapping;
        private System.Windows.Forms.DataGridView dataGridViewMapping;
        private System.Windows.Forms.GroupBox groupBoxMapping;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.Button btnClose;
    }
}