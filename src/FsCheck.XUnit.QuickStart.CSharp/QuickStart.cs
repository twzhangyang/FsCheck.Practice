﻿using System.Linq;
using FsCheck.Xunit;
using Xunit;

namespace FsCheck.QuickStart.XUnit.CSharp
{
    public class QuickStart 
    {
        [Property(QuietOnSuccess = true, Verbose = true)]
        public bool ReverseReverseIsOriginal(int[] xs)
        {
            return xs.Reverse().Reverse().SequenceEqual(xs);
        }

        [Property(Verbose = true)]
        public bool ReverseIsNotOriginal(int[] xs)
        {
            return xs.Reverse().SequenceEqual(xs);
        }
        
        [Fact]
        public void QuantifiedProperties()
        {
            var orderedList = Arb.From<int[]>()
                .MapFilter(xs => xs.OrderBy(i => i).ToArray(), xs => xs.IsOrdered());

            Prop.ForAll<int>(x => Prop.ForAll(orderedList, xs => xs.Insert(x).IsOrdered()))
                .QuickCheckThrowOnFailure();
        }
    }
    
}