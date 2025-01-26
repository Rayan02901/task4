using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DBINIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 2, 18, 6, 55, 949, DateTimeKind.Local).AddTicks(7428), new DateTime(2025, 1, 27, 18, 6, 55, 949, DateTimeKind.Local).AddTicks(7409) });

            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 5, 18, 6, 55, 949, DateTimeKind.Local).AddTicks(7431), new DateTime(2025, 1, 28, 18, 6, 55, 949, DateTimeKind.Local).AddTicks(7431) });

            migrationBuilder.UpdateData(
                table: "AuctionListings",
                keyColumn: "AuctionId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 2, 10, 18, 6, 55, 949, DateTimeKind.Local).AddTicks(7434), new DateTime(2025, 1, 31, 18, 6, 55, 949, DateTimeKind.Local).AddTicks(7434) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
