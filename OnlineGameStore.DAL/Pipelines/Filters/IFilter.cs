namespace OnlineGameStore.DAL.Pipelines.Filters
{
    public interface IFilter<in TIn, out TOut>
    {
        TOut Execute(TIn input);   
    }
}