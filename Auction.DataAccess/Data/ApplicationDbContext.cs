using Microsoft.EntityFrameworkCore;
using Auction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    PropertyCategoryId = 1
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
                    PropertyCategoryId = 2
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
                    PropertyCategoryId = 3
                }
            );

            // Seeding AuctionListings
            modelBuilder.Entity<AuctionListing>().HasData(
                new AuctionListing
                {
                    AuctionId = 1,
                    PropertyId = 1,  // Linking to Property with PropertyId 1 (Modern Family House)
                    StartingBid = 500000,
                    CurrentHighestBid = 500000,
                    ReservationPrice = 600000,
                    MinimumBidIncrement = 5000,
                    StartDate = DateTime.Now.AddDays(1),  // Auction starts tomorrow
                    EndDate = DateTime.Now.AddDays(7),    // Auction ends in 7 days
                    Status = AuctionStatus.Active
                },
                new AuctionListing
                {
                    AuctionId = 2,
                    PropertyId = 2,  // Linking to Property with PropertyId 2 (Luxury Beachfront Villa)
                    StartingBid = 1000000,
                    CurrentHighestBid = 1000000,
                    ReservationPrice = 1200000,
                    MinimumBidIncrement = 10000,
                    StartDate = DateTime.Now.AddDays(2),  // Auction starts in 2 days
                    EndDate = DateTime.Now.AddDays(10),   // Auction ends in 10 days
                    Status = AuctionStatus.Active
                },
                new AuctionListing
                {
                    AuctionId = 3,
                    PropertyId = 3,  // Linking to Property with PropertyId 3 (Cozy Cottage)
                    StartingBid = 300000,
                    CurrentHighestBid = 300000,
                    ReservationPrice = 350000,
                    MinimumBidIncrement = 3000,
                    StartDate = DateTime.Now.AddDays(5),  // Auction starts in 5 days
                    EndDate = DateTime.Now.AddDays(15),   // Auction ends in 15 days
                    Status = AuctionStatus.Active
                }
            );

            // Configuring decimal type columns for AuctionListing
            modelBuilder.Entity<AuctionListing>()
                .Property(a => a.ReservationPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AuctionListing>()
                .Property(a => a.MinimumBidIncrement)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AuctionListing>()
                .Property(a => a.CurrentHighestBid)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AuctionListing>()
                .Property(a => a.StartingBid)
                .HasColumnType("decimal(18,2)");

            // Specify decimal type for BidAmount in Bid entity
            modelBuilder.Entity<Bid>()
                .Property(b => b.BidAmount)
                .HasColumnType("decimal(18,2)"); // You can adjust the precision and scale as necessary

            // Configure relationships for other entities if necessary (e.g., Bid)
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Auction)
                .WithMany(a => a.Bids)
                .HasForeignKey(b => b.AuctionId);
        }

        

    }
}
