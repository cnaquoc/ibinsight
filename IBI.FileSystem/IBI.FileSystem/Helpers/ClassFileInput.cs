using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.FileSystem.Helpers
{
    public class ClassFileInput
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Code { get; set; }
        public string TaxCode { get; set; }
        public string ClassifyName { get; set; }
        public string FileName { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string FileGUID { get; set; }
        public string OldFileName { get; set; }




    }
}
