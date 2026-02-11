using PipesAndFilters.Messages;

namespace PipesAndFilters.Filters
{
    public interface IFilter
    {
        IMessage Run(IMessage message);
    }
}