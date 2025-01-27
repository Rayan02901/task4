using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auction.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DBInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    NumberOfBathrooms = table.Column<int>(type: "int", nullable: false),
                    YearBuilt = table.Column<int>(type: "int", nullable: false),
                    PropertyCategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyCategoryId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyCategories_PropertyCategoryId",
                        column: x => x.PropertyCategoryId,
                        principalTable: "PropertyCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyCategories_PropertyCategoryId1",
                        column: x => x.PropertyCategoryId1,
                        principalTable: "PropertyCategories",
                        principalColumn: "Id");
                });

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
                    IsReservationPriceMet = table.Column<bool>(type: "bit", nullable: false),
                    IsBidStarted = table.Column<bool>(type: "bit", nullable: false),
                    MinimumBidIncrement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentHighestBid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HighestBidderId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuctionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    BidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BidTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_AuctionListings_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "AuctionListings",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27f77397-3ab6-4cba-8e57-31e0d16e5a98", null, "Seller", "SELLER" },
                    { "348e5667-5048-4292-b249-b4b55531606d", null, "Admin", "ADMIN" },
                    { "82426088-ca71-439c-b0c2-9c9194aa2cc0", null, "Buyer", "BUYER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "State", "StreetAddress", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ab6d1234-5678-9012-3456-bc12def34567", 0, null, "afddfb46-b4d0-421c-afd0-18ca63641f97", "ApplicationUser", "buyer3@example.com", true, false, null, "Buyer Three", "BUYER3@EXAMPLE.COM", "BUYER3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFDV0W9dxs/TZR6OubkVmM7BXnTLauIrA0DSaXtZaz0hEvjRQIm3Gi4z+RNM19KGUg==", null, false, null, "2076fe34-6a39-480b-9946-c92b46911929", null, null, false, "buyer3@example.com" },
                    { "b4db56e8-1234-4567-8901-bc12def34567", 0, null, "ab0c4f98-0992-45d1-9ca0-508ba97f6ff5", "ApplicationUser", "admin@example.com", true, false, null, "Admin User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAENWDZNev4PbH/CfJR0FrbUiM3DXHEgmJ0um/5u0xCFim70jxmFG6jglFAl/pVkcwsw==", null, false, null, "cb298898-76da-41a3-bdee-02da1cd3859b", null, null, false, "admin@example.com" },
                    { "cd2a7890-5678-1234-4567-bc12def34567", 0, null, "dea9d862-1b12-41f3-98a2-6d430e9751c9", "ApplicationUser", "seller2@example.com", true, false, null, "Seller Two", "SELLER2@EXAMPLE.COM", "SELLER2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOv/XGyqUR8IVY83zNzUeZWT+VW622F9WFX8XnZkAfgfobeoIOAFsZSWDqd6Hidgzw==", null, false, null, "bc6af4d5-8003-46d0-9fc4-6e1968feb42d", null, null, false, "seller2@example.com" },
                    { "de3a8901-3456-7890-1234-bc12def34567", 0, null, "b37ce505-ed51-4378-b6bb-7084500f762c", "ApplicationUser", "seller3@example.com", true, false, null, "Seller Three", "SELLER3@EXAMPLE.COM", "SELLER3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKfgBf1BE4FsKrdNJ0s6UFAfjRNOPljLMkk453mBY7dfE3O/4If81PVPp9TdXAbg9g==", null, false, null, "957e0e26-2bd6-404f-9d73-17a4876e00e5", null, null, false, "seller3@example.com" },
                    { "ed4b9012-2345-6789-1234-bc12def34567", 0, null, "229ae3d4-880d-41d2-ad6c-8ce45658bf61", "ApplicationUser", "buyer1@example.com", true, false, null, "Buyer One", "BUYER1@EXAMPLE.COM", "BUYER1@EXAMPLE.COM", "AQAAAAIAAYagAAAAECf2S5ey/BDRrkn3ZvgGHEYakZA4Ndjfif15JnvwAuWF80PPgXaOEKJF/diUGeJoSQ==", null, false, null, "fa29f5f7-86e1-4a73-95c8-39453173e7ff", null, null, false, "buyer1@example.com" },
                    { "fa0a5657-8901-2345-6789-bc12def34567", 0, null, "259568c7-58dc-4bc1-b4c5-ba761a1fd2be", "ApplicationUser", "seller1@example.com", true, false, null, "Seller One", "SELLER1@EXAMPLE.COM", "SELLER1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBmPpVL+d4oH7hyRZD/guU2BZ1k75TZIXxWgA3sjNOvJ1wBVlmfPpK+ZjUt2nWtrtw==", null, false, null, "c8c45829-2f39-4bdd-8364-9b62f79a7073", null, null, false, "seller1@example.com" },
                    { "fc5c0123-4567-8901-2345-bc12def34567", 0, null, "504d4ec3-6f2e-464d-b0a1-3339956229a9", "ApplicationUser", "buyer2@example.com", true, false, null, "Buyer Two", "BUYER2@EXAMPLE.COM", "BUYER2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEIJOWksR9myMuO5yyy9l5Xe0Scn9GuqBprNFG1xsQOXdoHitwsMv1RFsZis4GxvtIg==", null, false, null, "7af39da3-0773-4c7d-97c7-83c943cb0c6c", null, null, false, "buyer2@example.com" }
                });

            migrationBuilder.InsertData(
                table: "PropertyCategories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Residential" },
                    { 2, 2, "Commercial" },
                    { 3, 3, "Land" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "82426088-ca71-439c-b0c2-9c9194aa2cc0", "ab6d1234-5678-9012-3456-bc12def34567" },
                    { "348e5667-5048-4292-b249-b4b55531606d", "b4db56e8-1234-4567-8901-bc12def34567" },
                    { "27f77397-3ab6-4cba-8e57-31e0d16e5a98", "cd2a7890-5678-1234-4567-bc12def34567" },
                    { "27f77397-3ab6-4cba-8e57-31e0d16e5a98", "de3a8901-3456-7890-1234-bc12def34567" },
                    { "82426088-ca71-439c-b0c2-9c9194aa2cc0", "ed4b9012-2345-6789-1234-bc12def34567" },
                    { "27f77397-3ab6-4cba-8e57-31e0d16e5a98", "fa0a5657-8901-2345-6789-bc12def34567" },
                    { "82426088-ca71-439c-b0c2-9c9194aa2cc0", "fc5c0123-4567-8901-2345-bc12def34567" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Description", "ImageUrl", "Location", "NumberOfBathrooms", "NumberOfRooms", "PropertyCategoryId", "PropertyCategoryId1", "SellerId", "Size", "Title", "VideoUrl", "YearBuilt" },
                values: new object[,]
                {
                    { 1, "A beautiful modern house located in the heart of the city.", "", "Los Angeles, CA", 3, 4, 1, null, "fa0a5657-8901-2345-6789-bc12def34567", 2500.5, "Modern Family House", "", 2015 },
                    { 2, "A stunning villa overlooking the ocean with premium amenities.", "", "Miami, FL", 5, 6, 2, null, "fa0a5657-8901-2345-6789-bc12def34567", 4500.0, "Luxury Beachfront Villa", "", 2020 },
                    { 3, "A charming cottage in a peaceful countryside setting.", "", "Asheville, NC", 2, 3, 3, null, "cd2a7890-5678-1234-4567-bc12def34567", 1200.0, "Cozy Cottage", "", 1990 },
                    { 4, "Luxurious penthouse with panoramic city views.", "", "New York, NY", 4, 5, 1, null, "cd2a7890-5678-1234-4567-bc12def34567", 3000.0, "Downtown Penthouse", "", 2018 },
                    { 5, "Prime location office space in business district.", "", "Chicago, IL", 4, 8, 2, null, "de3a8901-3456-7890-1234-bc12def34567", 5000.0, "Commercial Office Space", "", 2019 },
                    { 6, "Beautiful waterfront land ready for development.", "", "Seattle, WA", 0, 0, 3, null, "de3a8901-3456-7890-1234-bc12def34567", 10000.0, "Waterfront Land Plot", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "AuctionListings",
                columns: new[] { "AuctionId", "CurrentHighestBid", "EndDate", "HighestBidderId", "IsBidStarted", "IsReservationPriceMet", "MinimumBidIncrement", "PropertyId", "ReservationPrice", "StartDate", "StartingBid", "Status" },
                values: new object[,]
                {
                    { 1, 500000m, new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, 5000m, 1, 600000m, new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 500000m, 0 },
                    { 2, 1000000m, new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, 10000m, 2, 1200000m, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000000m, 0 },
                    { 3, 300000m, new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, 3000m, 3, 350000m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 300000m, 0 },
                    { 4, 950000m, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ed4b9012-2345-6789-1234-bc12def34567", true, true, 5000m, 4, 900000m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 800000m, 1 },
                    { 5, 2500000m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fc5c0123-4567-8901-2345-bc12def34567", true, true, 20000m, 5, 2200000m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000000m, 1 },
                    { 6, 1800000m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ab6d1234-5678-9012-3456-bc12def34567", true, true, 10000m, 6, 1700000m, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500000m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionListings_PropertyId",
                table: "AuctionListings",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyCategoryId",
                table: "Properties",
                column: "PropertyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyCategoryId1",
                table: "Properties",
                column: "PropertyCategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_SellerId",
                table: "Properties",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AuctionListings");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PropertyCategories");
        }
    }
}
