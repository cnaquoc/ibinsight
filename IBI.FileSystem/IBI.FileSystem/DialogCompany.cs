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
    public partial class DialogCompany : Form
    {
        private string _Id = "";
        private string _Name = "";
        public string getId { get { return _Id; } }
        public string getName { get { return _Name; } }

        private DataClasses_LocalDataContext db = null;
        public DialogCompany()
        {
            InitializeComponent();
            db = DB_Singleton.GetDatabase();
            LoadData();
            FormatGrid();
        }

        private void LoadData()
        {
            var listCompany = db.Local_Companies.Select(t=> new {
                Id =t.Id,
                Name=t.Name,                
                Ticker =t.Ticker,
                TaxCode = t.TaxCode,
            }).ToList();
            dataGridView_Company.DataSource = listCompany;

            ShowTotal(listCompany.Count());
        }

        private void ShowTotal(int number)
        {
            lblTotal.Text = "Total: " + number.ToString();
        }
        private void FormatGrid()
        {
            dataGridView_Company.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;
            dataGridView_Company.RowHeadersVisible = false;
            dataGridView_Company.Columns[0].HeaderText = "Id";
            dataGridView_Company.Columns[0].Width = 10;
            dataGridView_Company.Columns[0].Visible = false;

            dataGridView_Company.Columns[1].HeaderText = "Name";
            dataGridView_Company.Columns[1].Width = 300;
            dataGridView_Company.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView_Company.Columns[2].HeaderText = "Ticker";
            dataGridView_Company.Columns[2].Width = 100;

            dataGridView_Company.Columns[3].HeaderText = "Tax code";
            dataGridView_Company.Columns[3].Width = 100;

            //Add Select button to grid
            DataGridViewButtonColumn btnColSelect = new DataGridViewButtonColumn();
            btnColSelect.HeaderText = "Action";
            btnColSelect.Text = "Select";
            btnColSelect.Name = "btnSelect";
            btnColSelect.Width = 75;
            btnColSelect.UseColumnTextForButtonValue = true;
            dataGridView_Company.Columns.Add(btnColSelect);
        }
        
        private void dataGridView_Company_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex==0)
            {
                DataGridViewRow row = this.dataGridView_Company.CurrentRow;
                _Id = row.Cells["Id"].Value.ToString();
                _Name = row.Cells["Name"].Value.ToString();
                this.Close();
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var listCompany = db.Local_Companies.Where(s=>s.Ticker.Contains(txtSearch.Text.Trim()) || s.TaxCode.Contains(txtSearch.Text.Trim())).Select(t => new {
                Id = t.Id,
                Name = t.Name,
                Ticker = t.Ticker,
                TaxCode = t.TaxCode,
            }).ToList();
            dataGridView_Company.DataSource = listCompany;            
            ShowTotal(listCompany.Count());
        }
    }
}
