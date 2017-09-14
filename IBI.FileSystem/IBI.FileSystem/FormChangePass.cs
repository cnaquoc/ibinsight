using IBI.FileSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBI.FileSystem
{
    public partial class FormChangePass : Form
    {
        private DataClasses_LocalDataContext db = null;
        public FormChangePass()
        {
            InitializeComponent();
            lblMessage.Text = "";
            db = DB_Singleton.GetDatabase();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            string error = ValidateInput();
            if (string.IsNullOrEmpty(error))
            {
                var user = db.Local_AspNetUsers.Where(t => t.Id == UserInfo.Id).FirstOrDefault();
                user.Password = txtNewPass.Text;
                db.SubmitChanges();
                MessageBox.Show("Change password successful!");
            }
            else
            {
                lblMessage.Text = error;
            }
        }


        private string ValidateInput()
        {
            string error = "";

            bool ValidOldPass= db.Local_AspNetUsers.Where(t => t.Password.Equals(txtOldPass.Text)).Any();
            if (!ValidOldPass)
            {
                return "Old password is invalid!";
            }

            bool NotMatch = !txtNewPass.Text.Equals(txtConfirmPass.Text);
            if (NotMatch)
            {
                return "New password and confirm password is not match!";
            }

            return error;
        }

        
    }
}
