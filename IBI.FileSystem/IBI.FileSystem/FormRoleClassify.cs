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
    public partial class FormRoleClassify : Form
    {
        private DataClasses_LocalDataContext db = null;
        public FormRoleClassify()
        {
            InitializeComponent();
            db = DB_Singleton.GetDatabase();
            LoadComboRole();
            LoadGrid();            
            FormatGrid();
        }

        private void LoadGrid()
        {
            var listRC = db.Local_Role_Classifies.ToList();
            var listR = db.Local_AspNetRoles.ToList();
            var listC = db.Local_Classifies.ToList();

            var listMain = listRC.Select(t => new
            {
                Id = t.Id,
                RoleName = listR.Where(r => r.Id == t.RoleId).FirstOrDefault().Name,
                ClassifyName = listC.Where(c => c.Id == t.ClassifyId).FirstOrDefault().Name
            }).ToList();

            dataGridViewMapping.DataSource = listMain;

            

        }

        private void FormatGrid()
        {
            dataGridViewMapping.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;


            dataGridViewMapping.Columns[0].HeaderText = "Id";
            dataGridViewMapping.Columns[0].Width = 10;
            dataGridViewMapping.Columns[0].Visible = false;

            dataGridViewMapping.Columns[1].HeaderText = "Role name";
            dataGridViewMapping.Columns[1].Width = 400;

            dataGridViewMapping.Columns[2].HeaderText = "Classify name";
            dataGridViewMapping.Columns[2].AutoSizeMode =DataGridViewAutoSizeColumnMode.Fill;

            //Add Delete button to grid
            DataGridViewButtonColumn btnColDelete = new DataGridViewButtonColumn();
            btnColDelete.HeaderText = "Action";
            btnColDelete.Text = "Delete";
            btnColDelete.Name = "btnDelete";
            btnColDelete.Width = 80;
            btnColDelete.UseColumnTextForButtonValue = true;
            dataGridViewMapping.Columns.Add(btnColDelete);
        }

        private void LoadComboRole()
        {           
            var listRole = db.Local_AspNetRoles.ToList();
            cbbRole.DisplayMember = "Name";
            cbbRole.ValueMember = "Id";
            cbbRole.DataSource = listRole;
        }


        private void LoadComboClassify()
        {
            string RoleId = cbbRole.SelectedValue.ToString();
            var arrayMapped = db.Local_Role_Classifies.Where(t => t.RoleId == RoleId).Select(t => t.ClassifyId).ToArray();

            var list = db.Local_Classifies.Where(t=> !arrayMapped.Contains(t.Id) && t.Code !="").Select(s=>new {
                Id= s.Id,
                Name = "[" + s.Code +"] " + s.Name
            }).ToList();
            cbbClassify.DisplayMember = "Name";
            cbbClassify.ValueMember = "Id";
            cbbClassify.DataSource = list;
        }

        private void btnMapping_Click(object sender, EventArgs e)
        {
            string error = ValidateMapping();
            if (error=="")
            {
                Local_Role_Classify role_Classify = new Local_Role_Classify();
                role_Classify.RoleId = cbbRole.SelectedValue.ToString();
                role_Classify.ClassifyId = int.Parse(cbbClassify.SelectedValue.ToString());
                db.Local_Role_Classifies.InsertOnSubmit(role_Classify);
                db.SubmitChanges();

                LoadComboClassify();
                LoadGrid();
                MessageBox.Show("Mapping is successful!");
            }
            else
            {
                MessageBox.Show(error);
            }
            
        }

        private string ValidateMapping()
        {
            int usergroupvalue = Helpers.Utils.StringToInt(cbbClassify.SelectedValue.ToString());
            if (usergroupvalue == 0)
            {
                return "Please choose another role. This role is full assignment!";
            }

            if (cbbRole.SelectedValue == null)
            {
                return "Please choose a role to map!";
            }

            return "";
        }

        private void cbbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComboClassify();
        }

        private void dataGridViewMapping_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >-1) // column button Delete
            {
                var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DataGridViewRow row = this.dataGridViewMapping.CurrentRow;
                    int Id = Helpers.Utils.StringToInt(row.Cells["Id"].Value.ToString());
                    if (Id > 0)
                    {
                        var role_Classify = db.Local_Role_Classifies.Where(t => t.Id == Id).FirstOrDefault();
                        db.Local_Role_Classifies.DeleteOnSubmit(role_Classify);
                        db.SubmitChanges();

                        LoadComboClassify();
                        LoadGrid();
                        MessageBox.Show("Delete is successful!");
                    }
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
