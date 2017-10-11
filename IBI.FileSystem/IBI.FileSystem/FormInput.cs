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
            //dataGridView.AutoGenerateColumns = false;
            dataGridView.RowHeadersVisible = false;
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
                CompanyName = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault() == null ? "" : listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().Name,
                Code = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault() == null ? "" : listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().Ticker,
                TaxCode = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault() ==null? "" : listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().TaxCode,
                ClassifyName = listC.Where(c => c.Id == t.ClassifyId).FirstOrDefault().Name,
                FileName = t.FileName,
                CreatedDate =t.CreatedDate,
                DateFrom = t.DateFrom.Value,
                DateTo = t.DateTo.Value,
                FileGUID = t.FileGUID.Value.ToString(),
                OldFileName=t.FileName
            }).ToList();
                        
            //Bind data to grid
            dataGridView.DataSource = listMain;           

            
        }


        

        public void LoadGridNotClassify()
        {
            //dataGridView.AutoGenerateColumns = false;
            dataGridView.RowHeadersVisible = false;

            //Get file from arrClassify
            var listFile = db.Local_Files.Where(t => (t.IsDone ?? false) == false && t.ClassifyId==0).ToList();

            //Get list for join data
            var listC = db.Local_Classifies.ToList();
            var listCompany = db.Local_Companies.ToList();

            listMain = listFile.Select(t => new ClassFileInput
            {
                Id = t.Id.ToString(),
                CompanyName = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault() == null ? "" : listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().Name,
                Code = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault() == null ? "" : listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().Ticker,
                TaxCode = listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault()==null? "": listCompany.Where(r => r.Id == t.CompanyId).FirstOrDefault().TaxCode,
                ClassifyName = "",
                FileName = t.FileName,
                CreatedDate = t.CreatedDate,
                DateFrom = t.DateFrom.Value,
                DateTo = t.DateTo.Value,
                FileGUID = t.FileGUID.Value.ToString(),
                OldFileName = t.FileName
            }).ToList();

            //Bind data to grid
            dataGridView.DataSource = listMain;


        }

        private void FormatGrid()
        {
            //Format grid
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;
            dataGridView.Columns[0].HeaderText = "Id";
            dataGridView.Columns[0].Width = 10;
            dataGridView.Columns[0].Visible = false;

            dataGridView.Columns[1].HeaderText = "Company name";
            dataGridView.Columns[1].Width = 250;
            dataGridView.Columns[1].ReadOnly = true;

            dataGridView.Columns[2].HeaderText = "Ticker";
            dataGridView.Columns[2].Width = 50;
            dataGridView.Columns[2].ReadOnly = true;

            dataGridView.Columns[3].HeaderText = "Tax code";
            dataGridView.Columns[3].Width = 100;
            dataGridView.Columns[3].ReadOnly = true;

            dataGridView.Columns[4].HeaderText = "Classify name";
            dataGridView.Columns[4].Width = 250;
            dataGridView.Columns[4].ReadOnly = true;

            dataGridView.Columns[5].HeaderText = "File name";            
            dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView.Columns[6].HeaderText = "Created date";
            dataGridView.Columns[6].Width = 110;
            dataGridView.Columns[6].Visible = false;
            

            dataGridView.Columns[7].HeaderText = "Date from";
            dataGridView.Columns[7].Width = 80;
            dataGridView.Columns[7].ReadOnly = true;

            dataGridView.Columns[8].HeaderText = "Date to";
            dataGridView.Columns[8].Width = 80;
            dataGridView.Columns[8].ReadOnly = true;

            dataGridView.Columns[9].HeaderText = "file GUID";
            dataGridView.Columns[9].Name = "Id";
            dataGridView.Columns[9].Width = 110;
            dataGridView.Columns[9].Visible = false;

            dataGridView.Columns[10].HeaderText = "Old File Name";
            dataGridView.Columns[10].Name = "OldFileName";
            dataGridView.Columns[10].Width = 110;
            dataGridView.Columns[10].Visible = false;
            


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

            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
                    string FileGUID = row.Cells["Id"].Value.ToString();
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

            var list = listMain.Where(t =>  (string.IsNullOrEmpty(t.Code) == true ? "" : t.Code.ToLower())
                                            .Contains(txtSearchTicker.Text.Trim().ToLower())).ToList();
            

            dataGridView.DataSource = list;
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            var list = listMain.Where(t => (string.IsNullOrEmpty(t.Code)==true? "": t.Code.ToLower())
                                           .Contains(txtSearchTicker.Text.Trim().ToLower()) &&
                                           t.DateFrom >= dtpFrom.Value.Date && 
                                           t.DateTo <= dtpTo.Value.Date 
                                           ).ToList();
            dataGridView.DataSource = list;
        }

        private void rdbExistClassify_CheckedChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void rdbExcludeClassify_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridNotClassify();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >-1)
            {
                string Id = dataGridView.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                string newFileName = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //string oldFileNameOnly = Path.GetFileNameWithoutExtension(oldFileName);
                var objSelect = listMain.Where(t => t.Id == Id).FirstOrDefault();

                string oldFileName = dataGridView.Rows[e.RowIndex].Cells["OldFileName"].Value.ToString(); 
                //string extension = Path.GetExtension(oldFileName);

                //string path = oldFileName.Replace(oldFileNameOnly + extension, "");

                if (objSelect != null)
                {
                    try
                    {
                        //update isstandard
                        bool isOK = false;
                        int classifyId = 0;

                        
                        ClassFile newClassFile = new ClassFile();
                        string reason = "";
                        bool IsparseAgain = ParseDetailGetClassify(objSelect.FileName, newClassFile, ref reason);

                        if (IsparseAgain && newClassFile.ListClassify.Count==1)
                        {
                            classifyId = newClassFile.ListClassify.FirstOrDefault().Id;
                            isOK = true;
                        }
                        else if (IsparseAgain && newClassFile.ListClassify.Count > 1)
                        {

                            //Show popup to choose classify
                            DialogClassify dialogClassify = new DialogClassify(newClassFile.ListClassify);
                            dialogClassify.ShowDialog();
                            classifyId = dialogClassify.getId;
                            if (classifyId>0)
                            {
                                isOK = true;
                            }

                                    
                        }
                                                                
                                

                        if (isOK)
                        {
                            //save to database

                            Local_File file = db.Local_Files.Where(t => t.Id.ToString() == Id).FirstOrDefault();
                            file.FileName = newFileName;
                            file.ClassifyId = classifyId;                           
                            db.SubmitChanges();

                            dataGridView.Update();
                            dataGridView.Refresh();
                            MessageBox.Show("File " + oldFileName + " renamed to file " + newFileName);
                        }
                        else
                        {
                            
                            MessageBox.Show("Can not rename this file " + oldFileName);
                            //dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = oldFileName;
                            
                        }

                        
                        //LoadGrid();

                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can not rename this file " + oldFileName);
                        LogHelper.WriteLog(ex.Message);
                        //LoadGrid();                    
                    }
                }
            }
            
        }

        public bool ParseDetailGetClassify(string filename, ClassFile classFile, ref string reason)
        {
            bool isNotStandard = true;

            //leng<8 + 1 + 1 + 1 + 1 + 4 =16
            bool isDateTime = true;
            if (filename.Trim().Length < 8)
            {
                reason = "Invalid date"; ;  //not show 4 .pdf
                return false;
            }

            //get string date
            //get string code

            int posCode = Utils.GetPostionFirstCharacter(filename);
            string filenameDate = filename.Substring(0, posCode);
            string filenameNotDate = filename.Substring(posCode);

            string filenameDateTemp = filenameDate.Replace("-", "").Replace("_", "").Replace(" ", "").Trim();

            if (!(filenameDateTemp.Length == 8))
            {
                reason = "Invalid date";
                return false;
            }

            string to = "";
            string from = "";

            to = filenameDateTemp;
            from = filenameDateTemp;

            //Parse date to
            DateTime dateTo = Utils.StringToDateTime(to, ref isDateTime);
            if (!isDateTime)
            {
                reason = "Invalid date";
                return false;
            }

            //Parse date from
            DateTime dateFrom = Utils.StringToDateTime(from, ref isDateTime);
            if (!isDateTime)
            {
                reason = "Invalid date";
                return false;
            }

            var compare = DateTime.Compare(dateFrom, dateTo);
            if (compare > 0)
            {
                var dateTemp = dateFrom;
                dateFrom = dateTo;
                dateTo = dateTemp;
            }



            //Get TaxCode

            string filenameNotDateExcluseExtension = Path.GetFileNameWithoutExtension(filenameNotDate);

            string filenameNotDateReverse = Utils.Reverse(filenameNotDateExcluseExtension);
            int posLetter = Utils.GetPostionFirstNotIsNumber(filenameNotDateReverse);
            string taxcode = filenameNotDateReverse.Substring(0, posLetter);

            if (!(taxcode.Length == 10 || taxcode.Length == 13))
            {
                reason = "Invalid taxcode";
                return false;
            }

            taxcode = Utils.Reverse(taxcode);

            //Keep this code is running correct

            filename = filenameDateTemp + "-" + filenameNotDate;


            //Parse keyword
            string extension = Path.GetExtension(filename);
            string keyword = filenameNotDate.Replace(taxcode, "").Trim();






            var classifyList = db.Local_Classifies.ToList();
            var classifyListFound = Utils.FindClassifyFromKeyword(keyword, classifyList);

            bool notExistKeyword = true;

            if (classifyListFound.Count == 0)
            {
                isNotStandard = false;
                reason = "Not exist keyword";
                notExistKeyword = false;
            }

            // Assign data to class file            
            classFile.Code = taxcode;
            classFile.IsStandard = isNotStandard;
            classFile.Keyword = keyword;
            classFile.DateFrom = dateFrom;
            classFile.DateTo = dateTo;
            //classFile.CompanyId = company.Id.ToString();
            classFile.ListClassify = classifyListFound;
            classFile.NotExistKeyword = notExistKeyword;

            return isNotStandard;
        }
    }
}
