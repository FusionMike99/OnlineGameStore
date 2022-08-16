using System.Collections.Generic;
using OnlineGameStore.DAL.Pipelines.Filters;

namespace OnlineGameStore.DAL.Pipelines
{
    public abstract class Pipeline<TIn, TOut>
    {
        protected readonly List<IFilter<TIn, TOut>> filters = new List<IFilter<TIn, TOut>>();

        public Pipeline<TIn, TOut> Register(IFilter<TIn, TOut> filter)
        {
            filters.Add(filter);
            return this;
        }

        public abstract TOut Process(TIn input);
    }
}