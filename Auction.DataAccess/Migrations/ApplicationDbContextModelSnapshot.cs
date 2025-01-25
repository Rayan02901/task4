﻿// <auto-generated />
using Auction.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Auction.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Auction.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfBathrooms")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("int");

                    b.Property<int>("PropertyCategoryId")
                        .HasColumnType("int");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearBuilt")
                        .HasColumnType("int");

                    b.HasKey("PropertyId");

                    b.HasIndex("PropertyCategoryId");

                    b.ToTable("Properties");

                    b.HasData(
                        new
                        {
                            PropertyId = 1,
                            Description = "A beautiful modern house located in the heart of the city.",
                            ImageUrl = "",
                            Location = "Los Angeles, CA",
                            NumberOfBathrooms = 3,
                            NumberOfRooms = 4,
                            PropertyCategoryId = 1,
                            Size = 2500.5,
                            Title = "Modern Family House",
                            VideoUrl = "",
                            YearBuilt = 2015
                        },
                        new
                        {
                            PropertyId = 2,
                            Description = "A stunning villa overlooking the ocean with premium amenities.",
                            ImageUrl = "",
                            Location = "Miami, FL",
                            NumberOfBathrooms = 5,
                            NumberOfRooms = 6,
                            PropertyCategoryId = 2,
                            Size = 4500.0,
                            Title = "Luxury Beachfront Villa",
                            VideoUrl = "",
                            YearBuilt = 2020
                        },
                        new
                        {
                            PropertyId = 3,
                            Description = "A charming cottage in a peaceful countryside setting.",
                            ImageUrl = "",
                            Location = "Asheville, NC",
                            NumberOfBathrooms = 2,
                            NumberOfRooms = 3,
                            PropertyCategoryId = 3,
                            Size = 1200.0,
                            Title = "Cozy Cottage",
                            VideoUrl = "",
                            YearBuilt = 1990
                        },
                        new
                        {
                            PropertyId = 4,
                            Description = "A compact and stylish apartment in the city center.",
                            ImageUrl = "",
                            Location = "New York, NY",
                            NumberOfBathrooms = 1,
                            NumberOfRooms = 2,
                            PropertyCategoryId = 1,
                            Size = 850.0,
                            Title = "Downtown Apartment",
                            VideoUrl = "",
                            YearBuilt = 2005
                        },
                        new
                        {
                            PropertyId = 5,
                            Description = "A spacious home with a large yard in a quiet neighborhood.",
                            ImageUrl = "",
                            Location = "Austin, TX",
                            NumberOfBathrooms = 4,
                            NumberOfRooms = 5,
                            PropertyCategoryId = 2,
                            Size = 3200.0,
                            Title = "Suburban Dream Home",
                            VideoUrl = "",
                            YearBuilt = 2010
                        },
                        new
                        {
                            PropertyId = 6,
                            Description = "A rustic cabin with breathtaking mountain views.",
                            ImageUrl = "",
                            Location = "Denver, CO",
                            NumberOfBathrooms = 2,
                            NumberOfRooms = 3,
                            PropertyCategoryId = 3,
                            Size = 1500.0,
                            Title = "Rustic Mountain Cabin",
                            VideoUrl = "",
                            YearBuilt = 1985
                        });
                });

            modelBuilder.Entity("Auction.Models.PropertyCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("PropertyCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Residential"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Commercial"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Land"
                        });
                });

            modelBuilder.Entity("Auction.Models.Property", b =>
                {
                    b.HasOne("Auction.Models.PropertyCategory", "PropertyCategory")
                        .WithMany()
                        .HasForeignKey("PropertyCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertyCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
