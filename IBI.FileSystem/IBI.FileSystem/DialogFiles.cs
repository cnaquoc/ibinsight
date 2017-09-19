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
    public partial class DialogFiles : Form
    {
        private List<ClassFile> _list;
        private ClassFile fileInfo;
        private DataClasses_LocalDataContext db = null;
        private string _FileName;
        
        public ClassFile getFileInfo { get { return fileInfo; } }
        public string getFileName { get { return _FileName; } }
       
        public DialogFiles(List<ClassFile> list)
        {
            InitializeComponent();
            db = DB_Singleton.GetDatabase();
            lblStandard.Text = "";
            lblNotStandard.Text = "";
            lblTotal.Text = "";
            _list = list;          
            
            LoadGrid();
            FormatGrid();            

            richTextBoxSuccess.Visible = false;
            richTextBoxError.Visible = false;
            btnUpload.Visible = true;
            
        }

        public void LoadGrid()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.RowHeadersVisible = false;
            
            var newList = db.Local_Files.Select(t => t.FileName).ToArray();
            var classifyList = db.Local_Classifies.ToList();
            _list = _list.Where(t => !newList.Contains(Path.GetFileName(t.FileName))).OrderBy(t => t.IsStandard).ToList();


            int iStandard = _list.Where(t => t.IsStandard == true).Count();

            lblStandard.Text = "Total files in standard: " + iStandard.ToString();
            lblNotStandard.Text = "Total files not in standard: " + (_list.Count() - iStandard).ToString();
            lblTotal.Text = "Total files: " + _list.Count().ToString();

            foreach (var item in _list)
            {
                item.ClassifyNames = "";
                item.CountClassify = "";
                if (item.ListClassify != null)
                {
                    item.CountClassify =  item.ListClassify.Count.ToString();                   

                    item.ClassifyNames = item.ListClassify==null? "": string.Join("; ", item.ListClassify.Select(t => t.Name).ToArray());
                }

                if (item.CountClassify == "0") item.CountClassify = "";

                item.FileNameOnly = Path.GetFileNameWithoutExtension(item.FileName);

                item.Extension = Path.GetExtension(item.FileName);
                
            }

               
            //Bind data to grid
            dataGridView.DataSource = _list;


        }

        private void FormatGrid()
        {
            //Format grid

            
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            //dataGridView.BackgroundColor= Color.

            //Add btnImage to grid
            DataGridViewImageColumn btnColImage = new DataGridViewImageColumn();
            btnColImage.HeaderText = "";           
            btnColImage.Name = "Image";
            btnColImage.Width = 35;
            btnColImage.Image = imageList1.Images[0];
            dataGridView.Columns.Add(btnColImage);

            

            DataGridViewTextBoxColumn txtColFileName = new DataGridViewTextBoxColumn();
            txtColFileName.HeaderText = "File name";            
            txtColFileName.Name = "txtColFileName";
            txtColFileName.DefaultCellStyle.WrapMode= DataGridViewTriState.True;
            txtColFileName.Width = 450;
            txtColFileName.DataPropertyName = "FileNameOnly";
            dataGridView.Columns.Add(txtColFileName);
                       


            DataGridViewTextBoxColumn txtColType = new DataGridViewTextBoxColumn();
            txtColType.HeaderText = "Type";
            txtColType.Name = "txtColType";
            txtColType.ReadOnly = true;
            txtColType.Width = 75;
            txtColType.DataPropertyName = "Extension";
            dataGridView.Columns.Add(txtColType);

            DataGridViewTextBoxColumn txtColReason = new DataGridViewTextBoxColumn();
            txtColReason.HeaderText = "Reason";
            txtColReason.Name = "txtColReason";
            txtColReason.ReadOnly = true;
            txtColReason.Width = 150;
            txtColReason.DataPropertyName = "Reason";
            dataGridView.Columns.Add(txtColReason);

            DataGridViewTextBoxColumn txtColCount = new DataGridViewTextBoxColumn();
            txtColCount.HeaderText = "";
            txtColCount.Name = "txtColCount";
            txtColCount.ReadOnly = true;
            txtColCount.Width = 20;
            txtColCount.DataPropertyName = "CountClassify";
            dataGridView.Columns.Add(txtColCount);

            DataGridViewTextBoxColumn txtColClassify = new DataGridViewTextBoxColumn();
            txtColClassify.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            txtColFileName.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            txtColClassify.HeaderText = "Classify";
            txtColClassify.Name = "txtColClassify";
            txtColClassify.ReadOnly = true;
            txtColClassify.Width = 250;
            txtColClassify.DataPropertyName = "ClassifyNames";
            dataGridView.Columns.Add(txtColClassify);


            DataGridViewTextBoxColumn txtColStandard = new DataGridViewTextBoxColumn();
            txtColStandard.HeaderText = "Standard";
            txtColStandard.Name = "txtColStandard";
            txtColStandard.Width = 50;
            txtColStandard.DataPropertyName = "IsStandard";
            txtColStandard.Visible = false;
            dataGridView.Columns.Add(txtColStandard);

            DataGridViewTextBoxColumn txtColId = new DataGridViewTextBoxColumn();
            txtColId.HeaderText = "Id";
            txtColId.Name = "txtColId";
            txtColId.Width = 50;
            txtColId.DataPropertyName = "FileName";
            txtColId.Visible = false;
            dataGridView.Columns.Add(txtColId);


            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


        }


        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView1.SelectedItems.Count == 0)
            //    return;

            //ListViewItem item = listView1.SelectedItems[0];
            
           
            //_FileName = item.SubItems[3].Text;

            //string Standard = item.SubItems[4].Text;
            //if (Standard.ToLower()=="true")
            //{
            //    MessageBox.Show("This file should be auto upload!");
            //    return;
            //}

            //fileInfo = _list.Where(t => t.FileName == _FileName).FirstOrDefault();


            
            //this.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile();
            richTextBoxSuccess.Visible = true;
            richTextBoxError.Visible = true;
        }

        private void UploadFile()
        {
            try
            {
                string RemoteServer = SettingInfo.RemoteServer;
                string RemoteServerPath = SettingInfo.RemoteServerPath;                               

                string remoteUNC = RemoteServer;
                string remoteFullPathUNC = Utils.ConcatPath(RemoteServer, RemoteServerPath);

                string connectionResult = Helpers.RemoteServer.OpenConnection();
                string errorUpload = "";

                if (string.IsNullOrEmpty(connectionResult)) //Connect successful 
                {
                    try
                    {
                        if (!Directory.Exists(@remoteFullPathUNC))
                        {
                            MessageBox.Show("Remote server path does not exist!");
                            return;
                        }


                        var listAutoUpload = _list.Where(t => t.IsStandard == true).OrderBy(t=>t.IsStandard).ToList();

                        string resultUploadSuccess = "";
                        string resultUploadErrors = "";

                        foreach (var autofile in listAutoUpload)
                        {
                            string FileName = autofile.FileName;
                            string FileExtension = Path.GetExtension(FileName);
                            
                            string fileGuid = Guid.NewGuid().ToString() ;
                            string newFileName = fileGuid + FileExtension;
                            File.Copy(FileName, Utils.ConcatPath(@remoteFullPathUNC, newFileName), true);

                            bool SaveOK = false;
                            SaveOK = SaveFileToDatabase(autofile, fileGuid);
                            //Copy data
                            if (SaveOK)
                            {
                                

                                if (resultUploadSuccess=="")
                                    resultUploadSuccess = autofile.FileNameOnly;
                                else
                                    resultUploadSuccess = resultUploadSuccess + "; " + autofile.FileNameOnly;
                                
                            }
                            else
                            {
                                if (resultUploadErrors == "")
                                    resultUploadErrors = autofile.FileNameOnly;
                                else
                                    resultUploadErrors = resultUploadErrors + "; " + autofile.FileNameOnly;
                                
                            }
                        }

                        Helpers.RemoteServer.CloseConnection();

                        if (resultUploadSuccess != "")
                        {
                            resultUploadSuccess = resultUploadSuccess + " uploaded successfull!";
                            
                        }

                        if (resultUploadErrors != "")
                        {
                            resultUploadSuccess = resultUploadSuccess + " uploaded failed";
                        }

                        LoadGrid();
                        richTextBoxSuccess.Text = resultUploadSuccess;
                        richTextBoxError.Text = resultUploadErrors;
                        MessageBox.Show("Upload done!");

                    }
                    catch (Exception ex)
                    {
                        errorUpload = "File copy. " + ex.Message;
                        LogHelper.WriteLog(errorUpload);
                    }
                }
                else // error connection
                {
                    errorUpload = "Can not remote to server" + remoteUNC + "! " + connectionResult;
                    LogHelper.WriteLog(errorUpload);
                }

                if (errorUpload != "")
                {
                    MessageBox.Show(errorUpload);
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message, "Upload file");
                MessageBox.Show("Upload file is failed!");
            }
        }


        private bool SaveFileToDatabase(ClassFile classFile, string FileGuid)
        {
            try
            {
                //Save to databse

                List<Local_File> listInsert = new List<Local_File>();

                Guid FileGUID = new Guid(FileGuid);
                Guid CompanyId = new Guid(classFile.CompanyId);
                DateTime currentDate = DateTime.Now;

                foreach (var classify in classFile.ListClassify)
                {
                    Local_File local_File = new Local_File();
                    local_File.FileName = Path.GetFileName(classFile.FileName);
                    local_File.Id = Guid.NewGuid();
                    local_File.CompanyId = CompanyId;
                    local_File.ClassifyId = classify.Id;
                    local_File.CreatedDate = currentDate;
                    local_File.UserId = Helpers.UserInfo.Id;
                    local_File.FileGUID = FileGUID;
                    local_File.DateFrom = classFile.DateFrom;
                    local_File.DateTo = classFile.DateTo;
                    listInsert.Add(local_File);
                }



                db.Local_Files.InsertAllOnSubmit(listInsert);
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Helpers.LogHelper.WriteLog(ex.Message);
                return false;
            }



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "Image")
            {
                DataGridViewRow currentRow = dataGridView.Rows[e.RowIndex];

                string standard= currentRow.Cells["txtColStandard"].Value.ToString();

                if (standard.ToLower()=="false")
                    e.Value = imageList1.Images[1];
                else
                    e.Value = imageList1.Images[0];
            }
        }

        

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
                        
            string oldFileName = dataGridView.Rows[e.RowIndex].Cells["txtColId"].Value.ToString();
            string newFileNameOnly= dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string oldFileNameOnly = Path.GetFileNameWithoutExtension(oldFileName);
            var objSelect= _list.Where(t => t.FileNameOnly == newFileNameOnly).FirstOrDefault();


            string extension = Path.GetExtension(oldFileName);
            
            string path = oldFileName.Replace(oldFileNameOnly + extension, "");

            if (objSelect!=null)
            {
                try
                {
                    string newFileName = path + newFileNameOnly + extension;
                    System.IO.File.Move(oldFileName, newFileName);
                    objSelect.FileName = newFileName;
                    objSelect.FileNameOnly = newFileNameOnly;

                    //update isstandard
                    foreach (Form frm in Application.OpenForms)
                    {
                        if (frm.GetType() == typeof(FormUpload))
                        {
                            ClassFile newClassFile = new ClassFile();
                            string reason = "";
                            bool IsparseAgain= ((FormUpload)frm).ParseDetail(objSelect.FileNameOnly + extension, newClassFile, ref reason);


                            objSelect.IsStandard = newClassFile.IsStandard;
                            objSelect.Reason = reason;
                            objSelect.NotExistKeyword = newClassFile.NotExistKeyword;
                            objSelect.ListClassify = newClassFile.ListClassify;
                            break;
                        }
                    }                                     

                    LoadGrid();

                    MessageBox.Show("File " + oldFileNameOnly + " renamed to file " + newFileNameOnly);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not rename this file " + oldFileNameOnly);
                    LogHelper.WriteLog(ex.Message);
                    LoadGrid();                    
                }
                

                
            }
            
            

        }
    }

    
    
}
