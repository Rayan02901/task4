// BidsRepository.cs
using Auction.DataAccess.Data;
using Auction.DataAccess.Repository.IRepository;
using Auction.Models;

namespace Auction.DataAccess.Repository
{
    public class BidsRepository : Repository<Bid>, IBidsRepository
    {
        private readonly ApplicationDbContext _db;

        public BidsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Bid obj)
        {
            _db.Bids.Update(obj);
        }
    }
}