using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models.ViewModels
{
    public class AuctionDetailsForBuyerVM
    {
        public int AuctionId { get; set; }
        public Property Property { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingBid { get; set; }
        public decimal CurrentHighestBid { get; set; }
        public decimal ReservationPrice { get; set; }
        public decimal MinimumBidIncrement { get; set; }
        public AuctionStatus Status { get; set; }
        public List<Bid> RecentBids { get; set; }
        public TimeSpan RemainingTime => EndDate - DateTime.Now;
    }
}
