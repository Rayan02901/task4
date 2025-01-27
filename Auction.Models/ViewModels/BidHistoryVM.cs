using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models.ViewModels
{
    public class BidHistoryVM
    {
        public int BidId { get; set; }
        public int AuctionId { get; set; }
        public string PropertyTitle { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }
        public AuctionStatus AuctionStatus { get; set; }
        public DateTime AuctionEndDate { get; set; }
    }
}
