﻿namespace IBI.ScheduleService
{
    partial class frmMain
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
            this.btnRun = new System.Windows.Forms.Button();
            this.webBrowserHose = new System.Windows.Forms.WebBrowser();
            this.timerHose = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerHnx = new System.Windows.Forms.Timer(this.components);
            this.timerUpcom = new System.Windows.Forms.Timer(this.components);
            this.webBrowserHnx = new System.Windows.Forms.WebBrowser();
            this.webBrowserUpcom = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(335, 260);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 34);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // webBrowserHose
            // 
            this.webBrowserHose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserHose.Location = new System.Drawing.Point(12, 37);
            this.webBrowserHose.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHose.Name = "webBrowserHose";
            this.webBrowserHose.ScriptErrorsSuppressed = true;
            this.webBrowserHose.Size = new System.Drawing.Size(418, 43);
            this.webBrowserHose.TabIndex = 2;
            this.webBrowserHose.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserHose_DocumentCompleted);
            // 
            // timerHose
            // 
            this.timerHose.Tick += new System.EventHandler(this.timerHose_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(335, 300);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(30, 281);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(50, 13);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Website screen";
            // 
            // timerHnx
            // 
            this.timerHnx.Tick += new System.EventHandler(this.timerHnx_Tick);
            // 
            // timerUpcom
            // 
            this.timerUpcom.Tick += new System.EventHandler(this.timerUpcom_Tick);
            // 
            // webBrowserHnx
            // 
            this.webBrowserHnx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserHnx.Location = new System.Drawing.Point(12, 100);
            this.webBrowserHnx.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHnx.Name = "webBrowserHnx";
            this.webBrowserHnx.ScriptErrorsSuppressed = true;
            this.webBrowserHnx.Size = new System.Drawing.Size(418, 43);
            this.webBrowserHnx.TabIndex = 2;
            this.webBrowserHnx.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserHnx_DocumentCompleted);
            // 
            // webBrowserUpcom
            // 
            this.webBrowserUpcom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserUpcom.Location = new System.Drawing.Point(12, 158);
            this.webBrowserUpcom.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserUpcom.Name = "webBrowserUpcom";
            this.webBrowserUpcom.ScriptErrorsSuppressed = true;
            this.webBrowserUpcom.Size = new System.Drawing.Size(418, 43);
            this.webBrowserUpcom.TabIndex = 2;
            this.webBrowserUpcom.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserUpcom_DocumentCompleted);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 339);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.webBrowserUpcom);
            this.Controls.Add(this.webBrowserHnx);
            this.Controls.Add(this.webBrowserHose);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Schedule service";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.WebBrowser webBrowserHose;
        private System.Windows.Forms.Timer timerHose;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerHnx;
        private System.Windows.Forms.Timer timerUpcom;
        private System.Windows.Forms.WebBrowser webBrowserHnx;
        private System.Windows.Forms.WebBrowser webBrowserUpcom;
    }
}

