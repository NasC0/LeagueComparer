using ApiProcessing.Enumerations;

namespace ApiProcessing.Contracts
{
    public interface IQueryResponse
    {
        string Content { get; }
        ObjectType QueryObjectType { get; }
    }
}
