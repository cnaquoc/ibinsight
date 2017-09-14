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
            
            LoadTreeView();
        }


        public void LoadTreeView()
        {
            var classifies = db.Local_Classifies.Where(t => (t.ParentId ?? 0) == 0).ToList();

            var listFiles = db.Local_Files.ToList();

            foreach (var classify in classifies)
            {
                //get info from files
                int totalFileUpload = listFiles.Where(t => t.ClassifyId == classify.Id).Count();
                int totalFileDone = listFiles.Where(t => t.ClassifyId == classify.Id && t.IsDone==true).Count();

                string report = " (" + totalFileDone.ToString() + "/" + totalFileUpload.ToString() + ")";
                if (totalFileUpload == 0) report = "";

                string code = "[" + classify.Code + "] ";
                if (string.IsNullOrEmpty(classify.Code)) code = "";

                string nodeName = code + classify.Name + report;
                parentNode = treeViewClassify.Nodes.Add(nodeName);
                parentNode.Name = classify.Id.ToString();
                PopulateTreeView(classify.Id, parentNode, listFiles);
            }

            treeViewClassify.ExpandAll();
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode, List<Local_File> listFiles)
        {
            var children = db.Local_Classifies.Where(t => t.ParentId == parentId).ToList();

            TreeNode childNode;
            foreach (var child in children)
            {
                //get info from files
                int totalFileUpload = listFiles.Where(t => t.ClassifyId == child.Id).Count();
                int totalFileDone = listFiles.Where(t => t.ClassifyId == child.Id && t.IsDone == true).Count();

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

                PopulateTreeView(child.Id, childNode, listFiles);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
