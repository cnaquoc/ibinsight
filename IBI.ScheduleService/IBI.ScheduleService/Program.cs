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
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

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
            var services = new ServiceCollection();
            ConfigureServices(services);

            SYS.ServiceProvider = services.BuildServiceProvider();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);              
            Application.Run(new frmMain(SYS.ServiceProvider.GetService<ApplicationDbContext>()));
        }


        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    ConfigurationManager.ConnectionStrings["ApplicationDBConnectionString"].ConnectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 100,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    }
                ),
                ServiceLifetime.Transient
            );

            services.AddLogging();

            //services.UseScheduleServiceDb();

            services.UseIBIDatabase();

            //services.UseDownloader();
        }
    }
}
