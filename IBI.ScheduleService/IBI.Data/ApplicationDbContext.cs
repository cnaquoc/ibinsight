using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;

namespace IBI.Data
{
    public class ApplicationDbContext : DbContext
    {
        //private string _connectionString;
        public ApplicationDbContext() 
        {
            //_connectionString = connectionString;
        }

        public DbSet<Entities.StockPrice> StockPrices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {                    
            //string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Test1;Integrated Security=true"; //ConfigurationSettings.AppSettings["ApplicationDBConnectionStringData"].ToString();
            string connectionString = ConfigurationSettings.AppSettings["ApplicationDBConnectionString"].ToString();
            optionsBuilder.UseSqlServer(connectionString);
        }


    }
}
