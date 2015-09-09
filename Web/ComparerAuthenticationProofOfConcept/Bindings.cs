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
            Kernel.Bind<IRepositoryFactory>().To<RepositoryFactory>().WithConstructorArgument(mongoConnection);

            var repoFactory = Kernel.TryGet<IRepositoryFactory>();

            Kernel.Bind<IRepository<Item>>().ToMethod(context => repoFactory.GetRepository<Item>());
            Kernel.Bind<IRepository<Champion>>().ToMethod(context => repoFactory.GetRepository<Champion>());
            Kernel.Bind<IRepository<Mastery>>().ToMethod(context => repoFactory.GetRepository<Mastery>());
            Kernel.Bind<IRepository<Rune>>().ToMethod(context => repoFactory.GetRepository<Rune>());
        }
    }
}
