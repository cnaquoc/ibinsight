using IBI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using IBI.Data.IManagers;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

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
            ApplicationDbContext context = new ApplicationDbContext();           
            Application.Run(new frmMain(context));
            
        }

        
    }
}
