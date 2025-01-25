using Auction.DataAccess.Repository.IRepository;
using Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Auction.DataAccess.Repository.IRepository
{
    public interface IPropertyCategoryRepository : IRepository<PropertyCategory>
    {
        void Update(PropertyCategory obj);
        
    }
}