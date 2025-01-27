using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.DataAccess.Data;
using Auction.DataAccess.Repository.IRepository;
using Auction.Models;
using Microsoft.EntityFrameworkCore;

namespace Auction.DataAccess.Repository
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        private readonly ApplicationDbContext _db;

        public PropertyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Property property)
        {
            var objFromDb = _db.Properties
                .FirstOrDefault(u => u.PropertyId == property.PropertyId);

            if (objFromDb != null)
            {
                objFromDb.Title = property.Title;
                objFromDb.Description = property.Description;
                objFromDb.Location = property.Location;
                objFromDb.Size = property.Size;
                objFromDb.NumberOfRooms = property.NumberOfRooms;
                objFromDb.NumberOfBathrooms = property.NumberOfBathrooms;
                objFromDb.YearBuilt = property.YearBuilt;
                objFromDb.PropertyCategoryId = property.PropertyCategoryId;

                if (!string.IsNullOrEmpty(property.ImageUrl))
                {
                    objFromDb.ImageUrl = property.ImageUrl;
                }

                if (!string.IsNullOrEmpty(property.VideoUrl))
                {
                    objFromDb.VideoUrl = property.VideoUrl;
                }
            }
        }
    }
}