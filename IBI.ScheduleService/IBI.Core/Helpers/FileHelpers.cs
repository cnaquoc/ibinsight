using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace IBI.Core
{
    public static class FileHelpers
    {
        public static String DownloadTo(this String url, String fileName)
        {
            using (var client = new WebClient() )
            {
                client.DownloadFile(url, fileName);
            }

            return fileName;
        }

        public static void Run(this String file)
        {
            Process.Start(file);
        }
    }
}
