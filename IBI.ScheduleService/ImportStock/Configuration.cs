using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace IBI.ImportStock
{
    public class Configuration
    {
        public static string GetConfigValue(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString().ToLower();
            }
            catch (Exception)
            {
                //throw;
            }
            return "";
        }
    }
}
