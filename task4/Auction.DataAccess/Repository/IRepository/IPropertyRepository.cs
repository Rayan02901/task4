using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.DataAccess.Repository.IRepository;
using Auction.Models;

namespace Auction.DataAccess.Repository.IRepository
{
    public interface IPropertyRepository : IRepository<Property>
    {
        void Update(Property property);
    }
}