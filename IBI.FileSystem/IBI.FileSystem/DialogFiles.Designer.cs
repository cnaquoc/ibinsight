namespace IBI.FileSystem
{
    partial class DialogFiles
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogFiles));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.richTextBoxSuccess = new System.Windows.Forms.RichTextBox();
            this.richTextBoxError = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 22);
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(595, 384);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Green.ico");
            this.imageList1.Images.SetKeyName(1, "Red.ico");
            // 
            // btnUpload
            // 
            this.btnUpload.Image = global::IBI.FileSystem.Properties.Resources.upload;
            this.btnUpload.Location = new System.Drawing.Point(523, 424);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(84, 36);
            this.btnUpload.TabIndex = 8;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::IBI.FileSystem.Properties.Resources.Close;
            this.btnClose.Location = new System.Drawing.Point(523, 477);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 36);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // richTextBoxSuccess
            // 
            this.richTextBoxSuccess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.richTextBoxSuccess.Location = new System.Drawing.Point(12, 412);
            this.richTextBoxSuccess.Name = "richTextBoxSuccess";
            this.richTextBoxSuccess.Size = new System.Drawing.Size(505, 48);
            this.richTextBoxSuccess.TabIndex = 10;
            this.richTextBoxSuccess.Text = "";
            // 
            // richTextBoxError
            // 
            this.richTextBoxError.ForeColor = System.Drawing.Color.Red;
            this.richTextBoxError.Location = new System.Drawing.Point(12, 466);
            this.richTextBoxError.Name = "richTextBoxError";
            this.richTextBoxError.Size = new System.Drawing.Size(505, 48);
            this.richTextBoxError.TabIndex = 11;
            this.richTextBoxError.Text = "";
            // 
            // DialogFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 525);
            this.Controls.Add(this.richTextBoxError);
            this.Controls.Add(this.richTextBoxSuccess);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DialogFiles";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select file dialog";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RichTextBox richTextBoxSuccess;
        private System.Windows.Forms.RichTextBox richTextBoxError;
    }
}