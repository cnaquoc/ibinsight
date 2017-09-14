namespace IBI.FileSystem
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageClassifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mappingRoleClassifyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mappingRoleUserGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadFileToolStripMenuItem,
            this.dataInputToolStripMenuItem,
            this.managerToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.administratorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // uploadFileToolStripMenuItem
            // 
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.uploadFileToolStripMenuItem.Text = "&Upload file";
            this.uploadFileToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // dataInputToolStripMenuItem
            // 
            this.dataInputToolStripMenuItem.Name = "dataInputToolStripMenuItem";
            this.dataInputToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.dataInputToolStripMenuItem.Text = "Data &Input";
            this.dataInputToolStripMenuItem.Click += new System.EventHandler(this.dataInputToolStripMenuItem_Click);
            // 
            // managerToolStripMenuItem
            // 
            this.managerToolStripMenuItem.Name = "managerToolStripMenuItem";
            this.managerToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.managerToolStripMenuItem.Text = "&Manager";
            this.managerToolStripMenuItem.Click += new System.EventHandler(this.managerToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem1,
            this.logOutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.changePasswordToolStripMenuItem.Text = "A&ccount";
            // 
            // changePasswordToolStripMenuItem1
            // 
            this.changePasswordToolStripMenuItem1.Name = "changePasswordToolStripMenuItem1";
            this.changePasswordToolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.changePasswordToolStripMenuItem1.Text = "&Change password";
            this.changePasswordToolStripMenuItem1.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.logOutToolStripMenuItem.Text = "&Log out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // administratorToolStripMenuItem
            // 
            this.administratorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageClassifyToolStripMenuItem,
            this.mappingRoleClassifyToolStripMenuItem1,
            this.mappingRoleUserGroupToolStripMenuItem});
            this.administratorToolStripMenuItem.Name = "administratorToolStripMenuItem";
            this.administratorToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.administratorToolStripMenuItem.Text = "&Administrator";
            // 
            // manageClassifyToolStripMenuItem
            // 
            this.manageClassifyToolStripMenuItem.Name = "manageClassifyToolStripMenuItem";
            this.manageClassifyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.manageClassifyToolStripMenuItem.Text = "Manage &Classify";
            this.manageClassifyToolStripMenuItem.Click += new System.EventHandler(this.manageClassifyToolStripMenuItem_Click);
            // 
            // mappingRoleClassifyToolStripMenuItem1
            // 
            this.mappingRoleClassifyToolStripMenuItem1.Name = "mappingRoleClassifyToolStripMenuItem1";
            this.mappingRoleClassifyToolStripMenuItem1.Size = new System.Drawing.Size(215, 22);
            this.mappingRoleClassifyToolStripMenuItem1.Text = "Mapping &Role - Classify";
            this.mappingRoleClassifyToolStripMenuItem1.Click += new System.EventHandler(this.mappingRoleClassifyToolStripMenuItem_Click);
            // 
            // mappingRoleUserGroupToolStripMenuItem
            // 
            this.mappingRoleUserGroupToolStripMenuItem.Name = "mappingRoleUserGroupToolStripMenuItem";
            this.mappingRoleUserGroupToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.mappingRoleUserGroupToolStripMenuItem.Text = "Mapping Role - &UserGroup";
            this.mappingRoleUserGroupToolStripMenuItem.Click += new System.EventHandler(this.mappingRoleUserGroupToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 512);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API File system";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mappingRoleClassifyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mappingRoleUserGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageClassifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}