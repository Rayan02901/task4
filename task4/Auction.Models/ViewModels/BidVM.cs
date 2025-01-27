using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models.ViewModels
{
    public class BidVM
    {
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
        public string BidderName { get; set; }
    }
}
