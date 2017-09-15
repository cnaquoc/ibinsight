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
            LoadListview();

            richTextBoxSuccess.Visible = false;
            richTextBoxError.Visible = false;
            btnUpload.Visible = true;
            
        }

        

        private void LoadListview()
        {
            listView1.Clear();
            
            

            //var arrFileName = _list.Select(t => t.FileName).ToArray();

            var newList = db.Local_Files.Select(t=>t.FileName).ToArray();

            var classifyList = db.Local_Classifies.ToList();

            _list = _list.Where(t => !newList.Contains(t.FileNameOnly)).OrderBy(t=>t.IsStandard).ToList();


            int iStandard = _list.Where(t => t.IsStandard == true).Count();

            lblStandard.Text = "Total files in standard: " + iStandard.ToString();
            lblNotStandard.Text = "Total files not in standard: " + (_list.Count()-iStandard).ToString();
            lblTotal.Text = "Total files: " + _list.Count().ToString();

            //bool isExistAutoUpload = false;

            //isExistAutoUpload = _list.Where(t => t.IsStandard == true).Any();

            //btnUpload.Visible = isExistAutoUpload;
            // Set the view to show details.
            listView1.View = View.Details;
            
            listView1.FullRowSelect = true;
            
            listView1.Sorting = SortOrder.Ascending;

            int index = 0;
            foreach (var item in _list)
            {   
                string classifyname = "";
                if (item.ListClassify !=null)
                {
                    classifyname = string.Join("; ", item.ListClassify.Select(t => t.Name).ToArray());
                }

                if (item.IsStandard)
                {
                    index = 0;
                }
                else
                {
                    index = 1;
                }
                

                string filename = Path.GetFileNameWithoutExtension(item.FileNameOnly);
                string extension = Path.GetExtension(item.FileName);
                ListViewItem lvi = new ListViewItem(filename, index);
                 
                lvi.SubItems.Add(extension);                
                lvi.SubItems.Add(classifyname);
                lvi.SubItems.Add(item.FileName);
                lvi.SubItems.Add(item.IsStandard.ToString());
                listView1.Items.Add(lvi);              

                
            }                       

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add("File name",  450 , HorizontalAlignment.Left);
            listView1.Columns.Add("Type", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Classify", -2, HorizontalAlignment.Left);

            //Assign the ImageList objects to the ListView.
            listView1.LargeImageList = imageList1;
            listView1.SmallImageList = imageList1;

            

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

                        LoadListview();
                        richTextBoxSuccess.Text = resultUploadSuccess;
                        richTextBoxError.Text = resultUploadErrors;


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
    }

    
    
}
