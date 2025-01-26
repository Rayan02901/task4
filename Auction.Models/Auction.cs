using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Auction.Models
{
    public class AuctionListing
    {
        [Key]
        public int AuctionId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        [ValidateNever]
        public virtual Property Property { get; set; }

        [Required]
        [Display(Name = "Auction Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Auction End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Auction Duration")]
        public TimeSpan AuctionDuration => EndDate - StartDate;

        [Required]
        [Display(Name = "Reservation Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Reservation price must be a positive value")]
        public decimal ReservationPrice { get; set; }

        [Required]
        [Display(Name = "Minimum Bid Increment")]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum bid increment must be a positive value")]
        public decimal MinimumBidIncrement { get; set; }

        [Display(Name = "Current Highest Bid")]
        public decimal? CurrentHighestBid { get; set; }

        [Display(Name = "Highest Bidder")]
        public string? HighestBidderId { get; set; }

        [Display(Name = "Auction Status")]
        public AuctionStatus Status { get; set; }

        [Display(Name = "Starting Bid")]
        [Range(0, double.MaxValue, ErrorMessage = "Starting bid must be a positive value")]
        public decimal StartingBid { get; set; }
    }

    public enum AuctionStatus
    {
        
        Active,
        Completed
        
    }
}