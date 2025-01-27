using Auction.DataAccess.Data;
using Auction.DataAccess.Repository.IRepository;
using Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DataAccess.Repository
{
    public class AuctionListingRepository : Repository<AuctionListing>, IAuctionListingRepository
    {
        private readonly ApplicationDbContext _db;

        public AuctionListingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(AuctionListing obj)
        {
            _db.AuctionListings.Update(obj);
        }

        // You can add more custom methods specific to AuctionListing if needed
    }
}
