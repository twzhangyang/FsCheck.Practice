using FsCheck.Xunit;

namespace FsCheck.QuickStart.XUnit.CSharp
{
    public class DollarExample
    {
        [Property]
        public bool SetAndGetShouldGiveSameResult(int amount)
        {
            var dollar = Dollar.Create(0);
            dollar.Add(amount);

            return dollar.Amount == amount;
        }

        [Property]
        public bool AddThenMultiplierSameAsCreate(int start, int times)
        {
            var dollar = Dollar.Create(0);
            dollar.Add(start);
            dollar.Multiplier(times);

            var dollar2 = Dollar.Create(start * times);

            return dollar.Amount == dollar2.Amount;
        }
        
        public class Dollar
        {
            private int _amount;

            public Dollar(int amount)
            {
                _amount = amount;
            } 
            
            public int Amount => _amount;
            
            public void Add(int add)
            {
                 _amount = _amount + add;
            }
            
            public void Multiplier(int multiplier)
            {
                _amount = _amount * multiplier;
            }
            
            public static Dollar Create(int amount)
            {
                return new Dollar(amount);
            }
        }
    }
}