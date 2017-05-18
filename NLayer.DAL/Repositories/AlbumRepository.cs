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
    class AlbumRepository : IRepository<Album>
    {
        private PhotoAlbumsContext dbContext;

        public AlbumRepository(PhotoAlbumsContext context)
        {
            this.dbContext = context;
        }

        public void Create(Album album)
        {
            dbContext.Albums.Add(album);
        }

        public void Delete(int id)
        {
            Album album = dbContext.Albums.Find(id);

            if (album != null)
            {
                dbContext.Albums.Remove(album);
            }
        }

        public IEnumerable<Album> Find(Func<Album, bool> predicate)
        {
            return dbContext.Albums.Where(predicate).ToList();
        }

        public Album Get(int id)
        {
            return dbContext.Albums.Find(id);
        }

        public IEnumerable<Album> GetAll()
        {
            return dbContext.Albums;
        }

        public void Update(Album album)
        {
            //if (dbContext.Entry(album).State == EntityState.Detached)
            //    dbContext.Albums.Attach(album);

            string query =                      "UPDATE [dbo].[Albums] " +
                                                "SET amountOfLikes = @likes@ " +
                                                "WHERE Id = @id@";
            query = query.Replace("@id@", "" + album.Id).Replace("@likes@", "" + album.amountOfLikes);

            dbContext.Database.ExecuteSqlCommand(query);

            //dbContext.Entry(album).State = EntityState.Modified;
        }
    }
}
