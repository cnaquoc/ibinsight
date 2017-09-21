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
    public partial class DialogClassify : Form
    {
        private int _Id = 0;
        private string _Name = "";
        public int getId { get { return _Id; } }
        public string getName { get { return _Name; } }

        private List<Local_Classify> _list;

        private DataClasses_LocalDataContext db = null;
        public DialogClassify(List<Local_Classify> list)
        {
            InitializeComponent();
            _list = list;
            db = DB_Singleton.GetDatabase();
            LoadData();
            FormatGrid();
        }

        private void LoadData()
        {
            var listMain = _list.Select(t => new
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name
            }).ToList();
            dataGridView.DataSource = listMain;            
        }

        private void FormatGrid()
        {
            dataGridView.RowHeadersVisible = false;
            //dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridView.Columns[0].HeaderText = "Id";
            dataGridView.Columns[0].Width = 10;
            dataGridView.Columns[0].Visible = false;

            dataGridView.Columns[1].HeaderText = "Code";
            dataGridView.Columns[1].Width = 100;

            dataGridView.Columns[2].HeaderText = "Name";
            dataGridView.Columns[2].Width = 420;
                        

            //Add Select button to grid
            DataGridViewButtonColumn btnColSelect = new DataGridViewButtonColumn();
            btnColSelect.HeaderText = "Action";
            btnColSelect.Text = "Select";
            btnColSelect.Name = "btnSelect";
            btnColSelect.Width = 75;
            btnColSelect.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(btnColSelect);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                DataGridViewRow row = this.dataGridView.CurrentRow;
                _Id =Convert.ToInt32( row.Cells["Id"].Value.ToString());
                _Name = row.Cells["Name"].Value.ToString();
                this.Close();
            }
        }
    }
}
