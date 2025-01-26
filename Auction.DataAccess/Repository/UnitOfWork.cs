using Auction.DataAccess.Data;
using Auction.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IPropertyCategoryRepository PropertyCategory { get; private set; }
        public IPropertyRepository Property { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            PropertyCategory = new PropertyCategoryRepository(_db);
            Property = new PropertyRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}