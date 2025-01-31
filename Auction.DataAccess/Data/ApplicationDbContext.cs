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
            string adminUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            string sellerUserId1 = "b68d3f5a-b414-48d9-8e34-7c6dde3941ef";
            string sellerUserId2 = "c1ce93ad-1e91-4b79-9f43-9f1d51d33511";
            string sellerUserId3 = "d9a9b8d7-73b2-4f28-9177-e1f9239e6673";
            string buyerUserId1 = "e4c2f668-5c51-4fd1-9b55-b5dd9b33abb7";
            string buyerUserId2 = "f682334f-82e1-4b89-8a56-89e4b7efde93";
            string buyerUserId3 = "g7d1c8e2-94a3-4d6b-ac32-b61e1e7b76d9";

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
                    Title = "Sunset Valley Estate",
                    Description = "Elegant contemporary mansion with smart home features and panoramic valley views.",
                    Location = "Beverly Hills, CA",
                    Size = 5200.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 6,
                    NumberOfBathrooms = 5,
                    YearBuilt = 2022,
                    PropertyCategoryId = 1,
                    SellerId = sellerUserId1
                },
                new Property
                {
                    PropertyId = 2,
                    Title = "Ocean Breeze Resort Complex",
                    Description = "Luxury beachfront resort property with private beach access and full-service amenities.",
                    Location = "Maui, HI",
                    Size = 8500.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 12,
                    NumberOfBathrooms = 14,
                    YearBuilt = 2021,
                    PropertyCategoryId = 2,
                    SellerId = sellerUserId1
                },
                new Property
                {
                    PropertyId = 3,
                    Title = "Mountain View Ranch",
                    Description = "Expansive ranch property with equestrian facilities and mountain backdrop.",
                    Location = "Aspen, CO",
                    Size = 15000.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 4,
                    NumberOfBathrooms = 3,
                    YearBuilt = 2015,
                    PropertyCategoryId = 3,
                    SellerId = sellerUserId2
                },
                new Property
                {
                    PropertyId = 4,
                    Title = "Skyline Tower Penthouse",
                    Description = "Ultra-luxury penthouse with 360-degree views and private helipad access.",
                    Location = "Manhattan, NY",
                    Size = 4800.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 5,
                    NumberOfBathrooms = 5,
                    YearBuilt = 2023,
                    PropertyCategoryId = 1,
                    SellerId = sellerUserId2
                },
                new Property
                {
                    PropertyId = 5,
                    Title = "Innovation Hub Complex",
                    Description = "State-of-the-art office complex with sustainable design and tech infrastructure.",
                    Location = "Austin, TX",
                    Size = 12000.0,
                    ImageUrl = "",
                    VideoUrl = "",
                    NumberOfRooms = 20,
                    NumberOfBathrooms = 8,
                    YearBuilt = 2024,
                    PropertyCategoryId = 2,
                    SellerId = sellerUserId3
                },
                new Property
                {
                    PropertyId = 6,
                    Title = "Vineyard Estate Development",
                    Description = "Prime wine country acreage with approved development plans and existing vineyards.",
                    Location = "Napa Valley, CA",
                    Size = 25000.0,
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
                    StartingBid = 8500000,
                    CurrentHighestBid = 8500000,
                    ReservationPrice = 10000000,
                    MinimumBidIncrement = 50000,
                    StartDate = new DateTime(2025, 1, 28),
                    EndDate = new DateTime(2025, 2, 3),
                    Status = AuctionStatus.Active,
                    IsBidStarted = false,
                    IsReservationPriceMet = false
                },
                new AuctionListing
                {
                    AuctionId = 2,
                    PropertyId = 2,
                    StartingBid = 15000000,
                    CurrentHighestBid = 15000000,
                    ReservationPrice = 18000000,
                    MinimumBidIncrement = 100000,
                    StartDate = new DateTime(2025, 1, 29),
                    EndDate = new DateTime(2025, 2, 6),
                    Status = AuctionStatus.Active,
                    IsBidStarted = false,
                    IsReservationPriceMet = false
                },
                new AuctionListing
                {
                    AuctionId = 3,
                    PropertyId = 3,
                    StartingBid = 12000000,
                    CurrentHighestBid = 12000000,
                    ReservationPrice = 14000000,
                    MinimumBidIncrement = 75000,
                    StartDate = new DateTime(2025, 2, 1),
                    EndDate = new DateTime(2025, 2, 11),
                    Status = AuctionStatus.Active,
                    IsBidStarted = false,
                    IsReservationPriceMet = false
                },
                // Completed Auctions
                new AuctionListing
                {
                    AuctionId = 4,
                    PropertyId = 4,
                    StartingBid = 25000000,
                    CurrentHighestBid = 28500000,
                    ReservationPrice = 27000000,
                    MinimumBidIncrement = 150000,
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
                    StartingBid = 45000000,
                    CurrentHighestBid = 52000000,
                    ReservationPrice = 48000000,
                    MinimumBidIncrement = 250000,
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
                    StartingBid = 20000000,
                    CurrentHighestBid = 23500000,
                    ReservationPrice = 22000000,
                    MinimumBidIncrement = 100000,
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