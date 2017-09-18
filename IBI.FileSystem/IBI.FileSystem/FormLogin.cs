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
using System.Diagnostics;
using System.Reflection;

namespace IBI.FileSystem
{
    
    public partial class FormLogin : Form
    {
        private DataClasses_LocalDataContext db = null;
        const string ADMINISTRATOR = "administrator";
        const string MANAGER = "filemanager";
        const string UPLOADER = "fileuploader";
        const string DATAINPUT = "fileprocessor";
        public FormLogin()
        {
            InitializeComponent();
            lblMessage.Text = "";
            db = DB_Singleton.GetDatabase();


            var settings = db.Local_Settings.ToList();
            string remember= GetSettingValue(settings, "remember");
            checkBoxRemember.Checked = false;
            if (remember.ToLower() == "true")
            {
                txtUserName.Text = GetSettingValue(settings, "username");
                txtPassword.Text = GetSettingValue(settings, "password");
                checkBoxRemember.Checked = true;
            }

            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            int loginResult = Login();
            if (loginResult==0)
            {               
                this.Hide();
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.GetType() == typeof(FormMain))
                    {
                        frm.Activate();
                        ((FormMain)frm).HideShowMenu();
                        frm.Show();                        
                        return;
                    }
                }
                FormMain formMain = new FormMain();
                formMain.Show();    
            }
            else if (loginResult==1)
            {
                lblMessage.Text = "Username or password is invalid!";                
            }
            else if (loginResult == 2)
            {
                lblMessage.Text = "Unable connect to the database server!";                
            }
        }

        private int Login()
        {
            //0: success
            //1: invalid username, pass
            //2: connection error

            try
            {
                var user = db.Local_AspNetUsers.Where(t => t.UserName.Equals(txtUserName.Text) && t.Password.Equals(txtPassword.Text)).FirstOrDefault();
                int result = 1;

                if (user != null)
                {
                    //assign to user info
                    AssignUser(user);
                    //assign settings
                    AssignSettings();
                    result = 0;

                    //Save remember
                    //SaveSetting("username", txtUserName.Text);
                    //SaveSetting("password", txtPassword.Text);

                    //string remember = "false";
                    //if (checkBoxRemember.Checked) remember = "true";

                    //SaveSetting("remember", remember);









                }

                return result;
            }
            catch (Exception ex)
            {
                Helpers.LogHelper.WriteLog(ex.Message);
                return 2;
            }

            
        }


        private void SaveSetting(string name, string value)
        {
            var setting = db.Local_Settings.Where(t => t.Name == name).FirstOrDefault();
            if (setting==null)
            {
                setting = new Local_Setting();
                setting.Name = name;
                setting.Value = value;
                db.Local_Settings.InsertOnSubmit(setting);
            }
            else
            {
                setting.Value = value;
            }
            db.SubmitChanges();
        }

        private void AssignSettings()
        {

            var settings = db.Local_Settings.ToList();
            SettingInfo.RemoteServer = GetSettingValue(settings, "remoteserver");
            SettingInfo.RemoteServerPath = GetSettingValue(settings, "remoteserverpath");
            SettingInfo.RemoteUsername = GetSettingValue(settings, "remoteusername");
            SettingInfo.RemotePassword = GetSettingValue(settings, "remotepassword");
        }

        private string GetSettingValue(List<Local_Setting> settings, string name)
        {
            string result = "";
            if (settings != null)
            {
                var obj = settings.Where(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (obj != null)
                {
                    result = obj.Value;
                }
            }
            return result;
        }

        private void AssignUser(Local_AspNetUser user)
        {
            UserInfo.Id = user.Id;
            UserInfo.Name = user.Name;
            UserInfo.Password = user.Password;
            UserInfo.UserName = user.UserName;
            //Assign permission
            AssignPermission(user);
        }

        private void AssignPermission(Local_AspNetUser user)
        {
            //Assign role admin
            UserInfo.IsAdmin = false;
            bool isAdmin = false;
            var roles = db.Local_AspNetUserRoles.Where(t => t.UserId == user.Id).Select(s=>s.RoleId).ToArray();
            var roleList = db.Local_AspNetRoles.ToList();

            var roleAdmin = roleList.Where(t => t.NormalizedName.ToLower() == ADMINISTRATOR).FirstOrDefault();
            if (roleAdmin !=null)
            {
                if (roles.Contains(roleAdmin.Id))
                {
                    isAdmin = true;
                }
            }
            if (isAdmin)
            {
                UserInfo.IsAdmin = true;                
            }
            else
            {
                //check is manager
                UserInfo.IsManager = false;                
                var roleManager = roleList.Where(t => t.NormalizedName.ToLower()==MANAGER).FirstOrDefault();
                if (roleManager != null)
                {
                    if (roles.Contains(roleManager.Id))
                    {
                        UserInfo.IsManager = true;
                    }
                }

                //check is fileuploader
                UserInfo.IsUploader = false;
                var roleUploader = roleList.Where(t => t.NormalizedName.ToLower()== UPLOADER).FirstOrDefault();
                if (roleUploader != null)
                {
                    if (roles.Contains(roleUploader.Id))
                    {
                        UserInfo.IsUploader = true;
                    }
                }

                //check is datainput
                UserInfo.IsDataInput = false;
                var roleDataInput = roleList.Where(t => t.NormalizedName.ToLower() ==DATAINPUT).FirstOrDefault();
                if (roleDataInput != null)
                {
                    if (roles.Contains(roleDataInput.Id))
                    {
                        UserInfo.IsDataInput = true;
                    }
                }

               
            }
                       


        }
    }
}
