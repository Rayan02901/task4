// IBidsRepository.cs
using Auction.DataAccess.Repository.IRepository;
using Auction.Models;

namespace Auction.DataAccess.Repository.IRepository
{
    public interface IBidsRepository : IRepository<Bid>
    {
        void Update(Bid bid);
    }
}