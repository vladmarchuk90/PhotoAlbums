using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.DAL.Entities;

namespace NLayer.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Album> Albums { get; }
        IRepository<Photo> Photos { get; }
        void Save();
    }
}
