using System;

namespace FsCheck.QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            Prop.ForAll<int>(x => x != x + 1)
               .QuickCheck("Number always not equal self add 1");
            
            Prop.ForAll<int>(x => x != x + 1)
                .VerboseCheck("Number always not equal self add 1");


            Console.ReadKey();
        }
    }
}