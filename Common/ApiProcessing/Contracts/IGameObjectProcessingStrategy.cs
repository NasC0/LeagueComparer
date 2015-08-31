using System.Threading.Tasks;
namespace ApiProcessing.Contracts
{
    public interface IGameObjectProcessingStrategy
    {
        Task ProcessQueryResponse(IQueryResponse response);
    }
}
