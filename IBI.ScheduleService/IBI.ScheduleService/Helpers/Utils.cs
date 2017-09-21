using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.ScheduleService.Helpers
{
    public class Utils
    {
        public static double StringToDouble(string input)
        {
            double result = 0;

            input = input.Replace(".", "");
            input = input.Replace(",", ".");

            double.TryParse(input, out result);

            return result;
        }

        public static string GetConfigValue(string key)
        {
            try
            {
                string result = ConfigurationSettings.AppSettings[key].ToString();
                return result;
            }
            catch (Exception)
            {
            }
            return "";
        }
    }
}
