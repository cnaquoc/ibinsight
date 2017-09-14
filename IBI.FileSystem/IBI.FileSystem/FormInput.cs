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
using System.IO;

namespace IBI.FileSystem
{
    public partial class FormInput : Form
    {
        private DataClasses_LocalDataContext db = null;
        public FormInput()
        {
            InitializeComponent();
            db = DB_Singleton.GetDatabase();
            LoadGrid();
            FormatGrid();
        }

        public void LoadGrid()
        {
            //Get roles from currents user login
            var arrRoles = db.Local_AspNetUserRoles.Where(t => t.UserId == UserInfo.Id).Select(s=>s.RoleId).ToArray();
            if (UserInfo.IsAdmin)
            {
                arrRoles = db.Local_AspNetUserRoles.Select(s => s.RoleId).ToArray();
            }
            
            //Get all classify from arrRoles
            var arrClassify = db.Local_Role_Classifies.Where(t => arrRoles.Contains(t.RoleId)).Select(s=>s.ClassifyId).ToArray();
            
            //Get file from arrClassify
            var listFile = db.Local_Files.Where(t=>(t.IsDone??false) == false && arrClassify.Contains(t.ClassifyId)).ToList();

            //Get list for join data
            var listC = db.Local_Classifies.ToList();
            var listCompany = db.Local_Companies.ToList();

            var listMain = listFile.Select(t => new
            {
                Id = t.Id,
                CompanyName = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().Name,
                ClassifyName = listC.Where(c => c.Id == t.ClassifyId).FirstOrDefault().Name,
                FileName = t.FileName,
                CreatedDate =t.CreatedDate,
                FileGUID = t.FileGUID
            }).ToList();
                        
            //Bind data to grid
            dataGridView.DataSource = listMain;           

            
        }

        private void FormatGrid()
        {
            //Format grid
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;
            dataGridView.Columns[0].HeaderText = "Id";
            dataGridView.Columns[0].Width = 10;
            dataGridView.Columns[0].Visible = false;

            dataGridView.Columns[1].HeaderText = "Company name";
            dataGridView.Columns[1].Width = 250;

            dataGridView.Columns[2].HeaderText = "Classify name";
            dataGridView.Columns[2].Width = 250;

            dataGridView.Columns[3].HeaderText = "File name";
            dataGridView.Columns[3].Width = 250;

            dataGridView.Columns[4].HeaderText = "Created date";
            dataGridView.Columns[4].Width = 110;

            dataGridView.Columns[5].HeaderText = "Created date";
            dataGridView.Columns[5].Width = 110;
            dataGridView.Columns[5].Visible = false;

            //Add Done button to grid
            DataGridViewButtonColumn btnColDone = new DataGridViewButtonColumn();
            btnColDone.HeaderText = "Action";
            btnColDone.Text = "Done";
            btnColDone.Name = "btnDone";            
            btnColDone.Width = 75;
            btnColDone.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(btnColDone);

            DataGridViewButtonColumn btnColOpenFile = new DataGridViewButtonColumn();
            btnColOpenFile.HeaderText = "";
            btnColOpenFile.Text = "Open file...";
            btnColOpenFile.Name = "btnOpenFile";
            btnColOpenFile.Width = 100;
            btnColOpenFile.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(btnColOpenFile);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1) //click button Done [-1 row header]
            {
                DoneAction();
            }
            else if ((e.ColumnIndex == 5 || e.ColumnIndex==1) && e.RowIndex > -1) //click open file 1 (col Open file) 5 (col File name)
            {
                DataGridViewRow row = this.dataGridView.CurrentRow;
                string Id = row.Cells["Id"].Value.ToString();
                if (Id != "")
                {
                    string FileName = row.Cells["FileName"].Value.ToString();
                    string FileGUID = row.Cells["FileGUID"].Value.ToString();
                    string extension = Path.GetExtension(FileName);
                    FileName = FileGUID + extension;
                    OpenFile(FileName);
                }
            }
        }

        private void DoneAction()
        {            
            var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataGridViewRow row = this.dataGridView.CurrentRow;
                string Id = row.Cells["Id"].Value.ToString();
                if (Id != "")
                {
                    var file = db.Local_Files.Where(t => t.Id.ToString() == Id).FirstOrDefault();
                    file.IsDone = true;
                    file.DoneDate = DateTime.Now;
                    db.SubmitChanges();
                    LoadGrid();
                    MessageBox.Show("Successful!");
                }
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void OpenFile(string filename)
        {
            try
            {
                //connect first
                string connectionResult = Helpers.RemoteServer.OpenConnection();
                if (string.IsNullOrEmpty(connectionResult))
                {
                    string RemoteServer = Helpers.RemoteServer.GetConfigValue("RemoteServer");
                    string RemoteServerPath = Helpers.RemoteServer.GetConfigValue("RemoteServerPath");  
                    
                    string remoteFullPath = Utils.ConcatPath(RemoteServer, RemoteServerPath);
                    filename = Utils.ConcatPath(remoteFullPath, filename);

                    if (File.Exists(filename))
                    {
                        System.Diagnostics.Process.Start(filename);
                    }
                    else
                    {
                        MessageBox.Show(filename + " does not exist");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Unable to remote to server!");
                }
            }
            catch (Exception ex)
            {
                Helpers.LogHelper.WriteLog(ex.Message);
                MessageBox.Show("Can not open file!");
            }
            
        }
    }
}
