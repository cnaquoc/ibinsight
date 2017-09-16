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
    public partial class FormManager : Form
    {
        private DataClasses_LocalDataContext db = null;
        TreeNode parentNode = null;
        public FormManager()
        {
            InitializeComponent();
            db = DB_Singleton.GetDatabase();
            lblTotalAll.Text = "";
            LoadTreeView();
            
        }


        private List<Local_Classify> GetAllChildManyLevel(List<Local_Classify> allClassifies, int parentid)
        {
            List<Local_Classify> returnList = new List<Local_Classify>();
            GetAllChildRecurr(allClassifies, parentid, returnList);
            return returnList;            
            
        }

        private List<Local_Classify> GetAllChildRecurr(List<Local_Classify> allClassifies, int parentid, List<Local_Classify> returnList)
        {
            List<Local_Classify> children = allClassifies.Where(t => t.ParentId == parentid).ToList();
            if (children.Count > 0)
            {
                returnList.AddRange(children);
                foreach (var child in children)
                {
                    GetAllChildRecurr(allClassifies, child.Id, returnList);                     
                }                
            }
            return returnList;

        }





        public void LoadTreeView()
        {


            treeViewClassify.Nodes.Clear();
            var allClassifies = db.Local_Classifies.ToList();

            

            var classifies = db.Local_Classifies.Where(t => (t.ParentId ?? 0) == 0).ToList();

            var listFiles = db.Local_Files.ToList();

            int totalFileUploadParent = 0;
            int totalFileDoneParent = 0;

            foreach (var classify in classifies)
            {
                //get all child from parent
                var arrayAllchild = GetAllChildManyLevel(allClassifies, classify.Id).Select(t=>t.Id).ToArray();                               

                //get info from files
                int totalFileUpload = listFiles.Where(t => arrayAllchild.Contains( t.ClassifyId)).Count();
                int totalFileDone = listFiles.Where(t => arrayAllchild.Contains(t.ClassifyId) && t.IsDone==true).Count();

                if (arrayAllchild.Count() == 0)
                {
                    totalFileUpload = listFiles.Where(t => t.ClassifyId == classify.Id).Count();
                    totalFileDone = listFiles.Where(t => t.ClassifyId == classify.Id && t.IsDone == true).Count();
                }

                totalFileUploadParent +=  totalFileUpload;
                totalFileDoneParent += totalFileDone;

                string report = " (" + totalFileDone.ToString() + "/" + totalFileUpload.ToString() + ")";
                if (totalFileUpload == 0) report = "";

                string code = "[" + classify.Code + "] ";
                if (string.IsNullOrEmpty(classify.Code)) code = "";

                string nodeName = code + classify.Name + report;
                parentNode = treeViewClassify.Nodes.Add(nodeName);
                parentNode.Name = classify.Id.ToString();
                PopulateTreeView(classify.Id, parentNode, listFiles, allClassifies);
            }

            treeViewClassify.ExpandAll();

            string reportparent = "Total file done/file uploaded:  (" + totalFileDoneParent.ToString() + "/" + totalFileUploadParent.ToString() + ")";
            if (totalFileUploadParent == 0) reportparent = "";
            lblTotalAll.Text = reportparent;
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode, List<Local_File> listFiles, List<Local_Classify> allClassifies)
        {
            var children = db.Local_Classifies.Where(t => t.ParentId == parentId).ToList();



            TreeNode childNode;

            

            foreach (var child in children)
            {
                var arrayAllchild = GetAllChildManyLevel(allClassifies, child.Id).Select(t => t.Id).ToArray();

                //get info from files
                int totalFileUpload = listFiles.Where(t => arrayAllchild.Contains(t.ClassifyId)).Count();
                int totalFileDone = listFiles.Where(t => arrayAllchild.Contains(t.ClassifyId) && t.IsDone == true).Count();

                if (arrayAllchild.Count()==0)
                {
                    totalFileUpload = listFiles.Where(t => t.ClassifyId == child.Id).Count();
                    totalFileDone = listFiles.Where(t => t.ClassifyId == child.Id && t.IsDone == true).Count();
                }

                //get info from files
                //int totalFileUpload = listFiles.Where(t => t.ClassifyId == child.Id).Count();
                //int totalFileDone = listFiles.Where(t => t.ClassifyId == child.Id && t.IsDone == true).Count();

                //totalFileUploadParent += totalFileUpload;
                //totalFileDoneParent += totalFileDone;

                string report= " (" + totalFileDone.ToString() + "/" + totalFileUpload.ToString() + ")";
                if (totalFileUpload == 0) report = "";

                string code = "[" + child.Code + "] ";
                if (string.IsNullOrEmpty(child.Code)) code = "";

                string nodeName = code + child.Name + report;

                if (parentNode == null)
                    childNode = treeViewClassify.Nodes.Add(nodeName);
                else
                    childNode = parentNode.Nodes.Add(nodeName);

                childNode.Name = child.Id.ToString();

                PopulateTreeView(child.Id, childNode, listFiles, allClassifies);
            }
            //if (children.Count > 0)
            //{
            //    string reportparent = " (" + totalFileDoneParent.ToString() + "/" + totalFileUploadParent.ToString() + ")";
            //    if (totalFileUploadParent == 0) reportparent = "";
            //    parentNode.Text = parentNode.Text + reportparent;
            //}

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
