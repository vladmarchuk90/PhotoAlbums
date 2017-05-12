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
    class UserRepository : IRepository<User>
    {
        private PhotoAlbumsContext dbContext;

        public UserRepository(PhotoAlbumsContext context)
        {
            this.dbContext = context;
        }

        public void Create(User user)
        {
            dbContext.Users.Add(user);
        }

        public void Delete(int id)
        {
            User user = dbContext.Users.Find(id);
            
            if (user != null)
            {
                dbContext.Users.Remove(user);
            }
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return dbContext.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return dbContext.Users;
        }

        public void Update(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
        }
    }
}
