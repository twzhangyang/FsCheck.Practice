namespace FsCheck.ModelBased.CSharp.Testing
{
    public class Counter
    {
        private int _n;

        public void Inc()
        {
            _n = _n + 1;
        }

        public void Dec()
        {
            if (_n > 2)
            {
                _n = _n - 2;
            }
            else
            {
                _n = _n - 1;
            }
        }

        public int Get()
        {
            return _n;
        }

        public void Reset()
        {
            _n = 0;
        }
    }
}