using Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DataAccess.Repository.IRepository
{
    public interface IAuctionListingRepository : IRepository<AuctionListing>
    {
        // Define any additional methods specific to AuctionListings here
        void Update(AuctionListing auctionlisting);
    }
}
