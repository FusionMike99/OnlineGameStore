using AutoFixture;
using Microsoft.AspNetCore.Mvc.ViewComponents;

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