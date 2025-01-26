﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models.ViewModels
{
    public class AuctionListVM
    {
        public int AuctionId { get; set; }
        public string PropertyTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingBid { get; set; }
        public decimal CurrentHighestBid { get; set; }
        public AuctionStatus Status { get; set; }
    }
}
