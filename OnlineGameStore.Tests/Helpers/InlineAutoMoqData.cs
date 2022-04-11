using AutoFixture.Xunit2;

namespace OnlineGameStore.Tests.Helpers
{
    internal sealed class InlineAutoMoqData : InlineAutoDataAttribute
    {
        public InlineAutoMoqData(params object[] inlineValues) : base(new AutoMoqDataAttribute(), inlineValues)
        {
        }
    }
}
