using System;
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


        [Fact]
        public void MapGenerator()
        {
            var gen = Gen.Choose(0, 127)
                .Select(x => (byte) x);

            var value = Gen.Sample(0, 10, gen);
        }

        [Fact]
        public void MapDateGenerator()
        {
            var gen = Gen.Choose(1, 30)
                .Select(x => new DateTime(2019, 11, x).ToString("u"));

            var value = Gen.Sample(0, 10, gen);
        }

        [Fact]
        public void ListOfGenerator()
        {
            var gen = Gen.Constant(42)
                .ListOf(1);

            var value = Gen.Sample(1, 10, gen);
        }

        [Fact]
        public void NonEmptyGenerator()
        {
            var gen = Gen.Elements("foo", "bar", "baz")
                .NonEmptyListOf();

            var value = Gen.Sample(3, 4, gen);
        }

        [Fact]
        public void Filter()
        {
            var gen = Gen.Choose(1, 100)
                .Two()
                .Where(x => x.Item1 != x.Item2)
                .Select(x => new List<int> {x.Item1, x.Item2});

            var value = gen.Sample(0, 10);
        }
        
        [Fact]
        public void GenerateUser()
        {
            var gen = Arb.Generate<User>();

            var value = gen.Sample(0, 3);
        }
        
        public class User
        {
            public string Name { get; set; }
            
            public int Age { get; set; }
        }
    }
}