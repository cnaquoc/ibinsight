using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBI.Data;
using IBI.Data.IManagers;
using Microsoft.Extensions.DependencyInjection;
using IBI.ImportStock;
using System.IO;
using System.Data.OleDb;
using IBI.Core;
using IBI.Data.Entities;

namespace ImportStock
{
    public partial class FormImportStock : Form
    {
        private readonly ApplicationDbContext _context;
        private readonly ICompanyManager _companyManager = IBI.ImportStock.SYS.ServiceProvider.GetService<ICompanyManager>();
        private bool IsStop { get; set; }
        public FormImportStock(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            txtFile.Text = "D:\\Parer\\Stock";
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = folderDialog.SelectedPath;
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            IsStop = false;
            btnImport.Enabled = false;
            btnStop.Visible = true;


            rtbLogging.Text = "";

            string folderImport = txtFile.Text;
            if (Directory.Exists(folderImport))
            {
                var files = Directory.GetFiles(folderImport).Where(t => t.ToLower().Contains(".xls") || t.Contains(".xlsx")).ToList();
                rtbLogging.Text = "Total files: " + files.Count + Environment.NewLine;
                lblCount.Text = "0/" + files.Count;

                int countFail = 0, countSuccess = 0;
                foreach (var filePath in files)
                {
                    if (IsStop) break;

                    var fileNameOnly = Path.GetFileName(filePath);
                    var stateText = "Processing " + fileNameOnly;
                    rtbLogging.AppendText(stateText);

                    bool isSuccess;
                    string msg = Environment.NewLine;
                    try
                    {
                        isSuccess = await ImportToDatabase(filePath);
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        msg = ex.Message + msg;
                    }

                    rtbLogging.SelectionStart = rtbLogging.TextLength - stateText.Length;
                    rtbLogging.SelectionLength = stateText.Length;
                    rtbLogging.SelectedText = "";
                    AppendTextWithColor(rtbLogging, fileNameOnly + (isSuccess ? " Success " : " Fail ") + msg, isSuccess ? Color.Green : Color.Red);

                    countSuccess += isSuccess ? 1 : 0;
                    countFail += !isSuccess ? 1 : 0;

                    lblCount.Text = (countSuccess + countFail) + "/" + files.Count + " - Fail: " + countFail;
                }
            }

            btnStop.Visible = false;
            btnImport.Enabled = true;
        }

        private void AppendTextWithColor(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            IsStop = true;
        }

        private async void FormImportStock_Load(object sender, EventArgs e)
        {
            string autoImport = Configuration.GetConfigValue("AutoImport");
            if (autoImport == "true")
            {
                await AutoImport();
            }
        }

        private async Task AutoImport()
        {
            string folderImport = Configuration.GetConfigValue("ImportFolder");
            if (Directory.Exists(folderImport))
            {
                var files = Directory.GetFiles(folderImport).Where(t => t.Contains(".xls") || t.Contains(".xlsx")).ToList();
                foreach (var fileName in files)
                {
                    await ImportToDatabase(fileName);
                }

            }

        }


        private async Task<bool> ImportToDatabase(string fileName)
        {
            string extension = System.IO.Path.GetExtension(fileName);
            if (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx")
            {
                return false;
            }
                        

            DataSet ds = ReadExcelFile(fileName);
                               
            
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
               
                bool isData = false;
                //find header row
                
                int colPlus = 0;

                List<StockPrice> listStock = new List<StockPrice>();

                foreach (DataRow dr in dt.Rows)
                {
                    colPlus = 0;

                    string FieldName = dr[0].ToString();
                    if (FieldName.IsEmpty())
                    {
                        FieldName = dr[1].ToString();
                        colPlus = 1;
                    }

                    if (!FieldName.IsEmpty())
                    {
                        string FieldNameTemp = StringHelpers.ConvertToUnSign(FieldName).Replace(" ", "").ToLower();
                        string Code = dr[1 + colPlus].ToString();

                        if (FieldNameTemp == "ticker" && Code.ToLower().Trim() == "per")
                        {                            
                            isData = true;
                            continue;
                        }

                        if (isData)
                        {

                            //Asign data
                            string ticker = dr[0].ToString();
                            string transactionDate = dr[2].ToString();
                            string openPrice = dr[4].ToString();
                            string highPrice = dr[5].ToString();
                            string lowPrice = dr[6].ToString();
                            string closePrice = dr[7].ToString();
                            string volume = dr[8].ToString();

                            // Validate 
                            if (ticker.Trim().Length !=3)
                            {
                                return false;
                            }                            
                            if (!StringHelpers.IsDateTime(transactionDate))
                            {
                                return false;
                            }
                            if (!StringHelpers.IsDouble(openPrice))
                            {
                                return false;
                            }
                            if (!StringHelpers.IsDouble(highPrice))
                            {
                                return false;
                            }
                            if (!StringHelpers.IsDouble(lowPrice))
                            {
                                return false;
                            }
                            if (!StringHelpers.IsDouble(closePrice))
                            {
                                return false;
                            }

                            if (!StringHelpers.IsDouble(volume))
                            {
                                return false;
                            }



                            StockPrice stock = new StockPrice()
                            {
                                Ticker = ticker,
                                TransactionDate = transactionDate.StringToDateTime(),
                                OpenPrice = openPrice.StringToDoubleExact(),
                                ClosePrice = closePrice.StringToDoubleExact(),
                                HighPrice = highPrice.StringToDoubleExact(),
                                LowPrice =lowPrice.StringToDoubleExact(),
                                MainVolume = volume.StringToDoubleExact()
                            };

                            listStock.Add(stock);
                        }
                    }

                    
                }
                //Process data
                if (listStock.Count() >0)
                {

                    //check douplicate
                    bool IsDouplicate = listStock.GroupBy(t => t.TransactionDate).Where(e => e.Count() > 1).Any();

                    if (!IsDouplicate)
                    {
                        using (ApplicationDbContext context = IBI.ImportStock.SYS.ServiceProvider.GetService<ApplicationDbContext>())
                        {
                            context.StockPrices.AddRange(listStock);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                
                
            }
            else
            {
                MessageBox.Show("Cannot read excel file!" + fileName);
                LogHelper.WriteLog("Cannot read excel file!" + fileName);
                return false;               

            }

            return true;
        }


        

        private DataSet ReadExcelFile(string fileName)
        {
            try
            {
                var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;

                if (System.IO.Path.GetExtension(fileName) == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;                    
                }


                using (var conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    var sheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "] ";

                        var adapter = new OleDbDataAdapter(cmd);
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);

            }
            return null;
        }
    }
}
