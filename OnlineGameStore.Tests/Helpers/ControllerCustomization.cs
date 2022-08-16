using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace OnlineGameStore.Tests.Helpers
{
    internal class ControllerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new ControllerBasePropertyOmitter());
            fixture.Inject(new ViewDataDictionary(fixture.Create<DefaultModelMetadataProvider>(),
                fixture.Create<ModelStateDictionary>()));
        }

        private class ControllerBasePropertyOmitter : Omitter
        {
            public ControllerBasePropertyOmitter()
                : base(new OrRequestSpecification(GetPropertySpecifications()))
            {
            }

            private static IEnumerable<IRequestSpecification> GetPropertySpecifications()
            {
                return typeof(ControllerBase).GetProperties().Where(x => x.CanWrite)
                    .Select(x => new PropertySpecification(x.PropertyType, x.Name));
            }
        }
    }
}