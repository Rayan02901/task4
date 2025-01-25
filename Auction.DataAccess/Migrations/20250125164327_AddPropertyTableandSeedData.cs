using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auction.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyTableandSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    NumberOfBathrooms = table.Column<int>(type: "int", nullable: false),
                    YearBuilt = table.Column<int>(type: "int", nullable: false),
                    PropertyCategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyCategories_PropertyCategoryId",
                        column: x => x.PropertyCategoryId,
                        principalTable: "PropertyCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Description", "ImageUrl", "Location", "NumberOfBathrooms", "NumberOfRooms", "PropertyCategoryId", "Size", "Title", "VideoUrl", "YearBuilt" },
                values: new object[,]
                {
                    { 1, "A beautiful modern house located in the heart of the city.", "", "Los Angeles, CA", 3, 4, 1, 2500.5, "Modern Family House", "", 2015 },
                    { 2, "A stunning villa overlooking the ocean with premium amenities.", "", "Miami, FL", 5, 6, 2, 4500.0, "Luxury Beachfront Villa", "", 2020 },
                    { 3, "A charming cottage in a peaceful countryside setting.", "", "Asheville, NC", 2, 3, 3, 1200.0, "Cozy Cottage", "", 1990 },
                    { 4, "A compact and stylish apartment in the city center.", "", "New York, NY", 1, 2, 1, 850.0, "Downtown Apartment", "", 2005 },
                    { 5, "A spacious home with a large yard in a quiet neighborhood.", "", "Austin, TX", 4, 5, 2, 3200.0, "Suburban Dream Home", "", 2010 },
                    { 6, "A rustic cabin with breathtaking mountain views.", "", "Denver, CO", 2, 3, 3, 1500.0, "Rustic Mountain Cabin", "", 1985 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyCategoryId",
                table: "Properties",
                column: "PropertyCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
