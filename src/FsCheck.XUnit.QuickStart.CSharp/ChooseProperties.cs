using System.Collections.Generic;
using System.Linq;
using FsCheck.Xunit;

namespace FsCheck.QuickStart.XUnit.CSharp
{
    public class ChooseProperties
    {
        private int Add1(int x)
        {
            return x + 1;
        }

        [Property]
        public bool AddOneThenSortShouldSameAsSortThenAddOne(List<int> list)
        {
            var result1 = list.OrderBy(x => x).Select(Add1);
            var result2 = list.Select(Add1).OrderBy(x => x);

            return result1.SequenceEqual(result2);
        }

        [Property]
        public bool ReverseThenReverseShouldSameAsOriginal(int[] list)
        {
          var result=  list.Reverse().Reverse();

          return result.SequenceEqual(list);
        }
        
        [Property]
        public bool SomethingNeverChanged(List<int> list)
        {
            var result = list.OrderBy(x => x);

            return result.Count() == list.Count;
        }
    }
}