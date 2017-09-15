using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.FileSystem.Helpers
{
    public class ClassFile
    {
        public string FileName { get; set; }
        public bool IsStandard { get; set; }
        public string Code { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Keyword { get; set; }
        public string FileNameOnly { get; set; }
        public int ClassifyId { get; set; }
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string FileGUID { get; set; }
        public string Reason { get; set; }
        public List<Local_Classify> ListClassify { get; set;}
    }
}
