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
using IBI.Core;


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

        private int RetryUpcomCount = 0;
        private int RetryHoseCount = 0;
        private int RetryHnxCount = 0;
        public frmMain(ApplicationDbContext context)
        {
            _context = context;            
            InitializeComponent();
            lblMessage.Text = "";
            //this.FormBorderStyle = FormBorderStyle.None;
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            
            string autorun=  Utils.GetConfigValue("Autorun");
            if (autorun=="true")
            {
                btnRun_Click(this, new EventArgs());
            }
        }


        private void btnRun_Click(object sender, EventArgs e)
        {
            LogHelper.WriteLog("Start for three");
            RetryUpcomCount = 0;
            //string url =Utils.GetConfigValue("UrlHose");

            RunHose();
            //RunHnx();
            //RunUpcom();

            btnRun.Visible = false;
        }

        private void RunHose()
        {
            lblMessage.Text = "Processing Hose...";
            string urlHose = Utils.GetConfigValue("UrlHose").Replace("*", "&");
            webBrowserHose.Navigate(urlHose);
        }

        private void RunHnx()
        {
            lblMessage.Text = "Processing Hnx...";
            string urlHnx = Utils.GetConfigValue("UrlHnx").Replace("*", "&");
            webBrowserHnx.Navigate(urlHnx);
        }

        private void RunUpcom()
        {
            lblMessage.Text = "Processing Upcom...";
            string urlUpcom = Utils.GetConfigValue("UrlUpcom").Replace("*", "&");
            webBrowserUpcom.Navigate(urlUpcom);
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

        private bool UpdateData(List<StockPrice> list)
        {
            try
            {
                _context.StockPrices.UpdateRange(list);
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
            timerHose.Interval = 20000; // 10 seconds
            timerHose.Start();
            LogHelper.WriteLog("Start Hose");
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
            bool isComplete = false;
            try
            {
                List<StockPrice> listInsert = new List<StockPrice>();
                List<StockPrice> listUpdate = new List<StockPrice>();
                ParseData(listInsert, listUpdate, webBrowserHose);

                bool isSuccessUpdate = false;
                bool isSuccessInsert = false;
                isSuccessInsert = SaveData(listInsert);
                isSuccessUpdate = UpdateData(listUpdate);

                if (listInsert.Count > 0 || listUpdate.Count > 0)
                {
                    if (isSuccessUpdate && isSuccessInsert)
                    {
                        lblMessage.Text = "Successful Hose";
                        LogHelper.WriteLog("Successful Hose");
                        
                    }
                    else
                    {
                        lblMessage.Text = "Failed Hose!";
                        LogHelper.WriteLog("Failed Hose!");
                    }
                }
                else
                {
                    lblMessage.Text = "No data Hose!";
                    LogHelper.WriteLog("No data Hose!");
                }
                timerHose.Stop();
                LogHelper.WriteLog("Stop Hose!");
                isComplete = true;
            }
            catch
            {
                RetryHose();
                if (RetryHoseCount == 10)
                {
                    isComplete = true;
                    RetryHoseCount = 0;
                }
                else
                {
                    return;
                }
                
            }
            if (isComplete)
            {
                RunHnx();
            }
            
        }

        private void ParseData(List<StockPrice> listInsert, List<StockPrice> listUpdate, WebBrowser wb)
        {

            try
            {
                DateTime currentDate = DateTime.Now.Date;

                var oldList = _context.StockPrices.Where(t => t.TransactionDate.Date.Equals(currentDate)).ToList();

                var tablerows = wb.Document.GetElementById("tbLP");

                var bodyrows = tablerows.GetElementsByTagName("tbody");

                if (bodyrows != null)
                {
                    var rows = bodyrows[0].GetElementsByTagName("tr");

                    foreach (HtmlElement row in rows)
                    {
                        bool isAddnew = true;
                        var nodeTicker = row.Children[0].GetElementsByTagName("span");
                        string ticker = nodeTicker[0].InnerText;
                        StockPrice stock = oldList.Where(t => t.Ticker == ticker).FirstOrDefault();
                        if (stock == null)
                        {
                            isAddnew = true;
                            stock = new StockPrice();
                            stock.Id = Guid.NewGuid();
                            stock.TransactionDate = DateTime.Now;
                            stock.Created = DateTime.Now;
                            stock.Ticker = ticker;
                        }
                        else
                        {
                            isAddnew = false;
                            stock.Modified = DateTime.Now;
                        }
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


                        if (stock.MainVolume == 0)
                        {
                            stock.OpenPrice = stock.ClosePrice = stock.HighPrice = stock.LowPrice = stock.PriorClosePrice;
                        }

                        if (isAddnew)
                            listInsert.Add(stock);
                        else
                            listUpdate.Add(stock);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timerHnx_Tick(object sender, EventArgs e)
        {
            bool isComplete = false;
            try
            {
                List<StockPrice> listInsert = new List<StockPrice>();
                List<StockPrice> listUpdate = new List<StockPrice>();
                ParseData(listInsert, listUpdate, webBrowserHnx);

                bool isSuccessUpdate = false;
                bool isSuccessInsert = false;
                isSuccessInsert = SaveData(listInsert);
                isSuccessUpdate = UpdateData(listUpdate);

                if (listInsert.Count > 0 || listUpdate.Count > 0)
                {
                    if (isSuccessUpdate && isSuccessInsert)
                    {
                        lblMessage.Text = "Successful Hnx";
                        LogHelper.WriteLog("Successful Hnx");                        
                    }
                    else
                    {
                        lblMessage.Text = "Failed Hnx!";
                        LogHelper.WriteLog("Successful Hnx");
                    }
                }
                else
                {
                    lblMessage.Text = "No data Hnx!";
                    LogHelper.WriteLog("No data Hnx!");
                }
                timerHnx.Stop();
                LogHelper.WriteLog("Stop Hnx!");
                isComplete = true;
            }
            catch
            {
                RetryHnx();
                if (RetryHnxCount== 10)
                {
                    isComplete = true;
                    RetryHnxCount = 0;
                }
                else
                {
                    return;
                }
                
            }
            if (isComplete)
            {
                RunUpcom();
            }
            
        }

        private void timerUpcom_Tick(object sender, EventArgs e)
        {
            bool isComplete = false;
            try
            {
                List<StockPrice> listInsert = new List<StockPrice>();
                List<StockPrice> listUpdate = new List<StockPrice>();
                ParseData(listInsert, listUpdate, webBrowserUpcom);

                bool isSuccessUpdate = false;
                bool isSuccessInsert = false;
                isSuccessInsert = SaveData(listInsert);
                isSuccessUpdate = UpdateData(listUpdate);

                if (listInsert.Count > 0 || listUpdate.Count > 0)
                {
                    if (isSuccessUpdate && isSuccessInsert)
                    {
                        lblMessage.Text = "Successful Upcom";
                        LogHelper.WriteLog("Successful Upcom");

                        Application.Exit();
                    }
                    else
                    {
                        lblMessage.Text = "Failed Upcom!";
                        LogHelper.WriteLog("Failed Upcom!");
                    }
                }
                else
                {
                    RetryUpcom();

                    lblMessage.Text = "No data Upcom!";
                    LogHelper.WriteLog("No data Upcom!");
                }

                btnRun.Visible = true;
                timerUpcom.Stop();
                LogHelper.WriteLog("Stop Upcom!");
                isComplete = true;
            }
            catch {
                RetryUpcom();
                if (RetryUpcomCount == 10)
                {
                    isComplete = true;
                    RetryUpcomCount = 0;
                }
                else
                {
                    return;
                }
                return;
            }

            if (isComplete)
            {
                Application.Exit();
            }

        }

        private void RetryUpcom()
        {
            if (RetryUpcomCount <=10)
            {
                RunUpcom();
            }
            RetryUpcomCount++;
        }

        private void RetryHose()
        {
            if (RetryHoseCount <= 10)
            {
                RunHose();
            }
            RetryHoseCount++;
        }

        private void RetryHnx()
        {
            if (RetryHnxCount <= 10)
            {
                RunHnx();
            }
            RetryHnxCount++;
        }

        private void webBrowserHnx_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            timerHnx.Interval = 20000; // 10 seconds
            timerHnx.Start();
            LogHelper.WriteLog("Start Hnx");
        }

        private void webBrowserUpcom_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            timerUpcom.Interval = 20000; // 10 seconds
            timerUpcom.Start();
            LogHelper.WriteLog("Start Upcom");
        }
    }
}
