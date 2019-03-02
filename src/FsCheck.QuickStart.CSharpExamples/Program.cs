using System;
using System.Linq;

namespace FsCheck.QuickStart.CSharpExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Prop.ForAll<int[]>(xs => xs.Reverse().Reverse().SequenceEqual(xs))
                .Label("label 1")
                .QuickCheck("Reverse reverse is original");
            
            Prop.ForAll<int[]>(xs => xs.Reverse().SequenceEqual(xs))
                .Label("label 2")
                .QuickCheck("Reverse is not original");
            
            Prop.ForAll<int>(x => false.Label("Always false")
                    .And(Math.Abs(x) - x == 0))
                .QuickCheck("label 3");
            
            Prop.ForAll<int>(x => false.Label("Always false")
                    .And(Math.Abs(x) - x == 0))
                .VerboseCheck("label 4");

            Console.ReadKey();
        }
    }
}