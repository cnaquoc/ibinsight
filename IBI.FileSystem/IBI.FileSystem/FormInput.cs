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
        private List<ClassFileInput> listMain = new List<ClassFileInput>();

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

            listMain = listFile.Select(t => new ClassFileInput
            {
                Id = t.Id.ToString(),
                CompanyName = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().Name,
                Code = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().Ticker,
                ClassifyName = listC.Where(c => c.Id == t.ClassifyId).FirstOrDefault().Name,
                FileName = t.FileName,
                CreatedDate =t.CreatedDate,
                DateFrom = t.DateFrom.Value,
                DateTo = t.DateTo.Value,
                FileGUID = t.FileGUID.Value.ToString()
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

            dataGridView.Columns[2].HeaderText = "Ticker";
            dataGridView.Columns[2].Width = 100;

            dataGridView.Columns[3].HeaderText = "Classify name";
            dataGridView.Columns[3].Width = 250;

            dataGridView.Columns[4].HeaderText = "File name";            
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView.Columns[5].HeaderText = "Created date";
            dataGridView.Columns[5].Width = 110;
            dataGridView.Columns[5].Visible = false;

            dataGridView.Columns[6].HeaderText = "Date from";
            dataGridView.Columns[6].Width = 80;

            dataGridView.Columns[7].HeaderText = "Date to";
            dataGridView.Columns[7].Width = 80;

            dataGridView.Columns[8].HeaderText = "file GUID";
            dataGridView.Columns[8].Width = 110;
            dataGridView.Columns[8].Visible = false;

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
            else if ((e.ColumnIndex==1) && e.RowIndex > -1) //click open file 1 (col Open file) 5 (col File name)
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
                    string RemoteServer = SettingInfo.RemoteServer;
                    string RemoteServerPath = SettingInfo.RemoteServerPath;
                    
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //LoadGrid();
            var list = listMain.Where(t => t.Code.ToLower().Contains(txtSearchTicker.Text.Trim().ToLower())).ToList();
            dataGridView.DataSource = list;
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            var list = listMain.Where(t => t.Code.ToLower().Contains(txtSearchTicker.Text.Trim().ToLower()) && 
                                           t.DateFrom >= dtpFrom.Value.Date && 
                                           t.DateTo <= dtpTo.Value.Date 
                                           ).ToList();
            dataGridView.DataSource = list;
        }
    }
}
