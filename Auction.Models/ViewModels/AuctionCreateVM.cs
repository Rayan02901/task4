using Auction.Models;
using System.ComponentModel.DataAnnotations;

public class AuctionCreateVM
{
    [Required]
    public int PropertyId { get; set; }

    [Required]
    [Display(Name = "Auction Start Date")]
    public DateTime StartDate { get; set; }

    [Required]
    [Display(Name = "Auction End Date")]
    public DateTime EndDate { get; set; }

    [Required]
    [Display(Name = "Reservation Price")]
    [Range(0, double.MaxValue)]
    public decimal ReservationPrice { get; set; }

    [Required]
    [Display(Name = "Minimum Bid Increment")]
    [Range(0, double.MaxValue)]
    public decimal MinimumBidIncrement { get; set; }

    [Required]
    [Display(Name = "Starting Bid")]
    [Range(0, double.MaxValue)]
    public decimal StartingBid { get; set; }

    public Property? SelectedProperty { get; set; }
}