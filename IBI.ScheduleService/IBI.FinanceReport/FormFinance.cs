using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel;
using System.Data.OleDb;
using IBI.Core;
using IBI.Data.Entities;
using IBI.Data;

namespace IBI.FinanceReport
{
    public partial class FormFinance : Form
    {
        private readonly ApplicationDbContext _context;
        
        private const string ZoneTaiSan = "taisan";
        private const string ZoneNguonVon = "nguonvon";
        private const string ZoneChiTieuNgoaiBang = "chitieungoaibang";
        private const string ZoneKetQuaKinhDoanh = "ketquakinhdoanh";
        private const string ZoneLuuChuyenTienTeGianTiep = "luuchuyentientegiantiep";
        private const string ZoneLuuChuyenTienTeTrucTiep = "luuchuyentientetructiep";
        public FormFinance(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel file |*.xls";
            if (fileDialog.ShowDialog() ==  DialogResult.OK)
            {
                txtFile.Text = fileDialog.FileName;
            }
            
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var fileName = @"D:\Test\AAM_20170506.xls";
            DataSet ds = ReadExcelFile(fileName);
            bool invalidField = false;
            string companyId = Guid.NewGuid().ToString();
            string Zone = "";
            if (ds !=null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                // get all columns name and data

                Dictionary<string, int> dic = new Dictionary<string, int>();

                bool isData = false;
                //find header row
                int rowStart = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    string FieldName = dr[0].ToString();
                    string FieldNameTemp = StringHelpers.ConvertToUnSign(FieldName).Replace(" ", "").ToLower();
                    string Code= dr[1].ToString();

                    if (FieldNameTemp == ZoneTaiSan && Code.ToLower().Trim() =="ms")
                    {
                        Zone = ZoneTaiSan;
                        isData = true;                        
                    }

                    if (isData)
                    {

                        for (int i = 2; i < dt.Columns.Count; i++)
                        {
                            int year = 0;
                            int quarter = 0;
                            if (GetYearQuarter(dr[i].ToString(), ref year, ref quarter))
                            {
                                dic.Add(quarter + "/" + year  , i);
                            }
                        }

                        break;
                    }

                    rowStart++;
                }

                // process data

                
                if (dic.Count>0)
                {
                    List<BalanceAssets> listInsertBalanceAssets = new List<BalanceAssets>();
                    List<BalanceCapitals> listInsertBalanceCapitals = new List<BalanceCapitals>();
                    List<BalanceExtras> listInsertBalanceExtras = new List<BalanceExtras>();
                    List<BusinessResults> listInsertBusinessResults = new List<BusinessResults>();
                    List<IndirectCashFlows> listInsertIndirectCashFlows = new List<IndirectCashFlows>();
                    List<DirectCashFlows> listInsertDirectCashFlows = new List<DirectCashFlows>();

                    foreach (var item in dic)
                    {
                        int year = Convert.ToInt32(item.Key.Substring(item.Key.Length - 4));
                        int quarter = Convert.ToInt32(item.Key.Substring(0, item.Key.Length - 5));
                        int columnIndex = item.Value;
                        //Add asset table 
                        BalanceAssets balanceAssets = new BalanceAssets();
                        InitBaseObject(balanceAssets, companyId, quarter, year);

                        //Add capital table 
                        BalanceCapitals balanceCapitals = new BalanceCapitals();
                        InitBaseObject(balanceCapitals, companyId, quarter, year);

                        //Add extra table 
                        BalanceExtras balanceExtras = new BalanceExtras();
                        InitBaseObject(balanceExtras, companyId, quarter, year);

                        //Add business result table 
                        BusinessResults businessResults = new BusinessResults();
                        InitBaseObject(businessResults, companyId, quarter, year);

                        //Add IndirectCashFlows table 
                        IndirectCashFlows indirectCashFlows = new IndirectCashFlows();
                        InitBaseObject(indirectCashFlows, companyId, quarter, year);

                        //Add business result table 
                        DirectCashFlows directCashFlows = new DirectCashFlows();
                        InitBaseObject(directCashFlows, companyId, quarter, year);


                        int rowCount = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            string fieldName = dr[0].ToString();
                            string FieldNameTemp = StringHelpers.ConvertToUnSign(fieldName).Replace(" ", "").ToLower();

                            GetZone(FieldNameTemp, ref Zone);

                            string code = dr[1].ToString();
                            string value = dr[columnIndex].ToString();
                            if (rowCount > rowStart)
                            {
                                fieldName = ConvertFieldName(fieldName);

                                //replace special case
                                fieldName = ReplaceSpeicalFieldName(fieldName, code);

                                if (!IgnoreSpecialCase(fieldName, code) && FieldNameTemp != Zone) // ignore rows is Zone or Empty 
                                {
                                    if (ValidateRow(fieldName, code, Zone))
                                    {
                                        switch (Zone)
                                        {
                                            case ZoneTaiSan:
                                                AssignValue(balanceAssets, fieldName, value);
                                                break;
                                            case ZoneNguonVon:
                                                AssignValue(balanceCapitals, fieldName, value);
                                                break;
                                            case ZoneChiTieuNgoaiBang:
                                                AssignValue(balanceExtras, fieldName, value);
                                                break;
                                            case ZoneKetQuaKinhDoanh:
                                                AssignValue(businessResults, fieldName, value);
                                                break;
                                            case ZoneLuuChuyenTienTeGianTiep:
                                                AssignValue(indirectCashFlows, fieldName, value);
                                                break;
                                            case ZoneLuuChuyenTienTeTrucTiep:
                                                AssignValue(directCashFlows, fieldName, value);
                                                break;
                                        }                                        

                                    }
                                    else
                                    {
                                        LogHelper.WriteLog("Field name is invalid: " + fieldName + " " + code, Zone);
                                        invalidField = true;
                                        break;
                                    }
                                }


                            }
                            rowCount++;

                        }



                        listInsertBalanceAssets.Add(balanceAssets);
                        listInsertBalanceCapitals.Add(balanceCapitals);
                        listInsertBalanceExtras.Add(balanceExtras);
                        listInsertBusinessResults.Add(businessResults);
                        listInsertIndirectCashFlows.Add(indirectCashFlows);
                        listInsertDirectCashFlows.Add(directCashFlows);
                        
                        
                    }
                    // Save data
                    if (!invalidField)
                    {
                        _context.BalanceAssets.AddRange(listInsertBalanceAssets);
                        _context.BalanceCapitals.AddRange(listInsertBalanceCapitals);
                        _context.BalanceExtras.AddRange(listInsertBalanceExtras);
                        _context.BusinessResults.AddRange(listInsertBusinessResults);
                        _context.IndirectCashFlows.AddRange(listInsertIndirectCashFlows);
                        _context.DirectCashFlows.AddRange(listInsertDirectCashFlows);
                        _context.SaveChanges();
                    }
                    
                }

                

            }
        }

        private static string ReplaceSpeicalFieldName(string fieldName, string code)
        {
            //BalanceAssets
            if (fieldName == "DauTuNamGiuDenNgayDaoHan" && code == "123") fieldName = nameof(BalanceAssets.DauTuNamGiuDenNgayDaoHanNganHan);
            else if (fieldName == "DauTuNamGiuDenNgayDaoHan" && code == "255") fieldName = nameof(BalanceAssets.DauTuNamGiuDenNgayDaoHanDaiHan);

            else if (fieldName == nameof(BalanceAssets.HangTonKho) && code == "140") fieldName = nameof(BalanceAssets.HangTonKhoTong);
            else if (fieldName == nameof(BalanceAssets.HangTonKho) && code == "141") fieldName = nameof(BalanceAssets.HangTonKho);

            else if (fieldName == nameof(BalanceAssets.TaiSanNganHanKhac) && code == "150") fieldName = nameof(BalanceAssets.TaiSanNganHanKhacTong);
            else if (fieldName == nameof(BalanceAssets.TaiSanNganHanKhac) && code == "155") fieldName = nameof(BalanceAssets.TaiSanNganHanKhac);

            else if (fieldName == nameof(BalanceAssets.TaiSanDaiHanKhac) && code == "260") fieldName = nameof(BalanceAssets.TaiSanDaiHanKhacTong);
            else if (fieldName == nameof(BalanceAssets.TaiSanDaiHanKhac) && code == "268") fieldName = nameof(BalanceAssets.TaiSanDaiHanKhac);

            else if (fieldName == "NguyenGia" && code == "222") fieldName = nameof(BalanceAssets.TaiSanCoDinhHuuHinhNguyenGia);
            else if (fieldName == "NguyenGia" && code == "225") fieldName = nameof(BalanceAssets.TaiSanCoDinhThueTaiChinhNguyenGia);
            else if (fieldName == "NguyenGia" && code == "228") fieldName = nameof(BalanceAssets.TaiSanCoDinhVoHinhNguyenGia);
            else if (fieldName == "NguyenGia" && code == "231") fieldName = nameof(BalanceAssets.BatDongSanDauTuNguyenGia);

            else if (fieldName == "GiaTriHaoMonLuyKe" && code == "223") fieldName = nameof(BalanceAssets.TaiSanCoDinhHuuHinhGiaTriHaoMonLuyKe);
            else if (fieldName == "GiaTriHaoMonLuyKe" && code == "226") fieldName = nameof(BalanceAssets.TaiSanCoDinhThueTaiChinhGiaTriHaoMonLuyKe);
            else if (fieldName == "GiaTriHaoMonLuyKe" && code == "229") fieldName = nameof(BalanceAssets.TaiSanCoDinhVoHinhGiaTriHaoMonLuyKe);
            else if (fieldName == "GiaTriHaoMonLuyKe" && code == "232") fieldName = nameof(BalanceAssets.BatDongSanDauTuGiaTriHaoMonLuyKe);


            //BalanceCapitals
            if (fieldName == nameof(BalanceCapitals.VonChuSoHuu) && code == "400") fieldName = nameof(BalanceCapitals.VonChuSoHuuTong);
            else if (fieldName == nameof(BalanceCapitals.VonChuSoHuu) && code == "410") fieldName = nameof(BalanceCapitals.VonChuSoHuu);

            //IndirectCashFlows
            if (fieldName.Contains(nameof(IndirectCashFlows.LaiLoChenhLechTyGiaHoiDoaiDoDanhGiaLai)))
            {
                fieldName = nameof(IndirectCashFlows.LaiLoChenhLechTyGiaHoiDoaiDoDanhGiaLai);
            }

            if (fieldName.Contains(nameof(IndirectCashFlows.TienChiTraVonGopChoCacChuSoHuu)))
            {
                fieldName = nameof(IndirectCashFlows.TienChiTraVonGopChoCacChuSoHuu);
            }



            return fieldName;
        }

        private bool IgnoreSpecialCase(string fieldName, string code)
        {
            return
                string.IsNullOrEmpty(fieldName) ||
                (fieldName ==nameof(BusinessResults.PhanLaiLoTrongCongTyLienDoanhLienKet) &&
                string.IsNullOrEmpty(code));
            
        }
        private void GetZone(string FieldNameTemp, ref string Zone)
        {
            switch (FieldNameTemp)
            {
                case ZoneTaiSan:
                    Zone = ZoneTaiSan;
                    break;
                case ZoneNguonVon:
                    Zone = ZoneNguonVon;
                    break;
                case ZoneChiTieuNgoaiBang:
                    Zone = ZoneChiTieuNgoaiBang;
                    break;
                case ZoneKetQuaKinhDoanh:
                    Zone = ZoneKetQuaKinhDoanh;
                    break;
                case ZoneLuuChuyenTienTeGianTiep:
                    Zone = ZoneLuuChuyenTienTeGianTiep;
                    break;
                case ZoneLuuChuyenTienTeTrucTiep:
                    Zone = ZoneLuuChuyenTienTeTrucTiep;
                    break;
            }
        }

        

        private void InitBaseObject(BaseEntityFinance obj, string companyId, int quarter, int year)
        {
            obj.Id = Guid.NewGuid();
            obj.CompanyId = new Guid(companyId);
            obj.Created = DateTime.Now;
            obj.QuarterType = quarter;
            obj.Year = year;
        }

        


        private void AssignValue(Object obj, string name , string value)
        {
            try
            {
                obj.GetType().GetProperty(name).SetValue(obj, StringHelpers.StringToDouble(value)); //StringHelpers.StringToDouble(value);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

       
        

        private static string ConvertFieldName(string fieldName)
        {
            int indexDot = fieldName.IndexOf(".");
            if (indexDot > 0)
            {
                fieldName = fieldName.Substring(indexDot + 1);
            }

            int indexColon = fieldName.IndexOf(":");
            if (indexColon >= 0)
            {
                fieldName = fieldName.Substring(indexColon + 1);
            }

            int indexBracket = fieldName.IndexOf("(");
            if (indexBracket > 0)
            {
                fieldName = fieldName.Substring(0, indexBracket);
            }

            int indexCurlyBracket = fieldName.IndexOf("{");
            if (indexCurlyBracket > 0)
            {
                fieldName = fieldName.Substring(0, indexCurlyBracket);
            }

            fieldName = fieldName.Replace("/", " ");

            fieldName = StringHelpers.ConvertToUnSign(fieldName).ToLower().Trim();
            fieldName = StringHelpers.ConvertToFirstUpper(fieldName);
            fieldName=fieldName.Replace("-", "");
            fieldName = fieldName.Replace(",", "");
            return fieldName;
        }

       
        private bool ValidateRow(string fieldName, string code, string Zone)
        {
            string compareValue = Zone + fieldName + code;

            var list = GetListDefine();
            if (list.Contains(compareValue))
            {
                return true;
            }
            
            return false;
        }


        private List<String> GetListDefine()
        {
            List<String> list = new List<string>();
            //ZoneTaiSan
            list.Add(CombineString(nameof(BalanceAssets.TaiSanNganHan), "100", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TienVaCacKhoanTuongDuongTien), "110", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.Tien), "111", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.CacKhoanTuongDuongTien), "112", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DauTuTaiChinhNganHan), "120", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.ChungKhoanKinhDoanh), "121", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DuPhongGiamGiaChungKhoanKinhDoanh), "122", ZoneTaiSan));            
            list.Add(CombineString(nameof(BalanceAssets.DauTuNamGiuDenNgayDaoHanNganHan), "123", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DauTuNamGiuDenNgayDaoHanDaiHan), "255", ZoneTaiSan));            
            list.Add(CombineString(nameof(BalanceAssets.CacKhoanPhaiThuNganHan), "130", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.PhaiThuNganHanCuaKhachHang), "131", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TraTruocChoNguoiBanNganHan), "132", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.PhaiThuNoiBoNganHan), "133", ZoneTaiSan));

            list.Add(CombineString(nameof(BalanceAssets.PhaiThuTheoTienDoKeHoachHopDongXayDung), "134", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.PhaiThuVeChoVayNganHan), "135", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.PhaiThuNganHanKhac), "136", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DuPhongPhaiThuNganHanKhoDoi), "137", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanThieuChoXuLy), "139", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.HangTonKhoTong), "140", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.HangTonKho), "141", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DuPhongGiamGiaHangTonKho), "149", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanNganHanKhacTong), "150", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.ChiPhiTraTruocNganHan), "151", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.ThueGtgtConDuocKhauTru), "152", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.ThueVaCacKhoanKhacPhaiThuCuaNhaNuoc), "153", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.GiaoDichMuaBanLaiTraiPhieuChinhPhu), "154", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanNganHanKhac), "155", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanDaiHan), "200", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.CacKhoanPhaiThuDaiHan), "210", ZoneTaiSan));


            list.Add(CombineString(nameof(BalanceAssets.PhaiThuDaiHanCuaKhachHang), "211", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TraTruocChoNguoiBanDaiHan), "212", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.VonKinhDoanhOCacDonViTrucThuoc), "213", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.PhaiThuNoiBoDaiHan), "214", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.PhaiThuVeChoVayDaiHan), "215", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.PhaiThuDaiHanKhac), "216", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DuPhongPhaiThuDaiHanKhoDoi), "219", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinh), "220", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhHuuHinh), "221", ZoneTaiSan));            
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhHuuHinhNguyenGia), "222", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhHuuHinhGiaTriHaoMonLuyKe), "223", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhThueTaiChinh), "224", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhThueTaiChinhNguyenGia), "225", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhThueTaiChinhGiaTriHaoMonLuyKe), "226", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhVoHinh), "227", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhVoHinhNguyenGia), "228", ZoneTaiSan));

            list.Add(CombineString(nameof(BalanceAssets.TaiSanCoDinhVoHinhGiaTriHaoMonLuyKe), "229", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.BatDongSanDauTu), "230", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.BatDongSanDauTuNguyenGia), "231", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.BatDongSanDauTuGiaTriHaoMonLuyKe), "232", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanDoDangDaiHan), "240", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.ChiPhiSanXuatKinhDoanhDoDangDaiHan), "241", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.ChiPhiXayDungCoBanDoDang), "242", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DauTuTaiChinhDaiHan), "250", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DauTuVaoCongTyCon), "251", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DauTuVaoCongTyLienKetLienDoanh), "252", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DauTuGopVonVaoDonViKhac), "253", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DuPhongDauTuTaiChinhDaiHan), "254", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.DauTuDaiHanKhac), "", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanDaiHanKhacTong), "260", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.ChiPhiTraTruocDaiHan), "261", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanThueThuNhapHoanLai), "262", ZoneTaiSan));

            list.Add(CombineString(nameof(BalanceAssets.ThietBiVatTuPhuTungThayTheDaiHan), "263", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TaiSanDaiHanKhac), "268", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.LoiTheThuongMai), "", ZoneTaiSan));
            list.Add(CombineString(nameof(BalanceAssets.TongCongTaiSan), "280", ZoneTaiSan));

            //ZoneNguonVon
            list.Add(CombineString(nameof(BalanceCapitals.NoPhaiTra), "300", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.NoNganHan), "310", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraNguoiBanNganHan), "311", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.NguoiMuaTraTienTruocNganHan), "312", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.ThueVaCacKhoanPhaiNopNhaNuoc), "313", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraNguoiLaoDong), "314", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.ChiPhiPhaiTraNganHan), "315", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraNoiBoNganHan), "316", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraTheoTienDoKeHoachHopDongXayDung), "317", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.DoanhThuChuaThucHienNganHan), "318", ZoneNguonVon));

            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraNganHanKhac), "319", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.VayVaNoThueTaiChinhNganHan), "320", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.DuPhongPhaiTraNganHan), "321", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.QuyKhenThuongPhucLoi), "322", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.QuyBinhOnGia), "323", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.GiaoDichMuaBanLaiTraiPhieuChinhPhu), "324", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.NoDaiHan), "330", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraNguoiBanDaiHan), "331", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.NguoiMuaTraTienTruocDaiHan), "332", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.ChiPhiPhaiTraDaiHan), "333", ZoneNguonVon));

            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraNoiBoVeVonKinhDoanh), "334", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraNoiBoDaiHan), "335", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.DoanhThuChuaThucHienDaiHan), "336", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.PhaiTraDaiHanKhac), "337", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.VayVaNoThueTaiChinhDaiHan), "338", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.TraiPhieuChuyenDoi), "339", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.CoPhieuUuDai), "340", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.ThueThuNhapHoanLaiPhaiTra), "341", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.DuPhongPhaiTraDaiHan), "342", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.QuyPhatTrienKhoaHocVaCongNghe), "343", ZoneNguonVon));

            list.Add(CombineString(nameof(BalanceCapitals.DuPhongTroCapMatViecLam), "", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.VonChuSoHuuTong), "400", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.VonChuSoHuu), "410", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.VonGopCuaChuSoHuu), "411", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.CoPhieuPhoThongCoQuyenBieuQuyet), "411a", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.CoPhieuUuDai), "411b", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.ThangDuVonCoPhan), "412", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.QuyenChonChuyenDoiTraiPhieu), "413", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.VonKhacCuaChuSoHuu), "414", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.CoPhieuQuy), "415", ZoneNguonVon));

            list.Add(CombineString(nameof(BalanceCapitals.ChenhLechDanhGiaLaiTaiSan), "416", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.ChenhLechTyGiaHoiDoai), "417", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.QuyDauTuPhatTrien), "418", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.QuyHoTroSapXepDoanhNghiep), "419", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.QuyKhacThuocVonChuSoHuu), "420", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.LoiNhuanSauThueChuaPhanPhoi), "421", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.LnstChuaPhanPhoiLuyKeDenCuoiKyTruoc), "421a", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.LnstChuaPhanPhoiKyNay), "421b", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.NguonVonDauTuXdcb), "422", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.LoiIchCoDongKhongKiemSoat), "429", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.QuyDuPhongTaiChinh), "", ZoneNguonVon));

            list.Add(CombineString(nameof(BalanceCapitals.NguonKinhPhiVaQuyKhac), "430", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.NguonKinhPhi), "431", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.NguonKinhPhiDaHinhThanhTscd), "432", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.LoiIchCuaCoDongThieuSo), "", ZoneNguonVon));
            list.Add(CombineString(nameof(BalanceCapitals.TongCongNguonVon), "440", ZoneNguonVon));


            //ZoneChiTieuNgoaiBang
            list.Add(CombineString(nameof(BalanceExtras.TaiSanThueNgoai), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.VatTuHangHoaNhanGiuHoNhanGiaCong), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.HangHoaNhanBanHoNhanKyGui), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.NoKhoDoiDaXuLy), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.NgoaiTeCacLoai), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.Usd), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.Euro), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.Gbp), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.Aud), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.Lak), "", ZoneChiTieuNgoaiBang));
            list.Add(CombineString(nameof(BalanceExtras.Khac), "", ZoneChiTieuNgoaiBang));            
            list.Add(CombineString(nameof(BalanceExtras.DuToanChiHoatDong), "", ZoneChiTieuNgoaiBang));

            //ZoneKetQuaKinhDoanh
            list.Add(CombineString(nameof(BusinessResults.DoanhThuBanHangVaCungCapDichVu), "01", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.CacKhoanGiamTruDoanhThu), "02", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.DoanhThuThuanVeBanHangVaCungCapDichVu), "10", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.GiaVonHangBan), "11", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.LoiNhuanGopVeBanHangVaCungCapDichVu), "20", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.DoanhThuHoatDongTaiChinh), "21", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.ChiPhiTaiChinh), "22", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.ChiPhiLaiVay), "23", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.PhanLaiLoTrongCongTyLienDoanhLienKet), "24", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.ChiPhiBanHang), "25", ZoneKetQuaKinhDoanh));

            list.Add(CombineString(nameof(BusinessResults.ChiPhiQuanLyDoanhNghiep), "26", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.LoiNhuanThuanTuHoatDongKinhDoanh), "30", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.ThuNhapKhac), "31", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.ChiPhiKhac), "32", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.LoiNhuanKhac), "40", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.LoiNhuanKhacPhanLaiLoTrongCongTyLienDoanhLienKet), "", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.TongLoiNhuanKeToanTruocThue), "50", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.ChiPhiThueTndnHienHanh), "51", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.ChiPhiThueTndnHoanLai), "52", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.LoiNhuanSauThueThuNhapDoanhNghiep), "60", ZoneKetQuaKinhDoanh));

            list.Add(CombineString(nameof(BusinessResults.LoiIchCuaCoDongThieuSo), "", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.LoiNhuanSauThueCuaCoDongCuaCongTyMe), "", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.LaiCoBanTrenCoPhieu), "", ZoneKetQuaKinhDoanh));
            list.Add(CombineString(nameof(BusinessResults.LaiSuyGiamTrenCoPhieu), "71", ZoneKetQuaKinhDoanh));


            //ZoneLuuChuyenTienTeGianTiep
            list.Add(CombineString(nameof(IndirectCashFlows.LuuChuyenTienTuHoatDongKinhDoanh), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LoiNhuanTruocThue), "01", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.DieuChinhChoCacKhoan), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.KhauHaoTscdVaBdsdt), "02", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.CacKhoanDuPhong), "03", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LaiLoChenhLechTyGiaHoiDoaiDoDanhGiaLai), "04", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LaiLoTuHoatDongDauTu), "05", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.ChiPhiLaiVay), "06", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LaiLoDoThanhLyTscd), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.ThuNhapLaiVayVaCoTuc), "", ZoneLuuChuyenTienTeGianTiep));

            list.Add(CombineString(nameof(IndirectCashFlows.PhanBoLoiTheThuongMai), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.DieuChinhChoCacKhoanKhac), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LoiNhuanTuHoatDongKinhDoanhTruocThayDoiVonLuuDong), "08", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TangGiamCacKhoanPhaiThu), "09", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TangGiamHangTonKho), "10", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TangGiamCacKhoanPhaiTra), "11", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TangGiamChiPhiTraTruoc), "12", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TangGiamChungKhoanKinhDoanh), "13", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienLaiVayDaTra), "13", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.ThueThuNhapDoanhNghiepDaNop), "14", ZoneLuuChuyenTienTeGianTiep));

            list.Add(CombineString(nameof(IndirectCashFlows.TienThuKhacTuHoatDongKinhDoanh), "15", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienChiKhacTuHoatDongKinhDoanh), "16", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LuuChuyenTienThuanTuHoatDongKinhDoanh), "20", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LuuChuyenTienTuHoatDongDauTu), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienChiDeMuaSamXayDungTscdVaCacTaiSanDaiHanKhac), "21", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienThuTuThanhLyNhuongBanTscdVaCacTaiSanDaiHanKhac), "22", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienChiChoVayMuaCacCongCuNoCuaDonViKhac), "23", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienThuHoiChoVayBanLaiCacCongCuNoCuaDonViKhac), "24", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienChiDauTuGopVonVaoDonViKhac), "25", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienThuHoiDauTuGopVonVaoDonViKhac), "26", ZoneLuuChuyenTienTeGianTiep));

            list.Add(CombineString(nameof(IndirectCashFlows.TienThuLaiChoVayCoTucVaLoiNhuanDuocChia), "27", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TangGiamTienGuiNganHangCoKyHan), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.MuaLaiKhoanGopVonCuaCoDongThieuSoTrongCongTyCon), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienThuKhacTuHoatDongDauTu), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienChiKhacChoHoatDongDauTu), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LuuChuyenTienThuanTuHoatDongDauTu), "30", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LuuChuyenTienTuHoatDongTaiChinh), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienThuTuPhatHanhCoPhieuNhanVonGopCuaChuSoHuu), "31", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienChiTraVonGopChoCacChuSoHuu), "32", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienThuTuDiVay), "33", ZoneLuuChuyenTienTeGianTiep));

            list.Add(CombineString(nameof(IndirectCashFlows.TienChiTraNoGocVay), "34", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienTraNoGocThueTaiChinh), "35", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.CoTucLoiNhuanDaTraChoChuSoHuu), "36", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienThuKhacTuHoatDongTaiChinh), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienChiKhacChoHoatDongTaiChinh), "", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LuuChuyenTienThuanTuHoatDongTaiChinh), "40", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.LuuChuyenTienThuanTrongKy), "50", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienVaTuongDuongTienDauKy), "60", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.AnhHuongCuaThayDoiTyGiaHoiDoaiQuyDoiNgoaiTe), "61", ZoneLuuChuyenTienTeGianTiep));
            list.Add(CombineString(nameof(IndirectCashFlows.TienVaTuongDuongTienCuoiKy), "70", ZoneLuuChuyenTienTeGianTiep));

            //ZoneLuuChuyenTienTeTrucTiep
            list.Add(CombineString(nameof(DirectCashFlows.LuuChuyenTienTuHoatDongKinhDoanh), "", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuTuBanHangCungCapDichVuVaDoanhThuKhac), "01", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiTraChoNguoiCungCapHangHoaVaDichVu), "02", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiTraChoNguoiLaoDong), "03", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiTraLaiVay), "04", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiNopThueThuNhapDoanhNghiep), "05", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuKhacTuHoatDongKinhDoanh), "06", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiKhacChoHoatDongKinhDoanh), "07", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.LuuChuyenTienThuanTuHoatDongKinhDoanh), "20", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.LuuChuyenTienTuHoatDongDauTu), "", ZoneLuuChuyenTienTeTrucTiep));

            list.Add(CombineString(nameof(DirectCashFlows.TienChiDeMuaSamXayDungTscdVaCacTaiSanDaiHanKhac), "21", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuTuThanhLyNhuongBanTscdVaCacTaiSanDaiHanKhac), "22", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiChoVayMuaCacCongCuNoCuaDonViKhac), "23", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuHoiChoVayBanLaiCacCongCuNoCuaDonViKhac), "24", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiDauTuGopVonVaoDonViKhac), "25", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuHoiDauTuGopVonVaoDonViKhac), "26", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuLaiChoVayCoTucVaLoiNhuanDuocChia), "27", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuKhacTuHoatDongDauTu), "", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiKhacChoHoatDongDauTu), "", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.LuuChuyenTienThuanTuHoatDongDauTu), "30", ZoneLuuChuyenTienTeTrucTiep));

            list.Add(CombineString(nameof(DirectCashFlows.LuuChuyenTienTuHoatDongTaiChinh), "", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuTuPhatHanhCoPhieuNhanVonGopCuaChuSoHuu), "31", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiTraVonGopChoCacChuSoHuu), "32", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuTuDiVay), "33", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiTraNoGocVay), "34", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienTraNoGocThueTaiChinh), "35", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.CoTucLoiNhuanDaTraChoChuSoHuu), "36", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienThuKhacTuHoatDongTaiChinh), "", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienChiKhacChoHoatDongTaiChinh), "", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.LuuChuyenTienThuanTuHoatDongTaiChinh), "40", ZoneLuuChuyenTienTeTrucTiep));

            list.Add(CombineString(nameof(DirectCashFlows.LuuChuyenTienThuanTrongKy), "50", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienVaTuongDuongTienDauKy), "60", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.AnhHuongCuaThayDoiTyGiaHoiDoaiQuyDoiNgoaiTe), "61", ZoneLuuChuyenTienTeTrucTiep));
            list.Add(CombineString(nameof(DirectCashFlows.TienVaTuongDuongTienCuoiKy), "70", ZoneLuuChuyenTienTeTrucTiep));
            
            return list;
        }

        private string CombineString(string name, string code, string zone)
        {
            return zone + name + code;
        }

        private bool ValidateBalanceAsset(string fieldName, string code)
        {
            bool result = false;

            

            return result;
        }

        private bool GetYearQuarter(string columnName, ref int Year, ref int Quarter)
        {
            try
            {
                if (columnName.Length == 6 || columnName.Length == 7)
                {
                    var arr = columnName.Split(new String[] { @"/" }, StringSplitOptions.RemoveEmptyEntries);
                    Year = Convert.ToInt32(arr[1].ToString());

                    string quarterTemp = arr[0].ToString();
                    if (quarterTemp.Contains("D"))
                    {
                        quarterTemp = StringHelpers.Reverse(quarterTemp);
                    }

                    Quarter = (int)Enum.Parse(typeof(EnumQuarter.Quarter), quarterTemp);
                    return true;
                }
            }
            catch
            {

            }
            
            return false;

        }

        private DataSet ReadExcelFile(string fileName)
        {
            try
            {                
                var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;

                if (System.IO.Path.GetExtension(fileName) == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;
                }


                using (var conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    var sheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "] ";

                        var adapter = new OleDbDataAdapter(cmd);
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                
            }
            return null;
        }

    }
}
