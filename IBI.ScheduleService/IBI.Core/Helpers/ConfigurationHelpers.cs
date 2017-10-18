using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBI.Core
{
    public static class ConfigurationHelpers
    {
        const String KEYWORD_DETECT_APP = "AppDb";

        public static String GetDefaultConnectionString(this IConfiguration config)
        {
            return config.GetConnectionString("DefaultConnection");
        }

        public static String GetDefaultAzureBlobConnectionString(this IConfiguration config)
        {
            return config.GetConnectionString("AzureStorageConnection");
        }

        public static bool IsAppMode(this IConfiguration config)
        {
            return config.GetDefaultConnectionString().Contains(KEYWORD_DETECT_APP);
        }
    }
}
