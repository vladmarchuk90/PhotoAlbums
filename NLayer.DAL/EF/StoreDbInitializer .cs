using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NLayer.DAL.Entities;

namespace NLayer.DAL.EF
{
    class StoreDbInitializer : DropCreateDatabaseIfModelChanges<PhotoAlbumsContext>
    {

        protected override void Seed(PhotoAlbumsContext dbContext)
        {
            //Creating users
            User user1 = new User { Name = "MiracleEyes", Password = "" };
            User user2 = new User { Name = "CrazyIve", Password = "" };
            User user3 = new User { Name = "likePresly", Password = "" };
            User user4 = new User { Name = "GiveMeMore", Password = "" };
            User user5 = new User { Name = "OpenMinded", Password = "" };

            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Users.Add(user3);
            dbContext.Users.Add(user4);
            dbContext.Users.Add(user5);

            dbContext.SaveChanges();

            //Creating Albums
            Album myTripToAsiaAlbum = new Album
            {
                Name = "My trip to Asia",
                Description = "In this album you can see how beautiful prospectives can be!",
                User = user1
            };
            Album beGreenAlbum = new Album
            {
                Name = "Be progreen!",
                Description = "Try to be progreen as we did it!",
                User = user2
            };
            Album cyclingAlbum = new Album
            {
                Name = "Cycling in Washington",
                Description = "",
                User = user2
            };
            Album computersAlbum = new Album
            {
                Name = "Computers of future.",
                Description = "New computers is coming.",
                User = user3
            };
            Album changesInMyLifeAlbum = new Album
            {
                Name = "Changes in my life",
                Description = "Changes in my life and how I do it.",
                User = user4
            };
            Album fishingAlbum = new Album
            {
                Name = "Fishing is exciting!",
                Description = "Fishing from my boat.",
                User = user5
            };

            dbContext.Albums.Add(myTripToAsiaAlbum);
            dbContext.Albums.Add(beGreenAlbum);
            dbContext.Albums.Add(cyclingAlbum);
            dbContext.Albums.Add(computersAlbum);
            dbContext.Albums.Add(changesInMyLifeAlbum);
            dbContext.Albums.Add(fishingAlbum);

            dbContext.SaveChanges();

        }
    }
}
