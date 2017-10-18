using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IBI.Data;

namespace IBI.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171016064724_FinanceReport")]
    partial class FinanceReport
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IBI.Data.Entities.BalanceAssets", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BatDongSanDauTu");

                    b.Property<double>("BatDongSanDauTuGiaTriHaoMonLuyKe");

                    b.Property<double>("BatDongSanDauTuNguyenGia");

                    b.Property<double>("CacKhoanPhaiThuDaiHan");

                    b.Property<double>("CacKhoanPhaiThuNganHan");

                    b.Property<double>("CacKhoanTuongDuongTien");

                    b.Property<double>("ChiPhiSanXuatKinhDoanhDoDangDaiHan");

                    b.Property<double>("ChiPhiTraTruocDaiHan");

                    b.Property<double>("ChiPhiTraTruocNganHan");

                    b.Property<double>("ChiPhiXayDungCoBanDoDang");

                    b.Property<double>("ChungKhoanKinhDoanh");

                    b.Property<Guid>("CompanyId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<double>("DauTuDaiHanKhac");

                    b.Property<double>("DauTuGopVonVaoDonViKhac");

                    b.Property<double>("DauTuNamGiuDenNgayDaoHanDaiHan");

                    b.Property<double>("DauTuNamGiuDenNgayDaoHanNganHan");

                    b.Property<double>("DauTuTaiChinhDaiHan");

                    b.Property<double>("DauTuTaiChinhNganHan");

                    b.Property<double>("DauTuVaoCongTyCon");

                    b.Property<double>("DauTuVaoCongTyLienKetLienDoanh");

                    b.Property<double>("DuPhongDauTuTaiChinhDaiHan");

                    b.Property<double>("DuPhongGiamGiaChungKhoanKinhDoanh");

                    b.Property<double>("DuPhongGiamGiaHangTonKho");

                    b.Property<double>("DuPhongPhaiThuDaiHanKhoDoi");

                    b.Property<double>("DuPhongPhaiThuNganHanKhoDoi");

                    b.Property<double>("GiaoDichMuaBanLaiTraiPhieuChinhPhu");

                    b.Property<double>("HangTonKho");

                    b.Property<double>("HangTonKhoTong");

                    b.Property<double>("LoiTheThuongMai");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("ModifiedBy");

                    b.Property<double>("PhaiThuDaiHanCuaKhachHang");

                    b.Property<double>("PhaiThuDaiHanKhac");

                    b.Property<double>("PhaiThuNganHanCuaKhachHang");

                    b.Property<double>("PhaiThuNganHanKhac");

                    b.Property<double>("PhaiThuNoiBoDaiHan");

                    b.Property<double>("PhaiThuNoiBoNganHan");

                    b.Property<double>("PhaiThuTheoTienDoKeHoachHopDongXayDung");

                    b.Property<double>("PhaiThuVeChoVayDaiHan");

                    b.Property<double>("PhaiThuVeChoVayNganHan");

                    b.Property<int>("QuarterType");

                    b.Property<double>("TaiSanCoDinh");

                    b.Property<double>("TaiSanCoDinhHuuHinh");

                    b.Property<double>("TaiSanCoDinhHuuHinhGiaTriHaoMonLuyKe");

                    b.Property<double>("TaiSanCoDinhHuuHinhNguyenGia");

                    b.Property<double>("TaiSanCoDinhThueTaiChinh");

                    b.Property<double>("TaiSanCoDinhThueTaiChinhGiaTriHaoMonLuyKe");

                    b.Property<double>("TaiSanCoDinhThueTaiChinhNguyenGia");

                    b.Property<double>("TaiSanCoDinhVoHinh");

                    b.Property<double>("TaiSanCoDinhVoHinhGiaTriHaoMonLuyKe");

                    b.Property<double>("TaiSanCoDinhVoHinhNguyenGia");

                    b.Property<double>("TaiSanDaiHan");

                    b.Property<double>("TaiSanDaiHanKhac");

                    b.Property<double>("TaiSanDaiHanKhacTong");

                    b.Property<double>("TaiSanDoDangDaiHan");

                    b.Property<double>("TaiSanNganHan");

                    b.Property<double>("TaiSanNganHanKhac");

                    b.Property<double>("TaiSanNganHanKhacTong");

                    b.Property<double>("TaiSanThieuChoXuLy");

                    b.Property<double>("TaiSanThueThuNhapHoanLai");

                    b.Property<double>("ThietBiVatTuPhuTungThayTheDaiHan");

                    b.Property<double>("ThueGtgtConDuocKhauTru");

                    b.Property<double>("ThueVaCacKhoanKhacPhaiThuCuaNhaNuoc");

                    b.Property<double>("Tien");

                    b.Property<double>("TienVaCacKhoanTuongDuongTien");

                    b.Property<double>("TongCongTaiSan");

                    b.Property<double>("TraTruocChoNguoiBanDaiHan");

                    b.Property<double>("TraTruocChoNguoiBanNganHan");

                    b.Property<double>("VonKinhDoanhCuaCacDonViTrucThuoc");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("BalanceAssets");
                });

            modelBuilder.Entity("IBI.Data.Entities.BalanceCapitals", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("ChenhLechDanhGiaLaiTaiSan");

                    b.Property<double>("ChenhLechTyGiaHoiDoai");

                    b.Property<double>("ChiPhiPhaiTraDaiHan");

                    b.Property<double>("ChiPhiPhaiTraNganHan");

                    b.Property<double>("CoPhieuPhoThongCoQuyenBieuQuyet");

                    b.Property<double>("CoPhieuQuy");

                    b.Property<double>("CoPhieuUuDai");

                    b.Property<double>("CoPhieuUuDaiNo");

                    b.Property<Guid>("CompanyId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<double>("DoanhThuChuaThucHienDaiHan");

                    b.Property<double>("DoanhThuChuaThucHienNganHan");

                    b.Property<double>("DuPhongPhaiTraDaiHan");

                    b.Property<double>("DuPhongPhaiTraNganHan");

                    b.Property<double>("DuPhongTroCapMatViecLam");

                    b.Property<double>("GiaoDichMuaBanLaiTraiPhieuChinhPhu");

                    b.Property<double>("LNSTChuaPhanPhoiKyNay");

                    b.Property<double>("LNSTChuaPhanPhoiLuyKeDenCuoiKyTruoc");

                    b.Property<double>("LoiIchCoDongKhongKiemSoat");

                    b.Property<double>("LoiIchCuaCoDongThieuSo");

                    b.Property<double>("LoiNhuanSauThueChuaPhanPhoi");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("ModifiedBy");

                    b.Property<double>("NguoiMuaTraTienTruocDaiHan");

                    b.Property<double>("NguoiMuaTraTienTruocNganHan");

                    b.Property<double>("NguonKinhPhi");

                    b.Property<double>("NguonKinhPhiDaHinhThanhTSCD");

                    b.Property<double>("NguonKinhPhiVaQuyKhac");

                    b.Property<double>("NguonVonDauTuXDCB");

                    b.Property<double>("NoDaiHan");

                    b.Property<double>("NoNganHan");

                    b.Property<double>("NoPhaiTra");

                    b.Property<double>("PhaiTraDaiHanKhac");

                    b.Property<double>("PhaiTraNganhanKhac");

                    b.Property<double>("PhaiTraNguoiBanDaiHan");

                    b.Property<double>("PhaiTraNguoiBanNganHan");

                    b.Property<double>("PhaiTraNguoiLaoDong");

                    b.Property<double>("PhaiTraNoiBoDaiHan");

                    b.Property<double>("PhaiTraNoiBoNganHan");

                    b.Property<double>("PhaiTraNoiBoVeVonKinhDoanh");

                    b.Property<double>("PhaiTraTheoTienDoKeHoachHopDongXayDung");

                    b.Property<int>("QuarterType");

                    b.Property<double>("QuyBinhOnGia");

                    b.Property<double>("QuyDauTuPhatTrien");

                    b.Property<double>("QuyDuPhongTaiChinh");

                    b.Property<double>("QuyHoTroSapXepDoanhNghiep");

                    b.Property<double>("QuyKhacThuocVonChuSoHuu");

                    b.Property<double>("QuyKhenThuongPhucLoi");

                    b.Property<double>("QuyPhatTrienKhoaHocVaCongNghe");

                    b.Property<double>("QuyenChonChuyenDoiTraiPhieu");

                    b.Property<double>("ThangDuVonCoPhan");

                    b.Property<double>("ThueThuNhapHoanLaiPhaiTra");

                    b.Property<double>("ThueVaCacKhoanPhaiNopNhaNuoc");

                    b.Property<double>("TongCongNguonVon");

                    b.Property<double>("TraiPhieuChuyenDoi");

                    b.Property<double>("VayVaNoThueTaiChinhDaiHan");

                    b.Property<double>("VayVaNoThueTaiChinhNganHan");

                    b.Property<double>("VonChuSoHuu");

                    b.Property<double>("VonChuSoHuuTong");

                    b.Property<double>("VonGopCuaChuSoHuu");

                    b.Property<double>("VonKhacCuaChuSoHuu");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("BalanceCapitals");
                });

            modelBuilder.Entity("IBI.Data.Entities.BalanceExtras", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CompanyId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<double>("DuToanChiTietHoatDong");

                    b.Property<double>("HangHoaNhanBanHoNhanKyGui");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("ModifiedBy");

                    b.Property<double>("NgoaiTeCacLoaiAUD");

                    b.Property<double>("NgoaiTeCacLoaiEURO");

                    b.Property<double>("NgoaiTeCacLoaiGBP");

                    b.Property<double>("NgoaiTeCacLoaiKhac");

                    b.Property<double>("NgoaiTeCacLoaiLAK");

                    b.Property<double>("NgoaiTeCacLoaiUSD");

                    b.Property<double>("NoKhoDoiDaXuLy");

                    b.Property<int>("QuarterType");

                    b.Property<double>("TaiSanThueNgoai");

                    b.Property<double>("VatTuHangHoaNhanGiuHoNhanGiaCong");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("BalanceExtras");
                });

            modelBuilder.Entity("IBI.Data.Entities.BusinessResults", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CacKhoanGiamTruDoanhThu");

                    b.Property<double>("ChiPhiBanHang");

                    b.Property<double>("ChiPhiKhac");

                    b.Property<double>("ChiPhiQuanLyDoanhNghiep");

                    b.Property<double>("ChiPhiTaiChinh");

                    b.Property<double>("ChiPhiTaiChinhLaiVay");

                    b.Property<double>("ChiPhiThueTNDNHienHanh");

                    b.Property<double>("ChiPhiThueTNDNHoanLai");

                    b.Property<Guid>("CompanyId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<double>("DoanhThuBanHangVaCungCapDichVu");

                    b.Property<double>("DoanhThuHoatDongTaiChinh");

                    b.Property<double>("DoanhThuThuanVeBanHangVaCungCapDichVu");

                    b.Property<double>("GiaVonHangBan");

                    b.Property<double>("LaiCoBanTrenCoPhieu");

                    b.Property<double>("LaiSuyGiamTrenCoPhieu");

                    b.Property<double>("LoiIchCuaCoDongThieuSo");

                    b.Property<double>("LoiNhuanGopVeBanHangVaCungCapDichVu");

                    b.Property<double>("LoiNhuanKhac");

                    b.Property<double>("LoiNhuanKhacPhanLaiLoTrongCongTyLienDoanhLienKet");

                    b.Property<double>("LoiNhuanSauThueCuaCoDongCuaCongTyMe");

                    b.Property<double>("LoiNhuanSauThueThuNhapDoanhNghiep");

                    b.Property<double>("LoiNhuanThuanTuHoatDongKinhDoanh");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("ModifiedBy");

                    b.Property<double>("PhanLaiLoTrongCongTyLienDoanhLienKet");

                    b.Property<int>("QuarterType");

                    b.Property<double>("ThuNhapKhac");

                    b.Property<double>("TongLoiNhuanKeToanTruocThue");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("BusinessResults");
                });

            modelBuilder.Entity("IBI.Data.Entities.DirectCashFlows", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AnhHuongCuaThayDoiTyGiaHoiDoaiQuyDoiNgoaiTe");

                    b.Property<double>("CoTucLoiNhuanDaTraChoChuSoHuu");

                    b.Property<Guid>("CompanyId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<double>("LuuChuyenTienThuanTrongKy");

                    b.Property<double>("LuuChuyenTienThuanTuHoatDongDauTu");

                    b.Property<double>("LuuChuyenTienThuanTuHoatDongKinhDoanh");

                    b.Property<double>("LuuChuyenTienThuanTuHoatDongTaiChinh");

                    b.Property<double>("LuuChuyenTienTuHoatDongDauTu");

                    b.Property<double>("LuuChuyenTienTuHoatDongKinhDoanh");

                    b.Property<double>("LuuChuyenTienTuHoatDongTaiChinh");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("ModifiedBy");

                    b.Property<int>("QuarterType");

                    b.Property<double>("TienChiChoVayMuaCacCongCuNoCuaDonViKhac");

                    b.Property<double>("TienChiDauTuGopVonVaoDonViKhac");

                    b.Property<double>("TienChiDeMuaSamXayDungTSCDVaCacTaiSanDaiHanKhac");

                    b.Property<double>("TienChiKhacChoHoatDongKinhDoanh");

                    b.Property<double>("TienChiKhacChoHoatDongTaiChinh");

                    b.Property<double>("TienChiKhacTuHoatDongDauTu");

                    b.Property<double>("TienChiNopThueThuNhapDoanhNghiep");

                    b.Property<double>("TienChiTraChoNguoiCungCapHangHoaVaDichVu");

                    b.Property<double>("TienChiTraChoNguoiLaoDong");

                    b.Property<double>("TienChiTraLaiVay");

                    b.Property<double>("TienChiTraNoGocVay");

                    b.Property<double>("TienChiTraVonGopChoCacChuSoHuu");

                    b.Property<double>("TienThuHoiChoVayBanLaiCacCongCuNoCuaDonViKhac");

                    b.Property<double>("TienThuHoiDauTuGopVonVaoDonViKhac");

                    b.Property<double>("TienThuKhacTuHoatDongDauTu");

                    b.Property<double>("TienThuKhacTuHoatDongKinhDoanh");

                    b.Property<double>("TienThuKhacTuHoatDongTaiChinh");

                    b.Property<double>("TienThuLaiChoVayCoTucVaLoiNhuanDuocChia");

                    b.Property<double>("TienThuTuBanHangCungCapDichVuVaDoanhThuKhac");

                    b.Property<double>("TienThuTuDiVay");

                    b.Property<double>("TienThuTuPhatHanhCoPhieuNhanVonGopCuaChuSoHuu");

                    b.Property<double>("TienThuTuThanhLyNhuongBanTSCDVaCacTaiSanDaiHanKhac");

                    b.Property<double>("TienTraNoGocThueTaiChinh");

                    b.Property<double>("TienVaTuongDuongTienCuoiKy");

                    b.Property<double>("TienVaTuongDuongTienDauKy");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("DirectCashFlows");
                });

            modelBuilder.Entity("IBI.Data.Entities.IndirectCashFlows", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AnhHuongCuaThayDoiTyGiaHoiDoaiQuyDoiNgoaiTe");

                    b.Property<double>("CacKhoanDuPhong");

                    b.Property<double>("ChiPhiLaiVay");

                    b.Property<double>("CoTucLoiNhuanDaTraChoChuSoHuu");

                    b.Property<Guid>("CompanyId");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<double>("DieuChinhChoCacKhoan");

                    b.Property<double>("DieuChinhChoCacKhoanKhac");

                    b.Property<double>("KhauHaoTSCDVaBDSDT");

                    b.Property<double>("LaiLoChenhLechTyGiaHoiDoai");

                    b.Property<double>("LaiLoDoThanhLyTSCD");

                    b.Property<double>("LaiLoTuHoatDongDauTu");

                    b.Property<double>("LoiNhuanTruocThue");

                    b.Property<double>("LoiNhuanTuHoatDongKinhDoanhTruocThayDoiVonLuuDong");

                    b.Property<double>("LuuChuyenTienTeTuHoatDongKinhDoanh");

                    b.Property<double>("LuuChuyenTienThuanTrongKy");

                    b.Property<double>("LuuChuyenTienThuanTuHoatDongDauTu");

                    b.Property<double>("LuuChuyenTienThuanTuHoatDongKinhDoanh");

                    b.Property<double>("LuuChuyenTienThuanTuHoatDongTaiChinh");

                    b.Property<double>("LuuChuyenTienTuHoatDongDauTu");

                    b.Property<double>("LuuChuyenTienTuHoatDongTaiChinh");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("ModifiedBy");

                    b.Property<double>("MuaLaiKhoanGopVonCuaCoDongThieuSoTrongCongTyCon");

                    b.Property<double>("PhanBoLoiTheThuongMai");

                    b.Property<int>("QuarterType");

                    b.Property<double>("TangGiamCacKhoanPhaiThu");

                    b.Property<double>("TangGiamCacKhoanPhaiTra");

                    b.Property<double>("TangGiamChiPhiTraTruoc");

                    b.Property<double>("TangGiamChungKhoanKinhDoanh");

                    b.Property<double>("TangGiamHangTonKho");

                    b.Property<double>("TangGiamTienGuiNganHangCoKyHan");

                    b.Property<double>("ThuNhapLaiVayVaCoTuc");

                    b.Property<double>("ThueThuNhapDoanhNghiepDaNop");

                    b.Property<double>("TienChiChoVayMuaCacCongCuNoCuaDonViKhac");

                    b.Property<double>("TienChiDauTuGopVonVaoDonViKhac");

                    b.Property<double>("TienChiDeMuaSamXayDungTSCDVaCacTaiSanDaiHanKhac");

                    b.Property<double>("TienChiKhacChoHoatDongDauTu");

                    b.Property<double>("TienChiKhacChoHoatDongTaiChinh");

                    b.Property<double>("TienChiKhacTuHoatDongKinhDoanh");

                    b.Property<double>("TienChiTraNoGocVay");

                    b.Property<double>("TienChiTraVonGopChoCacChuSoHuu");

                    b.Property<double>("TienLaiVayDaTra");

                    b.Property<double>("TienThuHoiChoVayBanLaiCacCongCuNoCuaDonViKhac");

                    b.Property<double>("TienThuHoiDauTuGopVonVaoDonViKhac");

                    b.Property<double>("TienThuKhacTuHoatDongDauTu");

                    b.Property<double>("TienThuKhacTuHoatDongKinhDoanh");

                    b.Property<double>("TienThuKhacTuHoatDongTaiChinh");

                    b.Property<double>("TienThuLaiChoVayCoTucVaLoiNhuanDuocChia");

                    b.Property<double>("TienThuTuDiVay");

                    b.Property<double>("TienThuTuPhatHanhCoPhieuNhanVonGopCuaChuSoHuu");

                    b.Property<double>("TienThuTuThanhLyNhuongBanTSCDVaCacTaiSanDaiHanKhac");

                    b.Property<double>("TienTraNoGocThueTaiChinh");

                    b.Property<double>("TienVaTuongDuongTienCuoiKy");

                    b.Property<double>("TienVaTuongDuongTienDauKy");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("IndirectCashFlows");
                });

            modelBuilder.Entity("IBI.Data.Entities.StockPrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AveragePrice");

                    b.Property<string>("BloombergCode");

                    b.Property<double>("Ceiling");

                    b.Property<double>("ChangePrice");

                    b.Property<double>("ChangePriceRatio");

                    b.Property<double>("ClosePrice");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<double>("Floor");

                    b.Property<double>("HighPrice");

                    b.Property<string>("IsinCode");

                    b.Property<double>("LowPrice");

                    b.Property<double>("MainValue");

                    b.Property<double>("MainVolume");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("ModifiedBy");

                    b.Property<double>("OpenPrice");

                    b.Property<double>("PriorClosePrice");

                    b.Property<string>("Ticker");

                    b.Property<DateTime>("TransactionDate");

                    b.HasKey("Id");

                    b.ToTable("StockPrices");
                });
        }
    }
}
