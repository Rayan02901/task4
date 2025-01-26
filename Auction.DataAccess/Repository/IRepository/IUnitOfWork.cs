using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPropertyCategoryRepository PropertyCategory { get; }
        IPropertyRepository Property { get; }
        IAuctionListingRepository AuctionListing { get; }
        IBidsRepository Bids { get; }

        void Save();
    }
}
