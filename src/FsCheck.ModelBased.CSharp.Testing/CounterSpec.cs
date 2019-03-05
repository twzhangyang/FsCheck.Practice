namespace FsCheck.ModelBased.CSharp.Testing
{
    public class CounterSpec: ICommandGenerator<Counter, int>
    {
        public Gen<Command<Counter, int>> Next(int value)
        {
            return Gen.Elements(new Command<Counter, int>[] {new IncCommand(), new DecCommand(),});
        }

        public Counter InitialActual => new Counter();
        
        public int InitialModel => 0;
    }
}