using Microsoft.EntityFrameworkCore;
using Auction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Auction.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PropertyCategory> PropertyCategories { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<AuctionListing> AuctionListings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(p => p.PropertyId);

                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasMaxLength(500);

                entity.Property(p => p.Location)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.Size)
                    .IsRequired();

                entity.Property(p => p.ImageUrl)
                    .HasMaxLength(500);

                entity.Property(p => p.VideoUrl)
                    .HasMaxLength(500);

                // Configure relationship with PropertyCategory
                entity.HasOne(p => p.PropertyCategory)
                    .WithMany()
                    .HasForeignKey(p => p.PropertyCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure relationship with Seller (ApplicationUser)
                entity.HasOne(p => p.Seller)
                    .WithMany()
                    .HasForeignKey(p => p.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure PropertyCategory table
            modelBuilder.Entity<PropertyCategory>(entity =>
            {
                entity.HasKey(pc => pc.Id);

                entity.Property(pc => pc.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(pc => pc.DisplayOrder)
                    .IsRequired();
            });
         
           

            modelBuilder.Entity<Property>()
                .HasOne(p => p.PropertyCategory)
                .WithMany()
                .HasForeignKey(p => p.PropertyCategoryId);
    

        // Configure AuctionListing table
        modelBuilder.Entity<AuctionListing>(entity =>
            {
                entity.HasKey(a => a.AuctionId);

                entity.Property(a => a.StartDate)
                    .IsRequired();

                entity.Property(a => a.EndDate)
                    .IsRequired();

                entity.Property(a => a.ReservationPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(a => a.MinimumBidIncrement)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(a => a.CurrentHighestBid)
                    .HasColumnType("decimal(18,2)");

                entity.Property(a => a.StartingBid)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(a => a.HighestBidderId)
                    .HasMaxLength(450);  // Match with ASP.NET Identity's default key length

                // Configure relationship with Property
                entity.HasOne(a => a.Property)
                    .WithMany()
                    .HasForeignKey(a => a.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure relationship with Bids
                entity.HasMany(a => a.Bids)
                    .WithOne(b => b.Auction)
                    .HasForeignKey(b => b.AuctionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Bid table
            modelBuilder.Entity<Bid>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.BidAmount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(b => b.BidTime)
                    .IsRequired();

                entity.Property(b => b.UserId)
                    .HasMaxLength(450)  // Match with ASP.NET Identity's default key length
                    .IsRequired();

                // Configure relationship with AuctionListing
                entity.HasOne(b => b.Auction)
                    .WithMany(a => a.Bids)
                    .HasForeignKey(b => b.AuctionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure ApplicationUser (if you have any specific configurations)
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.Name)
                    .HasMaxLength(100);
            });

            // Define role IDs
            string adminRoleId = Guid.NewGuid().ToString();
            string sellerRoleId = Guid.NewGuid().ToString();
            string buyerRoleId = Guid.NewGuid().ToString();

            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = sellerRoleId,
                    Name = "Seller",
                    NormalizedName = "SELLER"
                },
                new IdentityRole
                {
                    Id = buyerRoleId,
                    Name = "Buyer",
                    NormalizedName = "BUYER"
                }
            );

            // Define specific UserIds
            string adminUserId = "b4db56e8-1234-4567-8901-bc12def34567";
            string sellerUserId1 = "fa0a5657-8901-2345-6789-bc12def34567";
            string sellerUserId2 = "cd2a7890-5678-1234-4567-bc12def34567";
            string sellerUserId3 = "de3a8901-3456-7890-1234-bc12def34567";
            string buyerUserId1 = "ed4b9012-2345-6789-1234-bc12def34567";
            string buyerUserId2 = "fc5c0123-4567-8901-2345-bc12def34567";
            string buyerUserId3 = "ab6d1234-5678-9012-3456-bc12def34567";

            var hasher = new PasswordHasher<ApplicationUser>();

            // Seed users
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = "Admin User"
                },
                new ApplicationUser
                {
                    Id = sellerUserId1,
                    UserName = "seller1@example.com",
                    NormalizedUserName = "SELLER1@EXAMPLE.COM",
                    Email = "seller1@example.com",
                    NormalizedEmail = "SELLER1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Seller@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = "Seller One"
                },
                new ApplicationUser
                {
                    Id = sellerUserId2,
                    UserName = "seller2@example.com",
                    NormalizedUserName = "SELLER2@EXAMPLE.COM",
                    Email = "seller2@example.com",
                    NormalizedEmail = "SELLER2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Seller@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = "Seller Two"
                },
                new ApplicationUser
                {
                    Id = sellerUserId3,
                    UserName = "seller3@example.com",
                    NormalizedUserName = "SELLER3@EXAMPLE.COM",
                    Email = "seller3@example.com",
                    NormalizedEmail = "SELLER3@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Seller@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = "Seller Three"
                },
                new ApplicationUser
                {
                    Id = buyerUserId1,
                    UserName = "buyer1@example.com",
                    NormalizedUserName = "BUYER1@EXAMPLE.COM",
                    Email = "buyer1@example.com",
                    NormalizedEmail = "BUYER1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Buyer@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = "Buyer One"
                },
                new ApplicationUser
                {
                    Id = buyerUserId2,
                    UserName = "buyer2@example.com",
                    NormalizedUserName = "BUYER2@EXAMPLE.COM",
                    Email = "buyer2@example.com",
                    NormalizedEmail = "BUYER2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Buyer@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = "Buyer Two"
                },
                new ApplicationUser
                {
                    Id = buyerUserId3,
                    UserName = "buyer3@example.com",
                    NormalizedUserName = "BUYER3@EXAMPLE.COM",
                    Email = "buyer3@example.com",
                    NormalizedEmail = "BUYER3@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Buyer@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = "Buyer Three"
                }
            );

            // Seed user-role relationships
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId },
                new IdentityUserRole<string> { UserId = sellerUserId1, RoleId = sellerRoleId },
                new IdentityUserRole<string> { UserId = sellerUserId2, RoleId = sellerRoleId },
                new IdentityUserRole<string> { UserId = sellerUserId3, RoleId = sellerRoleId },
                new IdentityUserRole<string> { UserId = buyerUserId1, RoleId = buyerRoleId },
                new IdentityUserRole<string> { UserId = buyerUserId2, RoleId = buyerRoleId },
                new IdentityUserRole<string> { UserId = buyerUserId3, RoleId = buyerRoleId }
            );

            // Seeding PropertyCategory
            modelBuilder.Entity<PropertyCategory>().HasData(
                new PropertyCategory { Id = 1, Name = "Residential", DisplayOrder = 1 },
                new PropertyCategory { Id = 2, Name = "Commercial", DisplayOrder = 2 },
                new PropertyCategory { Id = 3, Name = "Land", DisplayOrder = 3 }
            );

            // Seeding Properties
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    PropertyId = 1,
                    Title = "Modern Family House",
                    Description = "A beautiful modern house located in the heart of the city.",
                    Location = "Los Angeles, CA",
                    Size = 2500.5,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 4,
                    NumberOfBathrooms = 3,
                    YearBuilt = 2015,
                    PropertyCategoryId = 1,
                    SellerId = sellerUserId1
                },
                new Property
                {
                    PropertyId = 2,
                    Title = "Luxury Beachfront Villa",
                    Description = "A stunning villa overlooking the ocean with premium amenities.",
                    Location = "Miami, FL",
                    Size = 4500.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 6,
                    NumberOfBathrooms = 5,
                    YearBuilt = 2020,
                    PropertyCategoryId = 2,
                    SellerId = sellerUserId1
                },
                new Property
                {
                    PropertyId = 3,
                    Title = "Cozy Cottage",
                    Description = "A charming cottage in a peaceful countryside setting.",
                    Location = "Asheville, NC",
                    Size = 1200.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 3,
                    NumberOfBathrooms = 2,
                    YearBuilt = 1990,
                    PropertyCategoryId = 3,
                    SellerId = sellerUserId2
                },
                new Property
                {
                    PropertyId = 4,
                    Title = "Downtown Penthouse",
                    Description = "Luxurious penthouse with panoramic city views.",
                    Location = "New York, NY",
                    Size = 3000.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 5,
                    NumberOfBathrooms = 4,
                    YearBuilt = 2018,
                    PropertyCategoryId = 1,
                    SellerId = sellerUserId2
                },
                new Property
                {
                    PropertyId = 5,
                    Title = "Commercial Office Space",
                    Description = "Prime location office space in business district.",
                    Location = "Chicago, IL",
                    Size = 5000.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 8,
                    NumberOfBathrooms = 4,
                    YearBuilt = 2019,
                    PropertyCategoryId = 2,
                    SellerId = sellerUserId3
                },
                new Property
                {
                    PropertyId = 6,
                    Title = "Waterfront Land Plot",
                    Description = "Beautiful waterfront land ready for development.",
                    Location = "Seattle, WA",
                    Size = 10000.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 0,
                    NumberOfBathrooms = 0,
                    YearBuilt = 0,
                    PropertyCategoryId = 3,
                    SellerId = sellerUserId3
                }
            );

            // Seeding AuctionListings
            modelBuilder.Entity<AuctionListing>().HasData(
                // Active Auctions
                new AuctionListing
                {
                    AuctionId = 1,
                    PropertyId = 1,
                    StartingBid = 500000,
                    CurrentHighestBid = 500000,
                    ReservationPrice = 600000,
                    MinimumBidIncrement = 5000,
                    StartDate = new DateTime(2025, 1, 28),  // Tomorrow
                    EndDate = new DateTime(2025, 2, 3),     // Week later
                    Status = AuctionStatus.Active,
                    IsBidStarted = false,
                    IsReservationPriceMet = false
                },
                new AuctionListing
                {
                    AuctionId = 2,
                    PropertyId = 2,
                    StartingBid = 1000000,
                    CurrentHighestBid = 1000000,
                    ReservationPrice = 1200000,
                    MinimumBidIncrement = 10000,
                    StartDate = new DateTime(2025, 1, 29),  // Day after tomorrow
                    EndDate = new DateTime(2025, 2, 6),     // 10 days later
                    Status = AuctionStatus.Active,
                    IsBidStarted = false,
                    IsReservationPriceMet = false
                },
                new AuctionListing
                {
                    AuctionId = 3,
                    PropertyId = 3,
                    StartingBid = 300000,
                    CurrentHighestBid = 300000,
                    ReservationPrice = 350000,
                    MinimumBidIncrement = 3000,
                    StartDate = new DateTime(2025, 2, 1),   // 5 days from now
                    EndDate = new DateTime(2025, 2, 11),    // 15 days from now
                    Status = AuctionStatus.Active,
                    IsBidStarted = false,
                    IsReservationPriceMet = false
                },
                // Completed Auctions
                new AuctionListing
                {
                    AuctionId = 4,
                    PropertyId = 4,
                    StartingBid = 800000,
                    CurrentHighestBid = 950000,
                    ReservationPrice = 900000,
                    MinimumBidIncrement = 5000,
                    StartDate = new DateTime(2024, 12, 15),
                    EndDate = new DateTime(2025, 1, 15),
                    Status = AuctionStatus.Completed,
                    IsBidStarted = true,
                    IsReservationPriceMet = true,
                    HighestBidderId = buyerUserId1
                },
                new AuctionListing
                {
                    AuctionId = 5,
                    PropertyId = 5,
                    StartingBid = 2000000,
                    CurrentHighestBid = 2500000,
                    ReservationPrice = 2200000,
                    MinimumBidIncrement = 20000,
                    StartDate = new DateTime(2024, 12, 1),
                    EndDate = new DateTime(2025, 1, 1),
                    Status = AuctionStatus.Completed,
                    IsBidStarted = true,
                    IsReservationPriceMet = true,
                    HighestBidderId = buyerUserId2
                },
                new AuctionListing
                {
                    AuctionId = 6,
                    PropertyId = 6,
                    StartingBid = 1500000,
                    CurrentHighestBid = 1800000,
                    ReservationPrice = 1700000,
                    MinimumBidIncrement = 10000,
                    StartDate = new DateTime(2024, 11, 15),
                    EndDate = new DateTime(2024, 12, 15),
                    Status = AuctionStatus.Completed,
                    IsBidStarted = true,
                    IsReservationPriceMet = true,
                    HighestBidderId = buyerUserId3
                }
                );
        }
    }
}