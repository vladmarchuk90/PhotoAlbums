using Ninject.Modules;
using NLayer.DAL.Interfaces;
using NLayer.DAL.Repositories;

namespace NLayer.BLL.Services
{
    public class ServiceModel : NinjectModule
    {
        private string connectionString;

        public ServiceModel(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
