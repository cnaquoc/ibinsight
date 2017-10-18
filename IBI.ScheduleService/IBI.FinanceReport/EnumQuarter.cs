using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.FinanceReport
{
    public class EnumQuarter
    {
        [Flags]
        public enum Quarter
        {
            Q1 = 1,
            Q2 = 2,
            Q3 = 3,
            Q4 = 4,            
            D6 = 6,
            D9 = 9,
            N  = 12,          
        }

    }
}
