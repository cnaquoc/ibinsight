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
    public partial class FormRoleUsergroup : Form
    {
        private DataClasses_LocalDataContext db = null;
        public FormRoleUsergroup()
        {
            InitializeComponent();
            db = DB_Singleton.GetDatabase();
            LoadComboRole();
            
            LoadGrid();
            FormatGrid();
        }

        private void LoadGrid()
        {            
            var listRU = db.Local_Role_UserGroups.ToList();
            var listR = db.Local_AspNetRoles.ToList();
            var listU = db.Local_UserGroups.ToList();

            var listMain = listRU.Select(t => new
            {
                Id = t.Id,
                RoleName = listR.Where(r => r.Id == t.RoleId).FirstOrDefault().Name,
                GroupName = listU.Where(c => c.Id == t.UserGroupId).FirstOrDefault().GroupName
            }).ToList();

            dataGridViewMapping.DataSource = listMain;
            
        }

        private void FormatGrid()
        {
            //Format grid
            dataGridViewMapping.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridViewMapping.Columns[0].HeaderText = "Id";
            dataGridViewMapping.Columns[0].Width = 10;
            dataGridViewMapping.Columns[0].Visible = false;

            dataGridViewMapping.Columns[1].HeaderText = "Role name";
            dataGridViewMapping.Columns[1].Width = 300;

            dataGridViewMapping.Columns[2].HeaderText = "User group";
            dataGridViewMapping.Columns[2].Width = 300;



            //Add Delete button to grid

            DataGridViewButtonColumn btnColDelete = new DataGridViewButtonColumn();
            btnColDelete.HeaderText = "Action";
            btnColDelete.Text = "Delete";
            btnColDelete.Name = "btnDelete";
            btnColDelete.Width = 80;
            btnColDelete.UseColumnTextForButtonValue = true;

            if (!dataGridViewMapping.Columns.Contains("btnDelete"))
            {
                dataGridViewMapping.Columns.Add(btnColDelete);
            }
        }

        private void LoadComboRole()
        {           
            var listRole = db.Local_AspNetRoles.ToList();
            cbbRole.DisplayMember = "Name";
            cbbRole.ValueMember = "Id";
            cbbRole.DataSource = listRole;
        }


        private void LoadComboUsergroup()
        {
            string RoleId = cbbRole.SelectedValue.ToString();
            var arrayMapped = db.Local_Role_UserGroups.Where(t => t.RoleId == RoleId).Select(t => t.UserGroupId).ToArray();

            var list = db.Local_UserGroups.Where(t=> !arrayMapped.Contains(t.Id)).ToList();
            cbbUsergroup.DisplayMember = "GroupName";
            cbbUsergroup.ValueMember = "Id";
            cbbUsergroup.DataSource = list;
        }

        private void btnMapping_Click(object sender, EventArgs e)
        {
            string error = ValidateMapping();
            if (error=="")
            {
                Local_Role_UserGroup role_UserGroup = new Local_Role_UserGroup();
                role_UserGroup.RoleId = cbbRole.SelectedValue.ToString();
                role_UserGroup.UserGroupId = int.Parse(cbbUsergroup.SelectedValue.ToString());
                db.Local_Role_UserGroups.InsertOnSubmit(role_UserGroup);
                db.SubmitChanges();

                LoadComboUsergroup();
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
            if (cbbUsergroup.SelectedValue ==null)
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
            LoadComboUsergroup();
        }

        private void dataGridViewMapping_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1) // column button Delete
            {
                var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DataGridViewRow row = this.dataGridViewMapping.CurrentRow;
                    int Id = Helpers.Utils.StringToInt(row.Cells["Id"].Value.ToString());
                    if (Id > 0)
                    {
                        var role_UserGroup = db.Local_Role_UserGroups.Where(t => t.Id == Id).FirstOrDefault();
                        db.Local_Role_UserGroups.DeleteOnSubmit(role_UserGroup);
                        db.SubmitChanges();

                        LoadComboUsergroup();
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
