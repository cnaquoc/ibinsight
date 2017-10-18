using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IBI.Data.Migrations
{
    public partial class ModifyFinance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TienThuTuThanhLyNhuongBanTSCDVaCacTaiSanDaiHanKhac",
                table: "IndirectCashFlows",
                newName: "TienThuTuThanhLyNhuongBanTscdVaCacTaiSanDaiHanKhac");

            migrationBuilder.RenameColumn(
                name: "TienChiDeMuaSamXayDungTSCDVaCacTaiSanDaiHanKhac",
                table: "IndirectCashFlows",
                newName: "TienChiDeMuaSamXayDungTscdVaCacTaiSanDaiHanKhac");

            migrationBuilder.RenameColumn(
                name: "LaiLoDoThanhLyTSCD",
                table: "IndirectCashFlows",
                newName: "LaiLoDoThanhLyTscd");

            migrationBuilder.RenameColumn(
                name: "KhauHaoTSCDVaBDSDT",
                table: "IndirectCashFlows",
                newName: "KhauHaoTscdVaBdsdt");

            migrationBuilder.RenameColumn(
                name: "LuuChuyenTienTeTuHoatDongKinhDoanh",
                table: "IndirectCashFlows",
                newName: "LuuChuyenTienTuHoatDongKinhDoanh");

            migrationBuilder.RenameColumn(
                name: "LaiLoChenhLechTyGiaHoiDoai",
                table: "IndirectCashFlows",
                newName: "LaiLoChenhLechTyGiaHoiDoaiDoDanhGiaLai");

            migrationBuilder.RenameColumn(
                name: "TienThuTuThanhLyNhuongBanTSCDVaCacTaiSanDaiHanKhac",
                table: "DirectCashFlows",
                newName: "TienThuTuThanhLyNhuongBanTscdVaCacTaiSanDaiHanKhac");

            migrationBuilder.RenameColumn(
                name: "TienChiDeMuaSamXayDungTSCDVaCacTaiSanDaiHanKhac",
                table: "DirectCashFlows",
                newName: "TienChiDeMuaSamXayDungTscdVaCacTaiSanDaiHanKhac");

            migrationBuilder.RenameColumn(
                name: "TienChiKhacTuHoatDongDauTu",
                table: "DirectCashFlows",
                newName: "TienChiKhacChoHoatDongDauTu");

            migrationBuilder.RenameColumn(
                name: "ChiPhiThueTNDNHoanLai",
                table: "BusinessResults",
                newName: "ChiPhiThueTndnHoanLai");

            migrationBuilder.RenameColumn(
                name: "ChiPhiThueTNDNHienHanh",
                table: "BusinessResults",
                newName: "ChiPhiThueTndnHienHanh");

            migrationBuilder.RenameColumn(
                name: "ChiPhiTaiChinhLaiVay",
                table: "BusinessResults",
                newName: "ChiPhiLaiVay");

            migrationBuilder.RenameColumn(
                name: "NgoaiTeCacLoaiUSD",
                table: "BalanceExtras",
                newName: "Usd");

            migrationBuilder.RenameColumn(
                name: "NgoaiTeCacLoaiLAK",
                table: "BalanceExtras",
                newName: "NgoaiTeCacLoai");

            migrationBuilder.RenameColumn(
                name: "NgoaiTeCacLoaiKhac",
                table: "BalanceExtras",
                newName: "Lak");

            migrationBuilder.RenameColumn(
                name: "NgoaiTeCacLoaiGBP",
                table: "BalanceExtras",
                newName: "Khac");

            migrationBuilder.RenameColumn(
                name: "NgoaiTeCacLoaiEURO",
                table: "BalanceExtras",
                newName: "Gbp");

            migrationBuilder.RenameColumn(
                name: "NgoaiTeCacLoaiAUD",
                table: "BalanceExtras",
                newName: "Euro");

            migrationBuilder.RenameColumn(
                name: "DuToanChiTietHoatDong",
                table: "BalanceExtras",
                newName: "DuToanChiHoatDong");

            migrationBuilder.RenameColumn(
                name: "PhaiTraNganhanKhac",
                table: "BalanceCapitals",
                newName: "PhaiTraNganHanKhac");

            migrationBuilder.RenameColumn(
                name: "NguonVonDauTuXDCB",
                table: "BalanceCapitals",
                newName: "NguonVonDauTuXdcb");

            migrationBuilder.RenameColumn(
                name: "NguonKinhPhiDaHinhThanhTSCD",
                table: "BalanceCapitals",
                newName: "NguonKinhPhiDaHinhThanhTscd");

            migrationBuilder.RenameColumn(
                name: "LNSTChuaPhanPhoiLuyKeDenCuoiKyTruoc",
                table: "BalanceCapitals",
                newName: "LnstChuaPhanPhoiLuyKeDenCuoiKyTruoc");

            migrationBuilder.RenameColumn(
                name: "LNSTChuaPhanPhoiKyNay",
                table: "BalanceCapitals",
                newName: "LnstChuaPhanPhoiKyNay");

            migrationBuilder.RenameColumn(
                name: "VonKinhDoanhCuaCacDonViTrucThuoc",
                table: "BalanceAssets",
                newName: "VonKinhDoanhOCacDonViTrucThuoc");

            migrationBuilder.AddColumn<double>(
                name: "Aud",
                table: "BalanceExtras",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aud",
                table: "BalanceExtras");

            migrationBuilder.RenameColumn(
                name: "TienThuTuThanhLyNhuongBanTscdVaCacTaiSanDaiHanKhac",
                table: "IndirectCashFlows",
                newName: "TienThuTuThanhLyNhuongBanTSCDVaCacTaiSanDaiHanKhac");

            migrationBuilder.RenameColumn(
                name: "TienChiDeMuaSamXayDungTscdVaCacTaiSanDaiHanKhac",
                table: "IndirectCashFlows",
                newName: "TienChiDeMuaSamXayDungTSCDVaCacTaiSanDaiHanKhac");

            migrationBuilder.RenameColumn(
                name: "LaiLoDoThanhLyTscd",
                table: "IndirectCashFlows",
                newName: "LaiLoDoThanhLyTSCD");

            migrationBuilder.RenameColumn(
                name: "KhauHaoTscdVaBdsdt",
                table: "IndirectCashFlows",
                newName: "KhauHaoTSCDVaBDSDT");

            migrationBuilder.RenameColumn(
                name: "LuuChuyenTienTuHoatDongKinhDoanh",
                table: "IndirectCashFlows",
                newName: "LuuChuyenTienTeTuHoatDongKinhDoanh");

            migrationBuilder.RenameColumn(
                name: "LaiLoChenhLechTyGiaHoiDoaiDoDanhGiaLai",
                table: "IndirectCashFlows",
                newName: "LaiLoChenhLechTyGiaHoiDoai");

            migrationBuilder.RenameColumn(
                name: "TienThuTuThanhLyNhuongBanTscdVaCacTaiSanDaiHanKhac",
                table: "DirectCashFlows",
                newName: "TienThuTuThanhLyNhuongBanTSCDVaCacTaiSanDaiHanKhac");

            migrationBuilder.RenameColumn(
                name: "TienChiDeMuaSamXayDungTscdVaCacTaiSanDaiHanKhac",
                table: "DirectCashFlows",
                newName: "TienChiDeMuaSamXayDungTSCDVaCacTaiSanDaiHanKhac");

            migrationBuilder.RenameColumn(
                name: "TienChiKhacChoHoatDongDauTu",
                table: "DirectCashFlows",
                newName: "TienChiKhacTuHoatDongDauTu");

            migrationBuilder.RenameColumn(
                name: "ChiPhiThueTndnHoanLai",
                table: "BusinessResults",
                newName: "ChiPhiThueTNDNHoanLai");

            migrationBuilder.RenameColumn(
                name: "ChiPhiThueTndnHienHanh",
                table: "BusinessResults",
                newName: "ChiPhiThueTNDNHienHanh");

            migrationBuilder.RenameColumn(
                name: "ChiPhiLaiVay",
                table: "BusinessResults",
                newName: "ChiPhiTaiChinhLaiVay");

            migrationBuilder.RenameColumn(
                name: "Usd",
                table: "BalanceExtras",
                newName: "NgoaiTeCacLoaiUSD");

            migrationBuilder.RenameColumn(
                name: "NgoaiTeCacLoai",
                table: "BalanceExtras",
                newName: "NgoaiTeCacLoaiLAK");

            migrationBuilder.RenameColumn(
                name: "Lak",
                table: "BalanceExtras",
                newName: "NgoaiTeCacLoaiKhac");

            migrationBuilder.RenameColumn(
                name: "Khac",
                table: "BalanceExtras",
                newName: "NgoaiTeCacLoaiGBP");

            migrationBuilder.RenameColumn(
                name: "Gbp",
                table: "BalanceExtras",
                newName: "NgoaiTeCacLoaiEURO");

            migrationBuilder.RenameColumn(
                name: "Euro",
                table: "BalanceExtras",
                newName: "NgoaiTeCacLoaiAUD");

            migrationBuilder.RenameColumn(
                name: "DuToanChiHoatDong",
                table: "BalanceExtras",
                newName: "DuToanChiTietHoatDong");

            migrationBuilder.RenameColumn(
                name: "PhaiTraNganHanKhac",
                table: "BalanceCapitals",
                newName: "PhaiTraNganhanKhac");

            migrationBuilder.RenameColumn(
                name: "NguonVonDauTuXdcb",
                table: "BalanceCapitals",
                newName: "NguonVonDauTuXDCB");

            migrationBuilder.RenameColumn(
                name: "NguonKinhPhiDaHinhThanhTscd",
                table: "BalanceCapitals",
                newName: "NguonKinhPhiDaHinhThanhTSCD");

            migrationBuilder.RenameColumn(
                name: "LnstChuaPhanPhoiLuyKeDenCuoiKyTruoc",
                table: "BalanceCapitals",
                newName: "LNSTChuaPhanPhoiLuyKeDenCuoiKyTruoc");

            migrationBuilder.RenameColumn(
                name: "LnstChuaPhanPhoiKyNay",
                table: "BalanceCapitals",
                newName: "LNSTChuaPhanPhoiKyNay");

            migrationBuilder.RenameColumn(
                name: "VonKinhDoanhOCacDonViTrucThuoc",
                table: "BalanceAssets",
                newName: "VonKinhDoanhCuaCacDonViTrucThuoc");
        }
    }
}
