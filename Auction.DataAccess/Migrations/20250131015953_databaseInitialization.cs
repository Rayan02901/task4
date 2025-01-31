using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auction.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class databaseInitialization : Migration
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
                        name: "FK_Bids_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { "4aa1853c-874f-48fb-b656-73855d9e074f", null, "Seller", "SELLER" },
                    { "c5ddd595-f622-456a-994a-36ebe2e1c383", null, "Admin", "ADMIN" },
                    { "e72c91f6-490e-41ec-b770-86ca471e698a", null, "Buyer", "BUYER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "State", "StreetAddress", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, null, "33e25cbd-3cd2-4246-b1d8-eab359db5d9a", "ApplicationUser", "admin@example.com", true, false, null, "Admin User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAELU+wsetPqml4o5EdyM4wY7occWYAm93T8CI6J9Pt4TJ3asulyX2T8zKZk5qy2r6vA==", null, false, null, "b167bdfd-4e6a-4766-a141-60cd780c715e", null, null, false, "admin@example.com" },
                    { "b68d3f5a-b414-48d9-8e34-7c6dde3941ef", 0, null, "624c5601-9510-4581-a205-d62b67b75150", "ApplicationUser", "seller1@example.com", true, false, null, "Seller One", "SELLER1@EXAMPLE.COM", "SELLER1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHot2gcER/3QYjX6N9i2AVgr1bdCkiHdy5PCYXWmg8qezL25H38NyCE80iAk94m/hg==", null, false, null, "598ebed0-05f5-4ea2-a143-11f777aee769", null, null, false, "seller1@example.com" },
                    { "c1ce93ad-1e91-4b79-9f43-9f1d51d33511", 0, null, "7427f7db-6b90-4996-9f00-aa56c4ca8e3f", "ApplicationUser", "seller2@example.com", true, false, null, "Seller Two", "SELLER2@EXAMPLE.COM", "SELLER2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEA9jeDjZRkSjWRBUEe12M//+/YLwIrnax7tAFIvPeh7t9MMrghikcHfT14pCTN7f6g==", null, false, null, "7d609eae-0162-46ec-861b-77a624bfb21f", null, null, false, "seller2@example.com" },
                    { "d9a9b8d7-73b2-4f28-9177-e1f9239e6673", 0, null, "1926086f-0413-49c8-a1e9-672e61d4e78e", "ApplicationUser", "seller3@example.com", true, false, null, "Seller Three", "SELLER3@EXAMPLE.COM", "SELLER3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBr6TrNzqr453Nq5AV8+cEyL9G5F/cYG2HqDoBYgrqiFo/psS1+fY8IJnpKUfxHCdg==", null, false, null, "f270a20f-e0a2-4f81-894b-cfaebccb8f02", null, null, false, "seller3@example.com" },
                    { "e4c2f668-5c51-4fd1-9b55-b5dd9b33abb7", 0, null, "e6753600-e3a4-4526-b5cb-2f6c670ae37a", "ApplicationUser", "buyer1@example.com", true, false, null, "Buyer One", "BUYER1@EXAMPLE.COM", "BUYER1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEIVqDXiGnmfn1c3UStMOP4LiwZVGJ+MOrz9mx/zzyRq+fyMZ4T3HlmqV+Aj5ucx1WQ==", null, false, null, "7660a85c-f509-4402-b74d-7f91b00cd471", null, null, false, "buyer1@example.com" },
                    { "f682334f-82e1-4b89-8a56-89e4b7efde93", 0, null, "22efe663-ca8a-45c6-a115-9d062ed20f33", "ApplicationUser", "buyer2@example.com", true, false, null, "Buyer Two", "BUYER2@EXAMPLE.COM", "BUYER2@EXAMPLE.COM", "AQAAAAIAAYagAAAAENptiyNOejpJblqglsrjlm1ZhfYYw4hNTAFmKgtky3tiNRBgyRQOAPNoQTQcInVFXw==", null, false, null, "9d71a321-4745-4bde-884b-38c6c8fdacbc", null, null, false, "buyer2@example.com" },
                    { "g7d1c8e2-94a3-4d6b-ac32-b61e1e7b76d9", 0, null, "30d4a76b-ed06-4f73-be2f-924234afa07b", "ApplicationUser", "buyer3@example.com", true, false, null, "Buyer Three", "BUYER3@EXAMPLE.COM", "BUYER3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEB2kL6IhK/Q2iCVyWSlnMCESp+SjhAr+rNX4T2VoWf3HL1eeP4tpHMW67CdxhUuzkw==", null, false, null, "af2f928c-6961-4b3a-aaef-5d7249491718", null, null, false, "buyer3@example.com" }
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
                    { "c5ddd595-f622-456a-994a-36ebe2e1c383", "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                    { "4aa1853c-874f-48fb-b656-73855d9e074f", "b68d3f5a-b414-48d9-8e34-7c6dde3941ef" },
                    { "4aa1853c-874f-48fb-b656-73855d9e074f", "c1ce93ad-1e91-4b79-9f43-9f1d51d33511" },
                    { "4aa1853c-874f-48fb-b656-73855d9e074f", "d9a9b8d7-73b2-4f28-9177-e1f9239e6673" },
                    { "e72c91f6-490e-41ec-b770-86ca471e698a", "e4c2f668-5c51-4fd1-9b55-b5dd9b33abb7" },
                    { "e72c91f6-490e-41ec-b770-86ca471e698a", "f682334f-82e1-4b89-8a56-89e4b7efde93" },
                    { "e72c91f6-490e-41ec-b770-86ca471e698a", "g7d1c8e2-94a3-4d6b-ac32-b61e1e7b76d9" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Description", "ImageUrl", "Location", "NumberOfBathrooms", "NumberOfRooms", "PropertyCategoryId", "PropertyCategoryId1", "SellerId", "Size", "Title", "VideoUrl", "YearBuilt" },
                values: new object[,]
                {
                    { 1, "Elegant contemporary mansion with smart home features and panoramic valley views.", "", "Beverly Hills, CA", 5, 6, 1, null, "b68d3f5a-b414-48d9-8e34-7c6dde3941ef", 5200.0, "Sunset Valley Estate", "", 2022 },
                    { 2, "Luxury beachfront resort property with private beach access and full-service amenities.", "", "Maui, HI", 14, 12, 2, null, "b68d3f5a-b414-48d9-8e34-7c6dde3941ef", 8500.0, "Ocean Breeze Resort Complex", "", 2021 },
                    { 3, "Expansive ranch property with equestrian facilities and mountain backdrop.", "", "Aspen, CO", 3, 4, 3, null, "c1ce93ad-1e91-4b79-9f43-9f1d51d33511", 15000.0, "Mountain View Ranch", "", 2015 },
                    { 4, "Ultra-luxury penthouse with 360-degree views and private helipad access.", "", "Manhattan, NY", 5, 5, 1, null, "c1ce93ad-1e91-4b79-9f43-9f1d51d33511", 4800.0, "Skyline Tower Penthouse", "", 2023 },
                    { 5, "State-of-the-art office complex with sustainable design and tech infrastructure.", "", "Austin, TX", 8, 20, 2, null, "d9a9b8d7-73b2-4f28-9177-e1f9239e6673", 12000.0, "Innovation Hub Complex", "", 2024 },
                    { 6, "Prime wine country acreage with approved development plans and existing vineyards.", "", "Napa Valley, CA", 0, 0, 3, null, "d9a9b8d7-73b2-4f28-9177-e1f9239e6673", 25000.0, "Vineyard Estate Development", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "AuctionListings",
                columns: new[] { "AuctionId", "CurrentHighestBid", "EndDate", "HighestBidderId", "IsBidStarted", "IsReservationPriceMet", "MinimumBidIncrement", "PropertyId", "ReservationPrice", "StartDate", "StartingBid", "Status" },
                values: new object[,]
                {
                    { 1, 8500000m, new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, 50000m, 1, 10000000m, new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 8500000m, 0 },
                    { 2, 15000000m, new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, 100000m, 2, 18000000m, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000000m, 0 },
                    { 3, 12000000m, new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, 75000m, 3, 14000000m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000000m, 0 },
                    { 4, 28500000m, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "e4c2f668-5c51-4fd1-9b55-b5dd9b33abb7", true, true, 150000m, 4, 27000000m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 25000000m, 1 },
                    { 5, 52000000m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f682334f-82e1-4b89-8a56-89e4b7efde93", true, true, 250000m, 5, 48000000m, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45000000m, 1 },
                    { 6, 23500000m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "g7d1c8e2-94a3-4d6b-ac32-b61e1e7b76d9", true, true, 100000m, 6, 22000000m, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 20000000m, 1 }
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
                name: "IX_Bids_UserId",
                table: "Bids",
                column: "UserId");

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
