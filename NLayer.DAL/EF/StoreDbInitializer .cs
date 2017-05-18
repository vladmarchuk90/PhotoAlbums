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
            User admin = new User { Name = "admin@gmail.com", FullName = "Admin" };
           
            dbContext.SaveChanges();
            
            //Creating Albums
            Album wasington = new Album
            {
                Name = "Washington air museum",
                Description = "Air museum in the centre of Washington",
                amountOfLikes = 0,
                User = admin
            };

            Album houses = new Album
            {
                Name = "Houses",
                Description = "Nice views of houses",
                amountOfLikes = 0,
                User = admin
            };

            Album beach = new Album
            {
                Name = "Beaches",
                Description = "How good on the beach...",
                amountOfLikes = 0,
                User = admin
            };

            Album beauty = new Album
            {
                Name = "Beauty",
                Description = "Beautiful nature",
                amountOfLikes = 0,
                User = admin
            };

            Album cityNight = new Album
            {
                Name = "City's night",
                Description = "Cities in nights",
                amountOfLikes = 0,
                User = admin
            };

            Album love = new Album
            {
                Name = "Love",
                Description = "Love and sun",
                amountOfLikes = 0,
                User = admin
            };

            Album nature = new Album
            {
                Name = "Nature",
                Description = "Nice prospects of nature",
                amountOfLikes = 0,
                User = admin
            };

            Album newyork = new Album
            {
                Name = "New York city",
                Description = "Photos of NYC",
                amountOfLikes = 0,
                User = admin
            };

            Album palms = new Album
            {
                Name = "Palms",
                Description = "Just palms",
                amountOfLikes = 0,
                User = admin
            };

            dbContext.SaveChanges();

            for (int i = 1; i <= 18; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = wasington,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/wmuseum/0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/wmuseum/thumb_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "Washington museum " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            for (int i = 1; i <= 12; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = houses,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/houses/0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/houses/thumb_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "Houses " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            for (int i = 1; i <= 17; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = beach,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/beach/beach_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/beach/thumb_beach_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "Beaches " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            for (int i = 1; i <= 20; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = beauty,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/beauty/beauty_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/beauty/thumb_beauty_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "Beauty " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            for (int i = 1; i <= 18; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = cityNight,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/citynight/citynight_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/citynight/thumb_citynight_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "City night " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            for (int i = 1; i <= 10; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = palms,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/palms/palms_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/palms/thumb_palms_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "Palm " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            for (int i = 1; i <= 10; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = newyork,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/newyork/newyork_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/newyork/thumb_newyork_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "New York " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            for (int i = 1; i <= 14; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = love,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/love/love_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/love/thumb_love_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "Love " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            for (int i = 1; i <= 12; i++)
            {
                dbContext.Photos.Add(new Photo()
                {
                    Album = nature,
                    DateUploaded = DateTime.Now,
                    PathToPhoto = "/PhotoStorage/general/nature/nature_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    PathToThumbPhoto = "/PhotoStorage/general/nature/thumb_nature_0" + (i < 10 ? "0" + i.ToString() : i.ToString()) + ".jpg",
                    Title = "Nature " + i,
                    amountOfLikes = 0,
                    User = admin
                });
            }

            dbContext.SaveChanges();
        }
    }
}
