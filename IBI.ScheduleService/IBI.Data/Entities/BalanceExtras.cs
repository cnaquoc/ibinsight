using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Data.Entities
{
    public class BalanceExtras: BaseEntityFinance
    {
        //public Guid CompanyId { get; set; }
        public double TaiSanThueNgoai { get; set; }
        public double VatTuHangHoaNhanGiuHoNhanGiaCong { get; set; }
        public double HangHoaNhanBanHoNhanKyGui { get; set; }
        public double NoKhoDoiDaXuLy { get; set; }
        public double NgoaiTeCacLoai { get; set; }
        public double Usd { get; set; }
        public double Euro { get; set; }
        public double Gbp { get; set; }
        public double Aud { get; set; }
        public double Lak { get; set; }
        public double Khac { get; set; }
        public double DuToanChiHoatDong { get; set; }


    }
}
