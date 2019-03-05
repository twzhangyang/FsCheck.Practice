namespace FsCheck.ModelBased.CSharp.Testing
{
    public class IncCommand : Command<Counter, int>
    {
        public override Counter RunActual(Counter counter)
        {
            counter.Inc();

            return counter;
        }

        public override int RunModel(int m)
        {
            return m + 1;
        }

        public override Property Post(Counter counter, int m)
        {
            return (m == counter.Get()).ToProperty();
        }

        public override string ToString()
        {
            return "inc";
        }
    }

    public class DecCommand : Command<Counter, int>
    {
        public override Counter RunActual(Counter counter)
        {
            counter.Dec();

            return counter;
        }

        public override int RunModel(int m)
        {
            return m - 1;
        }

        public override Property Post(Counter counter, int m)
        {
            return (m == counter.Get()).ToProperty();
        }

        public override string ToString()
        {
            return "dec";
        }
    }
}