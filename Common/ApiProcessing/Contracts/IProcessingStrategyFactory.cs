namespace ApiProcessing.Contracts
{
    public interface IProcessingStrategyFactory
    {
        IGameObjectProcessingStrategy GetProcessingStrategy(IQueryResponse queryResponse);
    }
}
