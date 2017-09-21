using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IBI.Data;

namespace IBI.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170921043435_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
