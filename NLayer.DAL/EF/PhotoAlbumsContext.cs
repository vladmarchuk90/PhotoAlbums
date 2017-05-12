using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NLayer.DAL.Entities;

namespace NLayer.DAL.EF
{
    class PhotoAlbumsContext: DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }

        static PhotoAlbumsContext()
        {
            Database.SetInitializer<PhotoAlbumsContext>(new StoreDbInitializer());
        }
        public PhotoAlbumsContext(string connectionString)
            : base(connectionString)
        {
           
        }
    }
}
