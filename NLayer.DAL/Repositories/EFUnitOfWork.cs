using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.DAL.EF;
using NLayer.DAL.Entities;
using NLayer.DAL.Interfaces;
using System.Data.Entity;
using System.Configuration;

namespace NLayer.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private PhotoAlbumsContext dbContext;
        private UserRepository userRepository;
        private AlbumRepository albumRepository;
        private PhotoRepository photoRepository;

        public EFUnitOfWork(string connectionString)
        {
            dbContext = new PhotoAlbumsContext(connectionString);
        }


        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(dbContext);

                return userRepository;
            }
        }

        public IRepository<Album> Albums
        {
            get
            {
                if (albumRepository == null)
                    albumRepository = new AlbumRepository(dbContext);

                return albumRepository;
            }
        }

        public IRepository<Photo> Photos
        {
            get
            {
                if (photoRepository == null)
                    photoRepository = new PhotoRepository(dbContext);

                return photoRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
