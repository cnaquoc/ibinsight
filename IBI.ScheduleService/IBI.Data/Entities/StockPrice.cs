using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Data.Entities
{
    public class StockPrice: BaseEntity
    {
        public DateTime TransactionDate { get; set; }
        public String Ticker { get; set; }
        public String IsinCode { get; set; }
        public String BloombergCode { get; set; }
        public double Ceiling { get; set; }
        public double Floor { get; set; }
        public double PriorClosePrice { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double ChangePrice { get; set; }
        public double ChangePriceRatio { get; set; }
        public double LowPrice { get; set; }
        public double HighPrice { get; set; }
        public double AveragePrice { get; set; }
        public double MainVolume { get; set; }
        public double MainValue { get; set; }


    }
}
