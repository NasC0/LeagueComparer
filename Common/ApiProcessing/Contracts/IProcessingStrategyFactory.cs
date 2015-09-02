using ApiProcessing.Enumerations;

namespace ApiProcessing.Contracts
{
    public interface IProcessingStrategyFactory
    {
        IGameObjectProcessingStrategy GetProcessingStrategy(ObjectType queryResponseType);
    }
}
