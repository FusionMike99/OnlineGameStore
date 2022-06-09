using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineGameStore.MVC.Infrastructure
{
    public class TrimmingModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var value = valueProviderResult.FirstValue?.Trim();

            bindingContext.Result = ModelBindingResult.Success(value);

            return Task.CompletedTask;
        }
    }
}