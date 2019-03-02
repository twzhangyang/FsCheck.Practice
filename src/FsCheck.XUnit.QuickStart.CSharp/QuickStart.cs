using System.Linq;
using FsCheck.Xunit;

namespace FsCheck.QuickStart.Xunit.CSharp
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
    }
    
}