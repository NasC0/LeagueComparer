using ComparerAuthenticationProofOfConcept.Infrastructure;
using Data;
using Data.Contracts;
using Models;
using MongoDB.Driver;
using Ninject;
using Ninject.Modules;

namespace ComparerAuthenticationProofOfConcept
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            var mongoDb = new MongoClient(Properties.Settings.Default.MongoConnection);
            var mongoConnection = mongoDb.GetDatabase(Properties.Settings.Default.DatabaseName);

            Kernel.Bind<IDbContext>().To<ApplicationDbContext>();
            Kernel.Bind<IRepositoryFactory>().To<RepositoryFactory>()
                .WithConstructorArgument(mongoConnection);
        }
    }
}
