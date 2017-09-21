using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IBI.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "StockPrices");
        }
    }
}
