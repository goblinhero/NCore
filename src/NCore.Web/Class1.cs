using Nest;

namespace NCore.Web
{
    public interface IElasticQuery<T> : IElasticQuery
        where T : class
    {
        ISearchResponse<T> Results { get; }
    }
}