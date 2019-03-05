using System.Collections.Generic;
using System.Linq;

namespace FsCheck.QuickStart.XUnit.CSharp
{
    public static class LIstExtensions
    {
        public static IEnumerable<int> Insert(this IEnumerable<int> cs, int x)
        {
            var result = new List<int>(cs);
            foreach (var c in cs)
            {
                if (x <= c)
                {
                    result.Insert(result.IndexOf(c), x);
                    return result;
                }
            }
            result.Add(x);
            return result;
        }

        ///<returns> true iff source is strictly decreasing </returns>
        public static bool IsOrdered<T>(this IEnumerable<T> source)
        {
            var comparer = Comparer<T>.Default;
            return source.Zip(source.Skip(1), (a,b) => comparer.Compare(a,b) > 0)
                .Aggregate((a,b) => a && b);
        }
    }
}