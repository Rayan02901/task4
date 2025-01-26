using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DBInit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 2, 16, 56, 52, 742, DateTimeKind.Local).AddTicks(3637), new DateTime(2025, 1, 27, 16, 56, 52, 742, DateTimeKind.Local).AddTicks(3618) });

            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 5, 16, 56, 52, 742, DateTimeKind.Local).AddTicks(3643), new DateTime(2025, 1, 28, 16, 56, 52, 742, DateTimeKind.Local).AddTicks(3641) });

            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 10, 16, 56, 52, 742, DateTimeKind.Local).AddTicks(3648), new DateTime(2025, 1, 31, 16, 56, 52, 742, DateTimeKind.Local).AddTicks(3647) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 2, 15, 45, 26, 509, DateTimeKind.Local).AddTicks(4622), new DateTime(2025, 1, 27, 15, 45, 26, 509, DateTimeKind.Local).AddTicks(4606) });

            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 5, 15, 45, 26, 509, DateTimeKind.Local).AddTicks(4625), new DateTime(2025, 1, 28, 15, 45, 26, 509, DateTimeKind.Local).AddTicks(4625) });

            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 10, 15, 45, 26, 509, DateTimeKind.Local).AddTicks(4628), new DateTime(2025, 1, 31, 15, 45, 26, 509, DateTimeKind.Local).AddTicks(4627) });
        }
    }
}
