using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using IBI.FileSystem.Helpers;
using System.Globalization;

namespace IBI.FileSystem
{
    public partial class FormUpload : Form
    {
        private DataClasses_LocalDataContext db = null;
        TreeNode parentNode = null;

        

        public FormUpload()
        {
            InitializeComponent();
            db = DB_Singleton.GetDatabase();            
            
            LoadTreeView();
            lblMessage.Text = "";
            txtClassifyId.Text = "";
            txtCompanyId.Text = "";
            lblSelectedMessage.Text = "";
            
            
        }

       

        private void LoadTreeView()
        {
            var classifies = db.Local_Classifies.Where(t=>(t.ParentId??0)==0).ToList();

            foreach (var classify in classifies)
            {
                string code = "[" + classify.Code + "] ";
                if (string.IsNullOrEmpty(classify.Code))
                {
                    code = "";                    
                }
                

                string nodeName = code + classify.Name;
                parentNode = treeViewClassify.Nodes.Add(nodeName);
                parentNode.Name = classify.Id.ToString();
                //parentNode.Tag = classify.Code;
                PopulateTreeView(classify.Id, parentNode);
            }

            treeViewClassify.ExpandAll();
            treeViewClassify.CheckBoxes = true;
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            var children = db.Local_Classifies.Where(t=>t.ParentId ==parentId).ToList();

            TreeNode childNode;
            foreach (var child in children)
            {
                string code = "[" + child.Code + "] ";
                if (string.IsNullOrEmpty(child.Code)) code = "";

                string nodeName = code + child.Name;

                if (parentNode == null)
                    childNode = treeViewClassify.Nodes.Add(nodeName);
                else
                    childNode = parentNode.Nodes.Add(nodeName);

                childNode.Name = child.Id.ToString();
                //childNode.Tag = child.Code;

                PopulateTreeView(child.Id, childNode);
            }
            
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            //Validate upload
            string error = ValidateUpload();
            if (error=="")
            {
                try
                {                    
                    string FileName =  Path.GetFileName(txtFile.Text);
                    string FileExtension = Path.GetExtension(txtFile.Text);

                    string remoteUNC = SettingInfo.RemoteServer; 
                    string remoteFullPathUNC =Utils.ConcatPath(SettingInfo.RemoteServer, SettingInfo.RemoteServerPath); 

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
                            
                            string fileGuid = Guid.NewGuid().ToString();
                            FileName = fileGuid + FileExtension;
                            //Copy data
                            File.Copy(txtFile.Text, Utils.ConcatPath(@remoteFullPathUNC, FileName), true);

                            bool SaveOK = false;
                            SaveOK = SaveFileToDatabase(FileName, fileGuid);
                            
                            if (SaveOK)
                            {
                                Helpers.RemoteServer.CloseConnection();
                                MessageBox.Show("Upload file is successful!");
                            }
                            else
                            {
                                MessageBox.Show("Error! Can not save to database!");
                            }
                        }
                        catch (Exception ex)
                        {
                            errorUpload = "File copy. " + ex.Message;
                            LogHelper.WriteLog(errorUpload);
                        }
                    }
                    else // error connection
                    {
                        errorUpload = "Can not remote to server " + remoteUNC + "! " + connectionResult;
                        LogHelper.WriteLog(errorUpload);
                    }

                    if (errorUpload != "")
                    {
                        MessageBox.Show(errorUpload);                        
                    }
                    
                }
                catch ( Exception ex )
                {
                    LogHelper.WriteLog(ex.Message, "Upload file");
                    MessageBox.Show("Upload file is failed!");
                } 
            }
            else
            {
                lblMessage.Text = error;                
            }
        }



        private bool SaveFileToDatabase2(ClassFile classFile, string FileGuid)
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
                    local_File.FileName = classFile.FileName;
                    local_File.Id = Guid.NewGuid();
                    local_File.CompanyId = CompanyId;
                    local_File.ClassifyId = classFile.ClassifyId;
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



        private bool SaveFileToDatabase(string FileName, string FileGuid)
        {
            try
            {
                //Save to databse
                List<Local_File> listInsert = new List<Local_File>();

                var arrayId = txtClassifyId.Text.Split(',');

                foreach (var Id in arrayId)
                {
                    Local_File local_File = new Local_File();
                    local_File.FileName = Path.GetFileName(FileName);
                    local_File.Id = Guid.NewGuid();
                    local_File.CompanyId = new Guid(txtCompanyId.Text);
                    local_File.CreatedDate = dtpDate.Value;
                    local_File.UserId = Helpers.UserInfo.Id;

                    local_File.ClassifyId = int.Parse(Id);

                    local_File.FileGUID = new Guid(FileGuid);
                    local_File.DateFrom = dtpFrom.Value;
                    local_File.DateTo = dtpTo.Value;

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

        private string ValidateUpload()
        {
            if (string.IsNullOrEmpty(txtFile.Text))
            {
                return "Please select a file to upload!";
            }

            if (string.IsNullOrEmpty(txtCompanyId.Text))
            {
                return "Please select a company!";
            }

            if (string.IsNullOrEmpty(txtSelectedClassify.Text))
            {
                return "Please select a classify with code!";
            }

            if ( DateTime.Compare(dtpFrom.Value.Date,dtpTo.Value.Date)>0)
            {
                return "From date can not greater than To date!";
            }

            return "";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {            
            DialogResult result = openFileDialog1.ShowDialog();           

            if (result == DialogResult.OK)
            {                
                string file = openFileDialog1.FileName;
                try
                {
                    txtFile.Text = file;
                }
                catch (IOException)
                {
                }
            }
        }

        private void btnSelectCompany_Click(object sender, EventArgs e)
        {
            DialogCompany dialog = new DialogCompany();
            dialog.ShowIcon = false;
            dialog.ShowInTaskbar = false;
            dialog.ShowDialog();
            txtCompanyId.Text = dialog.getId;
            txtCompanyName.Text = dialog.getName;
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                                        
                    if(files.Length>0)
                    {
                        //Show Dialog Files
                        List<ClassFile> list = new List<ClassFile>();
                        foreach( var filename in files)
                        {
                            //show only files got extension
                            string extension = Path.GetExtension(filename);

                            //string filenameOnly = Path.GetFileName(filename);                                                       


                            if (!string.IsNullOrEmpty(extension))
                            {                                
                                //ParseStandard
                                ClassFile cf = ParseStandard(filename);                                
                                list.Add(cf);
                            }                           
                            
                        }     
                        
                        if (list.Count>0)
                        { 
                            DialogFiles df = new DialogFiles(list);
                            df.ShowDialog();

                            if (!string.IsNullOrEmpty(df.getFileName))
                            {
                                
                                var returnObj = df.getFileInfo;

                                txtFile.Text = returnObj.FileName; //df.getFileName;

                                //Get companyinfo from ticker
                                var company = db.Local_Companies.Where(t => t.Ticker == returnObj.Code).FirstOrDefault();
                                
                                txtCompanyName.Text =  company==null? "": company.Name;
                                txtCompanyId.Text = company == null ? "" : company.Id.ToString();

                                try
                                {
                                    dtpFrom.Value = returnObj.DateFrom;
                                }
                                catch
                                {
                                    dtpFrom.Value = DateTime.Now;
                                }

                                try
                                {
                                    dtpTo.Value = returnObj.DateTo;
                                }
                                catch
                                {
                                    dtpTo.Value = DateTime.Now;
                                }
                                

                                //Get classifyinfo from keyword
                                string keyword = returnObj.Keyword;

                                var classifyList = db.Local_Classifies.ToList();
                                var classifyListFound = Utils.FindClassifyFromKeyword(keyword, classifyList);

                                //txtSelectedClassify.Text = classify == null ? "" : "[" + classify.Code + "] " + classify.Name;
                                //lblSelectedMessage.Text = txtSelectedClassify.Text;
                                //txtClassifyId.Text = classify == null ? "" : classify.Id.ToString();


                            }
                                
                        }
                        else
                        {
                            MessageBox.Show("No files exist in this folder!");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("No files exist in this folder!");
                    }
                }
            }
        }


        //private Local_Classify FindClassifyFromKeyword(string keyword)
        //{
        //    if (string.IsNullOrEmpty(keyword)) return null;

        //    var classifyList = db.Local_Classifies.ToList();
        //    foreach (var classify in classifyList)
        //    {
        //        if (!string.IsNullOrEmpty(classify.Keyword))
        //        {
        //            //var keywordArray = classify.Keyword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //            var keywordArray = classify.Keyword.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        //            foreach (var subkey in keywordArray)
        //            {                        
        //                if (keyword.Contains(subkey.Replace(@"\r", "").Trim()))
        //                {
        //                    return classify;
        //                }
        //            }
        //        }

        //    }
        //    return null;
        //}

        private ClassFile ParseStandard(string filename)
        {
            string filenameOnly = Path.GetFileName(filename);

            var result = new ClassFile();
            result.FileName = filename;
            result.FileNameOnly = filenameOnly;

            bool isStandard = false;
            string reason = "";
            isStandard = ParseDetail(filenameOnly, result, ref reason);            
            if (!isStandard)
            {
                result.IsStandard = isStandard;
                result.Reason = reason;
            }

            return result;
        }


        private bool ParseDetail(string filename, ClassFile classFile, ref string reason)
        {
            bool isStandard = true;

            //leng<8 + 1 + 1 + 1 + 1 + 4 =16
            bool isDateTime = true;
            if (filename.Trim().Length < 8)
            {
                reason = "Invalid date"; ;  //not show 4 .pdf
                return false;
            }

            //get string date
            //get string code

            int posCode = GetPostionFirstCharacter(filename);
            string filenameDate = filename.Substring(0, posCode);
            string filenameNotDate = filename.Substring(posCode + 1);

            string filenameDateTemp = filenameDate.Replace("-", "").Replace("_", "").Replace(" ","").Trim(); 
            
            if (!(filenameDateTemp.Length==8 || filenameDateTemp.Length == 16))
            {
                reason = "Invalid date";
                return false;
            }

            string to = ""; 
            string from = ""; 
            if (filenameDateTemp.Length == 8)
            {
                to = filenameDateTemp;
                from = filenameDateTemp;
            }
            else if (filenameDateTemp.Length == 16)
            {
                to = filenameDateTemp.Substring(0,8);
                from = filenameDateTemp.Substring(8);
            }


            //Parse date to
            DateTime dateTo = StringToDateTime(to, ref isDateTime);
            if (!isDateTime)
            {
                reason = "Invalid date";
                return false;
            }
            
            //Parse date from
            DateTime dateFrom = StringToDateTime(from, ref isDateTime);
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



            //Keep this code is running correct

            filename = filenameDateTemp + "-" + filenameNotDate;

            //Parse first hyphen
            int firstIndexHyphen = filename.IndexOf("-");
            //if (firstIndexHyphen <= 0) isStandard= false;

            //Parse second hyphen
            int secondIndexHyphen = filename.Substring(firstIndexHyphen + 1).IndexOf("-");
            if (secondIndexHyphen <= 0)
            {
                reason = "Invalid ticker";
                return false;
            }

            int realSecondIndexHyphen = firstIndexHyphen + secondIndexHyphen;

            //Parse code
            string code = filename.Substring(firstIndexHyphen + 1, secondIndexHyphen).Trim();

            //Parse keyword
            string extension = Path.GetExtension(filename);
            string keyword = filename.Substring(realSecondIndexHyphen + 1).Replace(extension, "").Trim();



            var company = db.Local_Companies.Where(t => t.Ticker == code).FirstOrDefault();
            if (company==null)
            {
                reason = "Not exist ticker";
                return false;
            }


            var classifyList = db.Local_Classifies.ToList();
            var classifyListFound = Utils.FindClassifyFromKeyword(keyword, classifyList);

            if (classifyListFound.Count == 0)
            {
                isStandard = false;
                reason = "Not exist keyword";
            }
            
            // Assign data to class file            
            classFile.Code = code;
            classFile.IsStandard = isStandard;
            classFile.Keyword = keyword;
            classFile.DateFrom = dateFrom;
            classFile.DateTo = dateTo;            
            classFile.CompanyId = company.Id.ToString();
            classFile.ListClassify = classifyListFound;

            return isStandard;
        }


        private int GetPostionFirstCharacter(string filename)
        {
            int length = filename.Length;
            for (int i = 0; i < length; i++)
            {
                var value = filename[i];
                if( ( value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z'))
                {
                    return i;
                }
            }

            return 0;
        }

        private bool CheckIsNumberic()
        {
            return true;
        }

        private bool CheckIsOneDate(string input)
        {
            return true;
        }

        private bool CheckIsTwoDate(string input)
        {
            return true;
        }

        private DateTime StringToDateTime(string inputDate, ref bool isDateTime)
        {
            isDateTime = true;
            DateTime result = new DateTime();
            try
            {
                result = DateTime.ParseExact(inputDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                isDateTime = false;
                Helpers.LogHelper.WriteLog(ex.Message);
            }            
            return result;
        }

        private DateTime StringToDateTime(string inputDate)
        {            
            DateTime result = new DateTime();
            try
            {
                result = DateTime.ParseExact(inputDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {                
                Helpers.LogHelper.WriteLog(ex.Message);
            }
            return result;
        }

        private void treeViewClassify_AfterCheck(object sender, TreeViewEventArgs e)
        {

            string name = e.Node.Name;
            
            //string name = treeViewClassify.SelectedNode.Name.ToString();
            //txtSelectedClassify.Text = "";
            //txtClassifyId.Text = "";
            int Id = 0;
            int.TryParse(name, out Id);

            

            var classify = db.Local_Classifies.Where(t => t.Id == Id).FirstOrDefault();
            // assign data
            if (!string.IsNullOrEmpty(classify.Code) && e.Node.FirstNode == null && e.Node.LastNode == null)
            {
                string code = "[" + classify.Code + "] ";
                txtClassifyId.ReadOnly = false;
                txtClassifyId.ForeColor = Color.BlueViolet;
                //add if check
                if (e.Node.Checked)
                {
                    txtSelectedClassify.Text = AddRemoveCheck(txtSelectedClassify.Text, code + classify.Name, true);
                    txtClassifyId.Text = AddRemoveCheck(txtClassifyId.Text, classify.Id.ToString(), true);                    
                }
                else//remove if uncheck
                {
                    txtSelectedClassify.Text = AddRemoveCheck(txtSelectedClassify.Text, code + classify.Name, false);
                    txtClassifyId.Text = AddRemoveCheck(txtClassifyId.Text, classify.Id.ToString(), false);
                }               

                
                
            }

            lblSelectedMessage.Text = txtSelectedClassify.Text;
        }
        

        private string AddRemoveCheck(string inputId, string id, bool isAdd)
        {
            var arrInput = inputId.Split(',');
            

            if (isAdd)
            {
                if (!arrInput.Contains(id))
                {
                    if (string.IsNullOrEmpty(inputId)) return id;
                    else return inputId + "," + id;
                }
            }
            else
            {
                if (arrInput.Contains(id))
                {
                    var list = arrInput.ToList();
                    list.Remove(id);
                    return string.Join(",", list.ToArray());
                }
            }
            return inputId;
        }
        private void treeViewClassify_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {                
                if (e.Node.Checked)
                {
                    e.Node.Checked = false;
                }
                else
                {
                    e.Node.Checked = true;
                }
                
            }

        }

        
    }
}
