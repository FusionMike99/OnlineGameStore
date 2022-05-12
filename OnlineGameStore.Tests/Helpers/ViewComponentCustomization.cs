using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace OnlineGameStore.Tests.Helpers
{
    public class ViewComponentCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Inject(new ViewComponentContext());
        }
    }
}