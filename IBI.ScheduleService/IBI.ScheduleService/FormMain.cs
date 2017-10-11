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
            //string url =Utils.GetConfigValue("UrlHose");
            string urlHose = Utils.GetConfigValue("UrlHose").Replace("*", "&"); //"http://priceboard.fpts.com.vn/?s=34&t=aAll";
            webBrowserHose.Navigate(urlHose);

            string urlHnx = Utils.GetConfigValue("UrlHnx").Replace("*", "&"); //"http://priceboard.fpts.com.vn/?s=34&t=aHNXIndex";
            webBrowserHnx.Navigate(urlHnx);

            string urlUpcom = Utils.GetConfigValue("UrlUpcom").Replace("*", "&"); //"http://priceboard.fpts.com.vn/?s=34&t=aHNXUpcomIndex";
            webBrowserUpcom.Navigate(urlUpcom);

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

            var els = webBrowserHose.Document.GetElementsByTagName(tagName); // all elems with tag
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

        private void webBrowserHose_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        { 
            timerHose.Interval = 30000; // 10 seconds
            timerHose.Start();            
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
        private void timerHose_Tick(object sender, EventArgs e)
        {
            List<StockPrice> list = new List<StockPrice>();
            ParseData(list, webBrowserHose);
            if (list.Count > 0)
            {
                if (SaveData(list))
                {                    
                    lblMessage.Text = "Successful Hose";
                }
                else
                {
                    lblMessage.Text = "Failed Hose!";
                }
            }
            else
            {
                lblMessage.Text = "No data or duplicated Hose!";
            }
            //btnRun.Visible = true;            
            timerHose.Stop();
        }

        private void ParseData(List<StockPrice> list, WebBrowser wb)
        {
            DateTime currentDate = DateTime.Now.Date;

            var oldList = _context.StockPrices.Where(t => t.TransactionDate.Date.Equals(currentDate)).ToList();

            var tablerows = wb.Document.GetElementById("tbLP");

            var bodyrows = tablerows.GetElementsByTagName("tbody");

            if (bodyrows!=null)
            {
                var rows = bodyrows[0].GetElementsByTagName("tr");


                foreach (HtmlElement row in rows)
                {
                    StockPrice stock = new StockPrice();
                    if (!oldList.Where(t => t.Ticker == stock.Ticker).Any())
                    {
                        stock.Id = Guid.NewGuid();
                        stock.TransactionDate = DateTime.Now;
                        stock.Created = DateTime.Now;
                        var nodeTicker = row.Children[0].GetElementsByTagName("span");

                        stock.Ticker = nodeTicker[0].InnerText;
                        stock.PriorClosePrice = Convert.ToDouble(row.Children[1].InnerText);
                        stock.Ceiling = Convert.ToDouble(row.Children[2].InnerText);
                        stock.Floor = Convert.ToDouble(row.Children[3].InnerText);

                        stock.MainVolume = Convert.ToDouble(row.Children[21].InnerText);
                        stock.OpenPrice = Convert.ToDouble(row.Children[22].InnerText);
                        stock.ClosePrice = Convert.ToDouble(row.Children[11].InnerText);

                        stock.ChangePrice = Convert.ToDouble(row.Children[13].InnerText);
                        if (stock.PriorClosePrice != 0)
                            stock.ChangePriceRatio = Math.Round(stock.ChangePrice / stock.PriorClosePrice, 1);

                        stock.HighPrice = Convert.ToDouble(row.Children[23].InnerText);
                        stock.LowPrice = Convert.ToDouble(row.Children[24].InnerText);
                        stock.AveragePrice = (stock.HighPrice + stock.LowPrice) / 2;

                        list.Add(stock);
                    }
                }
            }

           

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timerHnx_Tick(object sender, EventArgs e)
        {
            List<StockPrice> list = new List<StockPrice>();
            ParseData(list, webBrowserHnx);
            if (list.Count > 0)
            {
                if (SaveData(list))
                {                    
                    lblMessage.Text = "Successful Hnx";
                }
                else
                {
                    lblMessage.Text = "Failed Hnx!";
                }
            }
            else
            {
                lblMessage.Text = "No data or duplicated Hnx!";
            }
            //btnRun.Visible = true;            
            timerHnx.Stop();
        }

        private void timerUpcom_Tick(object sender, EventArgs e)
        {
            List<StockPrice> list = new List<StockPrice>();
            ParseData(list, webBrowserUpcom);
            if (list.Count > 0)
            {
                if (SaveData(list))
                {
                    lblMessage.Text = "Successful Upcom";
                }
                else
                {
                    lblMessage.Text = "Failed Upcom!";
                }
            }
            else
            {
                lblMessage.Text = "No data or duplicated Upcom!";
            }
            btnRun.Visible = true;            
            timerUpcom.Stop();
        }

        private void webBrowserHnx_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            timerHnx.Interval = 60000; // 10 seconds
            timerHnx.Start();
        }

        private void webBrowserUpcom_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            timerUpcom.Interval = 90000; // 10 seconds
            timerUpcom.Start();
        }
    }
}
