using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IBI.Data.Migrations
{
    public partial class FinanceReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceAssets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BatDongSanDauTu = table.Column<double>(nullable: false),
                    BatDongSanDauTuGiaTriHaoMonLuyKe = table.Column<double>(nullable: false),
                    BatDongSanDauTuNguyenGia = table.Column<double>(nullable: false),
                    CacKhoanPhaiThuDaiHan = table.Column<double>(nullable: false),
                    CacKhoanPhaiThuNganHan = table.Column<double>(nullable: false),
                    CacKhoanTuongDuongTien = table.Column<double>(nullable: false),
                    ChiPhiSanXuatKinhDoanhDoDangDaiHan = table.Column<double>(nullable: false),
                    ChiPhiTraTruocDaiHan = table.Column<double>(nullable: false),
                    ChiPhiTraTruocNganHan = table.Column<double>(nullable: false),
                    ChiPhiXayDungCoBanDoDang = table.Column<double>(nullable: false),
                    ChungKhoanKinhDoanh = table.Column<double>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DauTuDaiHanKhac = table.Column<double>(nullable: false),
                    DauTuGopVonVaoDonViKhac = table.Column<double>(nullable: false),
                    DauTuNamGiuDenNgayDaoHanDaiHan = table.Column<double>(nullable: false),
                    DauTuNamGiuDenNgayDaoHanNganHan = table.Column<double>(nullable: false),
                    DauTuTaiChinhDaiHan = table.Column<double>(nullable: false),
                    DauTuTaiChinhNganHan = table.Column<double>(nullable: false),
                    DauTuVaoCongTyCon = table.Column<double>(nullable: false),
                    DauTuVaoCongTyLienKetLienDoanh = table.Column<double>(nullable: false),
                    DuPhongDauTuTaiChinhDaiHan = table.Column<double>(nullable: false),
                    DuPhongGiamGiaChungKhoanKinhDoanh = table.Column<double>(nullable: false),
                    DuPhongGiamGiaHangTonKho = table.Column<double>(nullable: false),
                    DuPhongPhaiThuDaiHanKhoDoi = table.Column<double>(nullable: false),
                    DuPhongPhaiThuNganHanKhoDoi = table.Column<double>(nullable: false),
                    GiaoDichMuaBanLaiTraiPhieuChinhPhu = table.Column<double>(nullable: false),
                    HangTonKho = table.Column<double>(nullable: false),
                    HangTonKhoTong = table.Column<double>(nullable: false),
                    LoiTheThuongMai = table.Column<double>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    PhaiThuDaiHanCuaKhachHang = table.Column<double>(nullable: false),
                    PhaiThuDaiHanKhac = table.Column<double>(nullable: false),
                    PhaiThuNganHanCuaKhachHang = table.Column<double>(nullable: false),
                    PhaiThuNganHanKhac = table.Column<double>(nullable: false),
                    PhaiThuNoiBoDaiHan = table.Column<double>(nullable: false),
                    PhaiThuNoiBoNganHan = table.Column<double>(nullable: false),
                    PhaiThuTheoTienDoKeHoachHopDongXayDung = table.Column<double>(nullable: false),
                    PhaiThuVeChoVayDaiHan = table.Column<double>(nullable: false),
                    PhaiThuVeChoVayNganHan = table.Column<double>(nullable: false),
                    QuarterType = table.Column<int>(nullable: false),
                    TaiSanCoDinh = table.Column<double>(nullable: false),
                    TaiSanCoDinhHuuHinh = table.Column<double>(nullable: false),
                    TaiSanCoDinhHuuHinhGiaTriHaoMonLuyKe = table.Column<double>(nullable: false),
                    TaiSanCoDinhHuuHinhNguyenGia = table.Column<double>(nullable: false),
                    TaiSanCoDinhThueTaiChinh = table.Column<double>(nullable: false),
                    TaiSanCoDinhThueTaiChinhGiaTriHaoMonLuyKe = table.Column<double>(nullable: false),
                    TaiSanCoDinhThueTaiChinhNguyenGia = table.Column<double>(nullable: false),
                    TaiSanCoDinhVoHinh = table.Column<double>(nullable: false),
                    TaiSanCoDinhVoHinhGiaTriHaoMonLuyKe = table.Column<double>(nullable: false),
                    TaiSanCoDinhVoHinhNguyenGia = table.Column<double>(nullable: false),
                    TaiSanDaiHan = table.Column<double>(nullable: false),
                    TaiSanDaiHanKhac = table.Column<double>(nullable: false),
                    TaiSanDaiHanKhacTong = table.Column<double>(nullable: false),
                    TaiSanDoDangDaiHan = table.Column<double>(nullable: false),
                    TaiSanNganHan = table.Column<double>(nullable: false),
                    TaiSanNganHanKhac = table.Column<double>(nullable: false),
                    TaiSanNganHanKhacTong = table.Column<double>(nullable: false),
                    TaiSanThieuChoXuLy = table.Column<double>(nullable: false),
                    TaiSanThueThuNhapHoanLai = table.Column<double>(nullable: false),
                    ThietBiVatTuPhuTungThayTheDaiHan = table.Column<double>(nullable: false),
                    ThueGtgtConDuocKhauTru = table.Column<double>(nullable: false),
                    ThueVaCacKhoanKhacPhaiThuCuaNhaNuoc = table.Column<double>(nullable: false),
                    Tien = table.Column<double>(nullable: false),
                    TienVaCacKhoanTuongDuongTien = table.Column<double>(nullable: false),
                    TongCongTaiSan = table.Column<double>(nullable: false),
                    TraTruocChoNguoiBanDaiHan = table.Column<double>(nullable: false),
                    TraTruocChoNguoiBanNganHan = table.Column<double>(nullable: false),
                    VonKinhDoanhCuaCacDonViTrucThuoc = table.Column<double>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BalanceCapitals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChenhLechDanhGiaLaiTaiSan = table.Column<double>(nullable: false),
                    ChenhLechTyGiaHoiDoai = table.Column<double>(nullable: false),
                    ChiPhiPhaiTraDaiHan = table.Column<double>(nullable: false),
                    ChiPhiPhaiTraNganHan = table.Column<double>(nullable: false),
                    CoPhieuPhoThongCoQuyenBieuQuyet = table.Column<double>(nullable: false),
                    CoPhieuQuy = table.Column<double>(nullable: false),
                    CoPhieuUuDai = table.Column<double>(nullable: false),
                    CoPhieuUuDaiNo = table.Column<double>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DoanhThuChuaThucHienDaiHan = table.Column<double>(nullable: false),
                    DoanhThuChuaThucHienNganHan = table.Column<double>(nullable: false),
                    DuPhongPhaiTraDaiHan = table.Column<double>(nullable: false),
                    DuPhongPhaiTraNganHan = table.Column<double>(nullable: false),
                    DuPhongTroCapMatViecLam = table.Column<double>(nullable: false),
                    GiaoDichMuaBanLaiTraiPhieuChinhPhu = table.Column<double>(nullable: false),
                    LNSTChuaPhanPhoiKyNay = table.Column<double>(nullable: false),
                    LNSTChuaPhanPhoiLuyKeDenCuoiKyTruoc = table.Column<double>(nullable: false),
                    LoiIchCoDongKhongKiemSoat = table.Column<double>(nullable: false),
                    LoiIchCuaCoDongThieuSo = table.Column<double>(nullable: false),
                    LoiNhuanSauThueChuaPhanPhoi = table.Column<double>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    NguoiMuaTraTienTruocDaiHan = table.Column<double>(nullable: false),
                    NguoiMuaTraTienTruocNganHan = table.Column<double>(nullable: false),
                    NguonKinhPhi = table.Column<double>(nullable: false),
                    NguonKinhPhiDaHinhThanhTSCD = table.Column<double>(nullable: false),
                    NguonKinhPhiVaQuyKhac = table.Column<double>(nullable: false),
                    NguonVonDauTuXDCB = table.Column<double>(nullable: false),
                    NoDaiHan = table.Column<double>(nullable: false),
                    NoNganHan = table.Column<double>(nullable: false),
                    NoPhaiTra = table.Column<double>(nullable: false),
                    PhaiTraDaiHanKhac = table.Column<double>(nullable: false),
                    PhaiTraNganhanKhac = table.Column<double>(nullable: false),
                    PhaiTraNguoiBanDaiHan = table.Column<double>(nullable: false),
                    PhaiTraNguoiBanNganHan = table.Column<double>(nullable: false),
                    PhaiTraNguoiLaoDong = table.Column<double>(nullable: false),
                    PhaiTraNoiBoDaiHan = table.Column<double>(nullable: false),
                    PhaiTraNoiBoNganHan = table.Column<double>(nullable: false),
                    PhaiTraNoiBoVeVonKinhDoanh = table.Column<double>(nullable: false),
                    PhaiTraTheoTienDoKeHoachHopDongXayDung = table.Column<double>(nullable: false),
                    QuarterType = table.Column<int>(nullable: false),
                    QuyBinhOnGia = table.Column<double>(nullable: false),
                    QuyDauTuPhatTrien = table.Column<double>(nullable: false),
                    QuyDuPhongTaiChinh = table.Column<double>(nullable: false),
                    QuyHoTroSapXepDoanhNghiep = table.Column<double>(nullable: false),
                    QuyKhacThuocVonChuSoHuu = table.Column<double>(nullable: false),
                    QuyKhenThuongPhucLoi = table.Column<double>(nullable: false),
                    QuyPhatTrienKhoaHocVaCongNghe = table.Column<double>(nullable: false),
                    QuyenChonChuyenDoiTraiPhieu = table.Column<double>(nullable: false),
                    ThangDuVonCoPhan = table.Column<double>(nullable: false),
                    ThueThuNhapHoanLaiPhaiTra = table.Column<double>(nullable: false),
                    ThueVaCacKhoanPhaiNopNhaNuoc = table.Column<double>(nullable: false),
                    TongCongNguonVon = table.Column<double>(nullable: false),
                    TraiPhieuChuyenDoi = table.Column<double>(nullable: false),
                    VayVaNoThueTaiChinhDaiHan = table.Column<double>(nullable: false),
                    VayVaNoThueTaiChinhNganHan = table.Column<double>(nullable: false),
                    VonChuSoHuu = table.Column<double>(nullable: false),
                    VonChuSoHuuTong = table.Column<double>(nullable: false),
                    VonGopCuaChuSoHuu = table.Column<double>(nullable: false),
                    VonKhacCuaChuSoHuu = table.Column<double>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceCapitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BalanceExtras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DuToanChiTietHoatDong = table.Column<double>(nullable: false),
                    HangHoaNhanBanHoNhanKyGui = table.Column<double>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    NgoaiTeCacLoaiAUD = table.Column<double>(nullable: false),
                    NgoaiTeCacLoaiEURO = table.Column<double>(nullable: false),
                    NgoaiTeCacLoaiGBP = table.Column<double>(nullable: false),
                    NgoaiTeCacLoaiKhac = table.Column<double>(nullable: false),
                    NgoaiTeCacLoaiLAK = table.Column<double>(nullable: false),
                    NgoaiTeCacLoaiUSD = table.Column<double>(nullable: false),
                    NoKhoDoiDaXuLy = table.Column<double>(nullable: false),
                    QuarterType = table.Column<int>(nullable: false),
                    TaiSanThueNgoai = table.Column<double>(nullable: false),
                    VatTuHangHoaNhanGiuHoNhanGiaCong = table.Column<double>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceExtras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CacKhoanGiamTruDoanhThu = table.Column<double>(nullable: false),
                    ChiPhiBanHang = table.Column<double>(nullable: false),
                    ChiPhiKhac = table.Column<double>(nullable: false),
                    ChiPhiQuanLyDoanhNghiep = table.Column<double>(nullable: false),
                    ChiPhiTaiChinh = table.Column<double>(nullable: false),
                    ChiPhiTaiChinhLaiVay = table.Column<double>(nullable: false),
                    ChiPhiThueTNDNHienHanh = table.Column<double>(nullable: false),
                    ChiPhiThueTNDNHoanLai = table.Column<double>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DoanhThuBanHangVaCungCapDichVu = table.Column<double>(nullable: false),
                    DoanhThuHoatDongTaiChinh = table.Column<double>(nullable: false),
                    DoanhThuThuanVeBanHangVaCungCapDichVu = table.Column<double>(nullable: false),
                    GiaVonHangBan = table.Column<double>(nullable: false),
                    LaiCoBanTrenCoPhieu = table.Column<double>(nullable: false),
                    LaiSuyGiamTrenCoPhieu = table.Column<double>(nullable: false),
                    LoiIchCuaCoDongThieuSo = table.Column<double>(nullable: false),
                    LoiNhuanGopVeBanHangVaCungCapDichVu = table.Column<double>(nullable: false),
                    LoiNhuanKhac = table.Column<double>(nullable: false),
                    LoiNhuanKhacPhanLaiLoTrongCongTyLienDoanhLienKet = table.Column<double>(nullable: false),
                    LoiNhuanSauThueCuaCoDongCuaCongTyMe = table.Column<double>(nullable: false),
                    LoiNhuanSauThueThuNhapDoanhNghiep = table.Column<double>(nullable: false),
                    LoiNhuanThuanTuHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    PhanLaiLoTrongCongTyLienDoanhLienKet = table.Column<double>(nullable: false),
                    QuarterType = table.Column<int>(nullable: false),
                    ThuNhapKhac = table.Column<double>(nullable: false),
                    TongLoiNhuanKeToanTruocThue = table.Column<double>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DirectCashFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnhHuongCuaThayDoiTyGiaHoiDoaiQuyDoiNgoaiTe = table.Column<double>(nullable: false),
                    CoTucLoiNhuanDaTraChoChuSoHuu = table.Column<double>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    LuuChuyenTienThuanTrongKy = table.Column<double>(nullable: false),
                    LuuChuyenTienThuanTuHoatDongDauTu = table.Column<double>(nullable: false),
                    LuuChuyenTienThuanTuHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    LuuChuyenTienThuanTuHoatDongTaiChinh = table.Column<double>(nullable: false),
                    LuuChuyenTienTuHoatDongDauTu = table.Column<double>(nullable: false),
                    LuuChuyenTienTuHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    LuuChuyenTienTuHoatDongTaiChinh = table.Column<double>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    QuarterType = table.Column<int>(nullable: false),
                    TienChiChoVayMuaCacCongCuNoCuaDonViKhac = table.Column<double>(nullable: false),
                    TienChiDauTuGopVonVaoDonViKhac = table.Column<double>(nullable: false),
                    TienChiDeMuaSamXayDungTSCDVaCacTaiSanDaiHanKhac = table.Column<double>(nullable: false),
                    TienChiKhacChoHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    TienChiKhacChoHoatDongTaiChinh = table.Column<double>(nullable: false),
                    TienChiKhacTuHoatDongDauTu = table.Column<double>(nullable: false),
                    TienChiNopThueThuNhapDoanhNghiep = table.Column<double>(nullable: false),
                    TienChiTraChoNguoiCungCapHangHoaVaDichVu = table.Column<double>(nullable: false),
                    TienChiTraChoNguoiLaoDong = table.Column<double>(nullable: false),
                    TienChiTraLaiVay = table.Column<double>(nullable: false),
                    TienChiTraNoGocVay = table.Column<double>(nullable: false),
                    TienChiTraVonGopChoCacChuSoHuu = table.Column<double>(nullable: false),
                    TienThuHoiChoVayBanLaiCacCongCuNoCuaDonViKhac = table.Column<double>(nullable: false),
                    TienThuHoiDauTuGopVonVaoDonViKhac = table.Column<double>(nullable: false),
                    TienThuKhacTuHoatDongDauTu = table.Column<double>(nullable: false),
                    TienThuKhacTuHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    TienThuKhacTuHoatDongTaiChinh = table.Column<double>(nullable: false),
                    TienThuLaiChoVayCoTucVaLoiNhuanDuocChia = table.Column<double>(nullable: false),
                    TienThuTuBanHangCungCapDichVuVaDoanhThuKhac = table.Column<double>(nullable: false),
                    TienThuTuDiVay = table.Column<double>(nullable: false),
                    TienThuTuPhatHanhCoPhieuNhanVonGopCuaChuSoHuu = table.Column<double>(nullable: false),
                    TienThuTuThanhLyNhuongBanTSCDVaCacTaiSanDaiHanKhac = table.Column<double>(nullable: false),
                    TienTraNoGocThueTaiChinh = table.Column<double>(nullable: false),
                    TienVaTuongDuongTienCuoiKy = table.Column<double>(nullable: false),
                    TienVaTuongDuongTienDauKy = table.Column<double>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCashFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndirectCashFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnhHuongCuaThayDoiTyGiaHoiDoaiQuyDoiNgoaiTe = table.Column<double>(nullable: false),
                    CacKhoanDuPhong = table.Column<double>(nullable: false),
                    ChiPhiLaiVay = table.Column<double>(nullable: false),
                    CoTucLoiNhuanDaTraChoChuSoHuu = table.Column<double>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DieuChinhChoCacKhoan = table.Column<double>(nullable: false),
                    DieuChinhChoCacKhoanKhac = table.Column<double>(nullable: false),
                    KhauHaoTSCDVaBDSDT = table.Column<double>(nullable: false),
                    LaiLoChenhLechTyGiaHoiDoai = table.Column<double>(nullable: false),
                    LaiLoDoThanhLyTSCD = table.Column<double>(nullable: false),
                    LaiLoTuHoatDongDauTu = table.Column<double>(nullable: false),
                    LoiNhuanTruocThue = table.Column<double>(nullable: false),
                    LoiNhuanTuHoatDongKinhDoanhTruocThayDoiVonLuuDong = table.Column<double>(nullable: false),
                    LuuChuyenTienTeTuHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    LuuChuyenTienThuanTrongKy = table.Column<double>(nullable: false),
                    LuuChuyenTienThuanTuHoatDongDauTu = table.Column<double>(nullable: false),
                    LuuChuyenTienThuanTuHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    LuuChuyenTienThuanTuHoatDongTaiChinh = table.Column<double>(nullable: false),
                    LuuChuyenTienTuHoatDongDauTu = table.Column<double>(nullable: false),
                    LuuChuyenTienTuHoatDongTaiChinh = table.Column<double>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MuaLaiKhoanGopVonCuaCoDongThieuSoTrongCongTyCon = table.Column<double>(nullable: false),
                    PhanBoLoiTheThuongMai = table.Column<double>(nullable: false),
                    QuarterType = table.Column<int>(nullable: false),
                    TangGiamCacKhoanPhaiThu = table.Column<double>(nullable: false),
                    TangGiamCacKhoanPhaiTra = table.Column<double>(nullable: false),
                    TangGiamChiPhiTraTruoc = table.Column<double>(nullable: false),
                    TangGiamChungKhoanKinhDoanh = table.Column<double>(nullable: false),
                    TangGiamHangTonKho = table.Column<double>(nullable: false),
                    TangGiamTienGuiNganHangCoKyHan = table.Column<double>(nullable: false),
                    ThuNhapLaiVayVaCoTuc = table.Column<double>(nullable: false),
                    ThueThuNhapDoanhNghiepDaNop = table.Column<double>(nullable: false),
                    TienChiChoVayMuaCacCongCuNoCuaDonViKhac = table.Column<double>(nullable: false),
                    TienChiDauTuGopVonVaoDonViKhac = table.Column<double>(nullable: false),
                    TienChiDeMuaSamXayDungTSCDVaCacTaiSanDaiHanKhac = table.Column<double>(nullable: false),
                    TienChiKhacChoHoatDongDauTu = table.Column<double>(nullable: false),
                    TienChiKhacChoHoatDongTaiChinh = table.Column<double>(nullable: false),
                    TienChiKhacTuHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    TienChiTraNoGocVay = table.Column<double>(nullable: false),
                    TienChiTraVonGopChoCacChuSoHuu = table.Column<double>(nullable: false),
                    TienLaiVayDaTra = table.Column<double>(nullable: false),
                    TienThuHoiChoVayBanLaiCacCongCuNoCuaDonViKhac = table.Column<double>(nullable: false),
                    TienThuHoiDauTuGopVonVaoDonViKhac = table.Column<double>(nullable: false),
                    TienThuKhacTuHoatDongDauTu = table.Column<double>(nullable: false),
                    TienThuKhacTuHoatDongKinhDoanh = table.Column<double>(nullable: false),
                    TienThuKhacTuHoatDongTaiChinh = table.Column<double>(nullable: false),
                    TienThuLaiChoVayCoTucVaLoiNhuanDuocChia = table.Column<double>(nullable: false),
                    TienThuTuDiVay = table.Column<double>(nullable: false),
                    TienThuTuPhatHanhCoPhieuNhanVonGopCuaChuSoHuu = table.Column<double>(nullable: false),
                    TienThuTuThanhLyNhuongBanTSCDVaCacTaiSanDaiHanKhac = table.Column<double>(nullable: false),
                    TienTraNoGocThueTaiChinh = table.Column<double>(nullable: false),
                    TienVaTuongDuongTienCuoiKy = table.Column<double>(nullable: false),
                    TienVaTuongDuongTienDauKy = table.Column<double>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndirectCashFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AveragePrice = table.Column<double>(nullable: false),
                    BloombergCode = table.Column<string>(nullable: true),
                    Ceiling = table.Column<double>(nullable: false),
                    ChangePrice = table.Column<double>(nullable: false),
                    ChangePriceRatio = table.Column<double>(nullable: false),
                    ClosePrice = table.Column<double>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Floor = table.Column<double>(nullable: false),
                    HighPrice = table.Column<double>(nullable: false),
                    IsinCode = table.Column<string>(nullable: true),
                    LowPrice = table.Column<double>(nullable: false),
                    MainValue = table.Column<double>(nullable: false),
                    MainVolume = table.Column<double>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    OpenPrice = table.Column<double>(nullable: false),
                    PriorClosePrice = table.Column<double>(nullable: false),
                    Ticker = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPrices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceAssets");

            migrationBuilder.DropTable(
                name: "BalanceCapitals");

            migrationBuilder.DropTable(
                name: "BalanceExtras");

            migrationBuilder.DropTable(
                name: "BusinessResults");

            migrationBuilder.DropTable(
                name: "DirectCashFlows");

            migrationBuilder.DropTable(
                name: "IndirectCashFlows");

            migrationBuilder.DropTable(
                name: "StockPrices");
        }
    }
}
