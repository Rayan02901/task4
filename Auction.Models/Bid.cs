using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public string? UserId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }

        public virtual AuctionListing Auction { get; set; }
        public virtual ApplicationUser User { get; set; }

    }

}
