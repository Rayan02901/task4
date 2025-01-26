using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAuctionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionListings",
                columns: table => new
                {
                    AuctionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumBidIncrement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentHighestBid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HighestBidderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartingBid = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionListings", x => x.AuctionId);
                    table.ForeignKey(
                        name: "FK_AuctionListings_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionListings_PropertyId",
                table: "AuctionListings",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionListings");
        }
    }
}
