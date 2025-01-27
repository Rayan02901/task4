using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models.ViewModels
{
    public class AuctionReportVM
    {
        public int AuctionId { get; set; }
        public string PropertyTitle { get; set; }
        public decimal WinningBid { get; set; }
        public string HighestBidderName { get; set; }
    }
}