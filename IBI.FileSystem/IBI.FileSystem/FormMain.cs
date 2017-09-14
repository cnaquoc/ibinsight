using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBI.FileSystem.Helpers;

namespace IBI.FileSystem
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            HideShowMenu();
        }

        private bool IsLogOut = false;

        public void HideShowMenu()
        {
            if (!UserInfo.IsAdmin)
            {
                administratorToolStripMenuItem.Visible = false;

                if (!UserInfo.IsManager) managerToolStripMenuItem.Visible = false;
                if (!UserInfo.IsUploader) uploadFileToolStripMenuItem.Visible = false;
                if (!UserInfo.IsDataInput) dataInputToolStripMenuItem.Visible = false;
            }
        }

        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(FormUpload))
                {
                    frm.Activate();
                    return;
                }
            }
            FormUpload form = new FormUpload();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Minimized;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ControlBox = false;
            form.Text = "";
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void dataInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(FormInput))
                {
                    frm.Activate();
                    ((FormInput)frm).LoadGrid();
                    return;
                }
            }
            FormInput form = new FormInput();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Minimized;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ControlBox = false;
            form.Text = "";
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(FormManager))
                {
                    frm.Activate();
                    ((FormManager)frm).LoadTreeView();
                    return;
                }
            }
            FormManager form = new FormManager();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Minimized;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ControlBox = false;
            form.Text = "";
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(FormChangePass))
                {
                    frm.Activate();
                    return;
                }
            }
            FormChangePass form = new FormChangePass();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Minimized;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ControlBox = false;
            form.Text = "";
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsLogOut = true;
            this.Close();
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(FormLogin))
                {
                    frm.Activate();
                    frm.Show();
                    return;
                }
            }
            
        }

        

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!IsLogOut)
            {
                Application.Exit();
            }
            
        }

        private void mappingRoleClassifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(FormRoleClassify))
                {
                    frm.Activate();
                    return;
                }
            }
            FormRoleClassify form = new FormRoleClassify();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Minimized;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ControlBox = false;
            form.Text = "";
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void manageClassifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(FormClassify))
                {
                    frm.Activate();
                    return;
                }
            }
            FormClassify form = new FormClassify();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Minimized;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ControlBox = false;
            form.Text = "";
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void mappingRoleUserGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(FormRoleUsergroup))
                {
                    frm.Activate();
                    return;
                }
            }
            FormRoleUsergroup form = new FormRoleUsergroup();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Minimized;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ControlBox = false;
            form.Text = "";
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
