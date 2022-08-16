using OnlineGameStore.DAL.Pipelines;

namespace OnlineGameStore.DAL.Builders.PipelineBuilders.Interfaces
{
    public interface IPipelineBuilder<TIn, TOut>
    {
        Pipeline<TIn, TOut> CreatePipeline();
    }
}