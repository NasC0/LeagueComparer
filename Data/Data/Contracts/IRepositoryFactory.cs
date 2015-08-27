using Models.Common;

namespace Data.Contracts
{
    public interface IRepositoryFactory
    {
        IMongoRepository<T> GetRepository<T>() where T : Entity;
    }
}
