using System.Collections.Generic;
using Xunit;

namespace FsCheck.QuickStart.XUnit.CSharp
{
    public class GeneratorExamples
    {
        [Fact]
        public void ConstantGenerator()
        {
            var gen = Gen.Constant("foo");

            var strings = Gen.Sample(0, 5, gen);
        }

        [Fact]
        public void ChooseGenerator()
        {
            var gen = Gen.Choose(0, 10);

            var value = Gen.Sample(0, 5, gen);
        }

        [Fact]
        public void ElementsGenerator()
        {
            var gen = Gen.Elements(42, 1337, 7, -100, 1453, -273);

            var value = Gen.Sample(0, 5, gen);
        }

        [Fact]
        public void GrowingElementsGenerator()
        {
            var gen = Gen.GrowingElements(new List<char> {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'});

            var value = Gen.Sample(3, 5, gen);
        }
    }
}