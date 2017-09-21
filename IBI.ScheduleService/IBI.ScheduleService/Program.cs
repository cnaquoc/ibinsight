using IBI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using IBI.Data.IManagers;

namespace IBI.ScheduleService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Test;Integrated Security=true";            
            ApplicationDbContext context = new ApplicationDbContext();
            //IStockPriceManager stockpriceManager =new ;
            Application.Run(new frmMain(context));
            
        }

        
    }
}
