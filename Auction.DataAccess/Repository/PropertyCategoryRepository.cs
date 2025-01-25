using Auction.DataAccess.Data;
using Auction.DataAccess.Repository.IRepository;
using Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Auction.DataAccess.Repository
{
    public class PropertyCategoryRepository : Repository<PropertyCategory>, IPropertyCategoryRepository
    {
        private ApplicationDbContext _db;
        public PropertyCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(PropertyCategory obj)
        {
            _db.PropertyCategories.Update(obj);
        }
    }
}