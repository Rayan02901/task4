using Auction.Models;

public class SellerAuctionDetailsVM
{
    public int AuctionId { get; set; }
    public Property Property { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal StartingBid { get; set; }
    public decimal CurrentHighestBid { get; set; }
    public decimal ReservationPrice { get; set; }
    public decimal MinimumBidIncrement { get; set; }
    public bool IsReservationPriceMet { get; set; }
    public bool IsBidStarted { get; set; }
    public AuctionStatus Status { get; set; }
    public string? HighestBidderId { get; set; }
    public List<Bid> Bids { get; set; }
    public TimeSpan RemainingTime => EndDate - DateTime.Now;
}