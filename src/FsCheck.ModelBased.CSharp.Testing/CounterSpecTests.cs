using FsCheck.Xunit;
using Xunit;

namespace FsCheck.ModelBased.CSharp.Testing
{
    public class CounterSpecTests
    {
        [Fact]
        public void CounterTests()
        {
            new CounterSpec()
                .ToProperty()
                .QuickCheckThrowOnFailure();
                
        }
    }
}