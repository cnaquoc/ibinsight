using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.FileSystem.Helpers
{
    public static class UserInfo
    {
        //User info
        public static string Id { get; set; }
        public static string Name { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }

        //Access rights
        public static bool IsAdmin { get; set; }
        public static bool IsManager { get; set; }
        public static bool IsUploader { get; set; }
        public static bool IsDataInput { get; set; }
    }
}
