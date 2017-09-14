using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.FileSystem
{
    public class DB_Singleton
    {
        private static DataClasses_LocalDataContext _instance = null;
        protected DB_Singleton()
        {

        }
        public static DataClasses_LocalDataContext GetDatabase()
        {
            if (_instance==null)
            {
                _instance = new DataClasses_LocalDataContext();
            }
            return _instance;
        }
        
    }
}
