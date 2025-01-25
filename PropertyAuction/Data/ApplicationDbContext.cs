using Microsoft.EntityFrameworkCore;
using PropertyAuction.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PropertyAuction.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyCategory>().HasData(
                new PropertyCategory { Id = 1, Name = "Residential", DisplayOrder = 1 },
                new PropertyCategory { Id = 2, Name = "Commercial", DisplayOrder = 2 },
                new PropertyCategory { Id = 3, Name = "Land", DisplayOrder = 3 }
                );
            //    modelBuilder.Entity<Property>().HasData(
            //        new Property
            //        {
            //            PropertyId = 1,
            //            Title = "Modern Family House",
            //            Description = "A beautiful modern house located in the heart of the city.",
            //            Location = "Los Angeles, CA",
            //            Size = 2500.5,
            //            ImageUrl = "",
            //            VideoUrl = "",
            //            NumberOfRooms = 4,
            //            NumberOfBathrooms = 3,
            //            YearBuilt = 2015,
            //            PropertyCategoryId = 1
            //        },
            //        new Property
            //        {
            //            PropertyId = 2,
            //            Title = "Luxury Beachfront Villa",
            //            Description = "A stunning villa overlooking the ocean with premium amenities.",
            //            Location = "Miami, FL",
            //            Size = 4500.0,
            //            ImageUrl = "",
            //            VideoUrl = "",
            //            NumberOfRooms = 6,
            //            NumberOfBathrooms = 5,
            //            YearBuilt = 2020,
            //            PropertyCategoryId = 2
            //        },
            //        new Property
            //        {
            //            PropertyId = 3,
            //            Title = "Cozy Cottage",
            //            Description = "A charming cottage in a peaceful countryside setting.",
            //            Location = "Asheville, NC",
            //            Size = 1200.0,
            //            ImageUrl = "",
            //            VideoUrl = "",
            //            NumberOfRooms = 3,
            //            NumberOfBathrooms = 2,
            //            YearBuilt = 1990,
            //            PropertyCategoryId = 3
            //        },
            //        new Property
            //        {
            //            PropertyId = 4,
            //            Title = "Downtown Apartment",
            //            Description = "A compact and stylish apartment in the city center.",
            //            Location = "New York, NY",
            //            Size = 850.0,
            //            ImageUrl = "",
            //            VideoUrl = "",
            //            NumberOfRooms = 2,
            //            NumberOfBathrooms = 1,
            //            YearBuilt = 2005,
            //            PropertyCategoryId = 1
            //        },
            //        new Property
            //        {
            //            PropertyId = 5,
            //            Title = "Suburban Dream Home",
            //            Description = "A spacious home with a large yard in a quiet neighborhood.",
            //            Location = "Austin, TX",
            //            Size = 3200.0,
            //            ImageUrl = "",
            //            VideoUrl = "",
            //            NumberOfRooms = 5,
            //            NumberOfBathrooms = 4,
            //            YearBuilt = 2010,
            //            PropertyCategoryId = 2
            //        },
            //        new Property
            //        {
            //            PropertyId = 6,
            //            Title = "Rustic Mountain Cabin",
            //            Description = "A rustic cabin with breathtaking mountain views.",
            //            Location = "Denver, CO",
            //            Size = 1500.0,
            //            ImageUrl = "",
            //            VideoUrl = "",
            //            NumberOfRooms = 3,
            //            NumberOfBathrooms = 2,
            //            YearBuilt = 1985,
            //            PropertyCategoryId = 3
            //        }
            //    );
            
        }
        public DbSet<PropertyCategory> PropertyCategories { get; set; }
        //public DbSet<Property> Properties { get; set; }
    }
}
