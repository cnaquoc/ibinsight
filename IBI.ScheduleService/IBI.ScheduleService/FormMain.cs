using IBI.Data;
using IBI.Data.Entities;
using IBI.Data.IManagers;
using IBI.ScheduleService.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace IBI.ScheduleService
{
    public partial class frmMain : Form
    {
        private readonly ApplicationDbContext _context;
        
        private const string TICKER = "quoteGrid-grid_SecuritySymbol";
        private const string ISIN_CODE = "quoteGrid-grid_Isin";
        private const string BLOOMBERG_CODE = "quoteGrid-grid_Bloomberg";
        private const string CEILING = "quoteGrid-grid_Ceiling";
        private const string FLOOR = "quoteGrid-grid_Floor";
        private const string PRIOR_CLOSE_PRICE = "quoteGrid-grid_PriorClosePrice";
        private const string OPEN_PRICE = "quoteGrid-grid_OpenPrice";
        private const string CLOSE_PRICE = "quoteGrid-grid_ClosePrice";
        private const string CHANGE_PRICE = "quoteGrid-grid_ChangePrice";
        private const string CHANGE_PRICE_RATIO = "quoteGrid-grid_ChangePriceRatio";
        private const string LOW_PRICE = "quoteGrid-grid_LowPrice";
        private const string HIGH_PRICE = "quoteGrid-grid_HighPrice";
        private const string AVERAGE_PRICE = "quoteGrid-grid_AveragePrice";
        private const string MAIN_VOLUME = "quoteGrid-grid_MainVolume";
        private const string MAIN_VALUE = "quoteGrid-grid_MainValue";
        public frmMain(ApplicationDbContext context)
        {
            _context = context;            
            InitializeComponent();
            lblMessage.Text = "";
            //this.FormBorderStyle = FormBorderStyle.None;
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {            
            string url =Utils.GetConfigValue("Url");
            webBrowser.Navigate(url);
            lblMessage.Text = "Processing...";
            btnRun.Visible = false;

        }
          
        private bool SaveData(List<StockPrice> list)
        {
            try
            {                
                _context.StockPrices.AddRange(list);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return false;
            }
        }

        private HtmlElement[] GetElementsByClassName(WebBrowser wb, string tagName, string className)
        {
            var l = new List<HtmlElement>();

            var els = webBrowser.Document.GetElementsByTagName(tagName); // all elems with tag
            foreach (HtmlElement el in els)
            {
                // getting "class" attribute value...
                // but stop! it isn't "class"! It is "className"! 0_o
                // el.GetAttribute("className") working, and el.GetAttribute("class") - not!
                // IE is so IE...
                //if (el.GetAttribute("className") == className)
                if (el.GetAttribute("className") == className)
                {
                    l.Add(el);
                }
            }

            return l.ToArray();
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        { 
            timer1.Interval = 10000; // 10 seconds
            timer1.Start();            
        }
              
        private StockPrice GetOneRowData(HtmlElement row)
        {
            StockPrice stock = new StockPrice();
            stock.TransactionDate = DateTime.Now;
            stock.Created = DateTime.Now;
            stock.Id = Guid.NewGuid();
            if (row.Children.Count > 0)
            {
                for (int i = 0; i < row.Children.Count; i++)
                {
                    var cell = row.Children[i];
                    UpdateRowData(cell, stock);
                }
            }

            return stock;
        }

        private void UpdateRowData(HtmlElement cell, StockPrice stock)
        {
            UpdateTicker(cell, stock);
            UpdateIsinCode(cell, stock);
            UpdateBloombergCode(cell, stock);
            UpdateCeiling(cell, stock);
            UpdateFloor(cell, stock);
            UpdatePriorClosePrice(cell, stock);
            UpdateOpenPrice(cell, stock);
            UpdateClosePrice(cell, stock);
            UpdateChangePrice(cell, stock);
            UpdateChangePriceRatio(cell, stock);
            UpdateLowPrice(cell, stock);
            UpdateHighPrice(cell, stock);
            UpdateAveragePrice(cell, stock);
            UpdateMainVolume(cell, stock);
            UpdateMainValue(cell, stock);
        }

        #region UpdateRowDataDetails

        private void UpdateTicker(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(TICKER))
            {
                stock.Ticker = cell.InnerText;
            }
        }

        private void UpdateIsinCode(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(ISIN_CODE))
            {
                stock.IsinCode = cell.InnerText;
            }
        }

        private void UpdateBloombergCode(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(BLOOMBERG_CODE))
            {
                stock.BloombergCode = cell.InnerText;
            }
        }

        private void UpdateCeiling(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(CEILING))
            {
                stock.Ceiling = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateFloor(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(FLOOR))
            {
                stock.Floor = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdatePriorClosePrice(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(PRIOR_CLOSE_PRICE))
            {
                stock.PriorClosePrice = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateOpenPrice(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(OPEN_PRICE))
            {
                stock.OpenPrice = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateClosePrice(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(CLOSE_PRICE))
            {
                stock.ClosePrice = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateChangePrice(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(CHANGE_PRICE))
            {
                stock.ChangePrice = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateChangePriceRatio(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(CHANGE_PRICE_RATIO))
            {
                stock.ChangePriceRatio = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateLowPrice(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(LOW_PRICE))
            {
                stock.LowPrice = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateHighPrice(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(HIGH_PRICE))
            {
                stock.HighPrice = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateAveragePrice(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(AVERAGE_PRICE))
            {
                stock.AveragePrice = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateMainVolume(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(MAIN_VOLUME))
            {
                stock.MainVolume = Utils.StringToDouble(cell.InnerText);
            }
        }

        private void UpdateMainValue(HtmlElement cell, StockPrice stock)
        {
            if (cell.OuterHtml.Contains(MAIN_VALUE))
            {
                stock.MainValue = Utils.StringToDouble(cell.InnerText);
            }
        }

        

        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now.Date;

            var oldList = _context.StockPrices.Where(t => t.TransactionDate.Date.Equals(currentDate)).ToList(); 
                        
            var rows = GetElementsByClassName(webBrowser, "tr", "ui-widget-content jqgrow ui-row-ltr");
            List<StockPrice> list = new List<StockPrice>();
            foreach (var row in rows)
            {                
                var stock = GetOneRowData(row);
                if (!oldList.Where(t=>t.Ticker== stock.Ticker).Any())
                {
                    list.Add(stock);
                }                                    
            }
           
            if (list.Count > 0)
            {
                if (SaveData(list))
                {
                    MessageBox.Show("Successful!");
                }
                else
                {
                    MessageBox.Show("Failed!");
                }
            }
            else
            {
                MessageBox.Show("No data or duplicated!");
            }


            btnRun.Visible = true;
            lblMessage.Text = "";
            timer1.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
