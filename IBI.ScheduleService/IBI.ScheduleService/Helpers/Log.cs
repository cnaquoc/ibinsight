using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IBI.ScheduleService.Helpers
{
    public class LogHelper
    {
        public static void WriteLog(string log, string Module = "")
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string Now = DateTime.Now.ToString().Replace("/", ".");
                string NowShort = DateTime.Now.ToShortDateString().Replace("/", ".");
                sb.AppendLine(Now);
                sb.AppendLine("**************************");
                sb.Append("Error: " + Module);
                sb.AppendLine();
                sb.Append(log);
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("**************************");

                string fileName = "/Logs/Log_" + NowShort + ".txt";
                fileName = fileName.Replace("/", "\\");
                string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var path = directory + "/Logs";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filePath = directory + fileName;
                {

                    if (!File.Exists(filePath))
                    {
                        using (StreamWriter outfile = new StreamWriter(filePath))
                        {
                            outfile.Write(sb.ToString());
                            outfile.WriteLine();
                        }
                    }
                    else
                    {
                        using (StreamWriter outfile = File.AppendText(filePath))
                        {
                            outfile.Write(sb.ToString());
                            outfile.WriteLine();
                        }
                    }

                }

            }
            catch
            {

            }


        }


    }
}
