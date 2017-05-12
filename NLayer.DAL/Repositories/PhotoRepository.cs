using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.DAL.EF;
using NLayer.DAL.Entities;
using NLayer.DAL.Interfaces;
using System.Data.Entity;

namespace NLayer.DAL.Repositories
{
    class PhotoRepository : IRepository<Photo>
    {
        private PhotoAlbumsContext dbContext;

        public PhotoRepository(PhotoAlbumsContext context)
        {
            this.dbContext = context;
        }

        public void Create(Photo photo)
        {
            dbContext.Photos.Add(photo);
        }

        public void Delete(int id)
        {
            Photo photo = dbContext.Photos.Find(id);

            if (photo != null)
            {
                dbContext.Photos.Remove(photo);
            }
        }

        public IEnumerable<Photo> Find(Func<Photo, bool> predicate)
        {
            return dbContext.Photos.Where(predicate).ToList();
        }

        public Photo Get(int id)
        {
            return dbContext.Photos.Find(id);
        }

        public IEnumerable<Photo> GetAll()
        {
            return dbContext.Photos;
        }

        public void Update(Photo photo)
        {
            dbContext.Entry(photo).State = EntityState.Modified;
        }
    }
}
