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

namespace IBI.FileSystem
{
    public partial class FormClassify : Form
    {
        private DataClasses_LocalDataContext db = null;
        TreeNode parentNode = null;
        bool isAddNew = false;
        public FormClassify()
        {
            InitializeComponent();
            db = DB_Singleton.GetDatabase();            
            LoadTreeView();
            LoadCombobox();
            lblMessage.Text = "";
            lblStatus.Text = "";
            //btnDelete.Visible = false;
            btnSave.Visible = false;
        }

        private void LoadTreeView()
        {
            treeView.Nodes.Clear();
            var classifies = db.Local_Classifies.Where(t => (t.ParentId ?? 0) == 0).ToList();

            foreach (var classify in classifies)
            {
                string code = "[" + classify.Code + "] ";
                if (string.IsNullOrEmpty(classify.Code)) code = "";

                string nodeName = code + classify.Name;

                parentNode = treeView.Nodes.Add(nodeName);
                parentNode.Tag = classify.Code;
                parentNode.ToolTipText = classify.Code;
                parentNode.Name = classify.Id.ToString();
                
                PopulateTreeView(classify.Id, parentNode);
            }

            treeView.ExpandAll();
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            var children = db.Local_Classifies.Where(t => t.ParentId == parentId).ToList();

            TreeNode childNode;
            foreach (var child in children)
            {
                string code = "[" + child.Code + "] ";
                if (string.IsNullOrEmpty(child.Code)) code = "";

                string nodeName = code + child.Name;

                if (parentNode == null)
                    childNode = treeView.Nodes.Add(nodeName);
                else
                    childNode = parentNode.Nodes.Add(nodeName);

                childNode.Tag = child.Code;
                childNode.Name = child.Id.ToString();
                childNode.ToolTipText = child.Code;

                PopulateTreeView(child.Id, childNode);
            }
        }


        private void LoadComboboxManyLevel()
        {
            var listParent = db.Local_Classifies.ToList();
            
            var listData = new List<SelectList>();

            SelectList listEmpty = new SelectList()
            {
                Id = 0,
                Value = "No parent"
            };
            listData.Add(listEmpty);

            foreach (var item in listParent)
            {               
                //Find parent
                string value = FindParent(item.Name, item.ParentId??0) ;
                SelectList list = new SelectList()
                {
                    Id= item.Id,
                    Value = value
                };
                listData.Add(list);              
                
            }
            cbbParrent.DisplayMember = "Value";
            cbbParrent.ValueMember = "Id";
            cbbParrent.DataSource = listData;
        }

        private void LoadCombobox()
        {
            var listParent = db.Local_Classifies.ToList();

            var listData = new List<SelectList>();

            SelectList listEmpty = new SelectList()
            {
                Id = 0,
                Value = "No parent"
            };
            listData.Add(listEmpty);

            foreach (var item in listParent)
            {
                string code = "[" + item.Code + "] ";
                if (string.IsNullOrEmpty(item.Code)) code = "";
                string value = code + item.Name;
                SelectList list = new SelectList()
                {
                    Id = item.Id,
                    Value = value
                };
                listData.Add(list);

            }
            cbbParrent.DisplayMember = "Value";
            cbbParrent.ValueMember = "Id";
            cbbParrent.DataSource = listData;
        }

        private string FindParent(string name, int parentId)
        {
            if (parentId == 0)
                return name;
            var parent = db.Local_Classifies.Where(t => t.Id == parentId).FirstOrDefault();
            if (parent !=null)
            {
                name = parent.Name + "--" + name;
                return FindParent(name, parent.ParentId ?? 0);
            }
            return name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            //Validate input
            string error = ValidateInput();
            if (error=="")
            {
                bool isNew = string.IsNullOrEmpty(txtId.Text);
                Local_Classify classify = null;

                int parentId = 0;
                int.TryParse(cbbParrent.SelectedValue.ToString(), out parentId);

                int Id = 0;
                int.TryParse(txtId.Text, out Id);

                if (isNew) //Insert
                {
                    classify = new Local_Classify()
                    {
                        Code = txtCode.Text,
                        Name = txtName.Text,
                        ParentId = parentId,
                        Keyword = txtKeyword.Text
                    };
                    db.Local_Classifies.InsertOnSubmit(classify);
                    db.SubmitChanges();

                    parentNode = null;
                    LoadTreeView();
                    LoadCombobox();
                    MessageBox.Show("Insert data successful!");
                }
                else //Update
                {
                    classify = db.Local_Classifies.Where(t => t.Id == Id).FirstOrDefault();
                    if (classify!=null)
                    {
                        classify.Code = txtCode.Text;
                        classify.Name = txtName.Text;
                        classify.ParentId = parentId;
                        classify.Keyword = txtKeyword.Text;
                        db.SubmitChanges();

                        parentNode = null;
                        LoadTreeView();
                        
                        MessageBox.Show("Update data successful!");
                    }
                }
                    
            }
            else
            {
                lblMessage.Text = error;
            }            
        }

        private string ValidateInput()
        {
            //check empty
            if (string.IsNullOrEmpty(txtName.Text))
                return "Name is not empty!";

            

            //check exist code
            int Id = 0;
            int.TryParse(txtId.Text, out Id);

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                bool ExistCode = db.Local_Classifies.Where(t => t.Code.Equals(txtCode.Text) && t.Id != Id).Any();
                if (ExistCode)
                    return "Code existed in system!";
            }

            //check exist keyword
            //if (!string.IsNullOrEmpty(txtKeyword.Text))
            //{
            //    var classifyList = db.Local_Classifies.Where(t => t.Id != Id).ToList();

            //    var classify = Utils.ExistClassifyFromKeyword(txtKeyword.Text, classifyList);

            //    if (classify!=null)
            //        return "Keyword existed in system!";
            //}

            return "";
        }


        private void btnAddNew_Click(object sender, EventArgs e)
        {
            //reset form
            btnSave.Visible = true;
            isAddNew = true;
            ResetForm();
            lblStatus.Text = "Status: Add new [Click classify tree on left hand side to choose parent]";
        }


        private void ResetForm()
        {
            //reset form
            lblStatus.Text = "";
            lblMessage.Text = "";
            txtCode.Text = "";
            txtName.Text = "";
            txtId.Text = "";
            cbbParrent.SelectedIndex = 0;
            txtKeyword.Text = "";
        }


        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string name = treeView.SelectedNode.Name.ToString();

            int Id = 0;
            int.TryParse(name, out Id);

            var classify = db.Local_Classifies.Where(t => t.Id == Id).FirstOrDefault();
            // assign data
            if (isAddNew)
            {
                cbbParrent.SelectedValue = classify.Id;
            }
            else
            {
                txtName.Text = classify.Name;
                txtCode.Text = classify.Code;
                txtId.Text = classify.Id.ToString();
                cbbParrent.SelectedValue = classify.ParentId ?? 0;
                txtKeyword.Text = classify.Keyword;
            }
            

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int Id = 0;
            int.TryParse(txtId.Text, out Id);

            if (Id <=0)
            {
                MessageBox.Show("Please choose an item to delete!");
            }
            else
            {
                //Validate is leaf node
                bool NotLeafNode = db.Local_Classifies.Where(t => t.ParentId == Id).Any();

                if (NotLeafNode)
                {
                    MessageBox.Show("Please delete node leaf first!");
                    return;
                }

                //confirm
                var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo);
                if (result==DialogResult.Yes)
                {
                    //delete                   

                    var classify= db.Local_Classifies.Where(t => t.Id == Id).FirstOrDefault();
                    if (classify !=null)
                    {
                        db.Local_Classifies.DeleteOnSubmit(classify);
                        db.SubmitChanges();

                        parentNode = null;
                        LoadTreeView();
                        LoadCombobox();
                        ResetForm();
                        MessageBox.Show("Delete data is successful!");
                    }
                    else
                    {
                        MessageBox.Show("Delete data is failed!");
                    }
                    



                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isAddNew = false;
            btnSave.Visible = true;
            lblStatus.Text = "Status: Edit [Click classify tree on left hand side to edit]";
        }
    }
}
