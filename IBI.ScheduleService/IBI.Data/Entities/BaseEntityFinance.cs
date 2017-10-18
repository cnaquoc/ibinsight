using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IBI.Data.Entities
{
    public abstract class BaseEntityFinance : BaseEntity
    {       
        public int Year { get; set; }
        public int QuarterType { get; set; }
        public Guid CompanyId { get; set; }
    }
}
