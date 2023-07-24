namespace HashTable
{
    public class AssociativeArray<TKey, TValue> where TKey : IComparable<TKey>
    {
        KeyValuePair<TKey, TValue>[] _data;
        public int Count { get; private set }

        public AssociativeArray(int capacity)
        {
            _data = new KeyValuePair<TKey, TValue>[capacity];
        }

        public void Add(TKey key, TValue value)
        {
            if (Count == _data.Length)
                throw new InvalidOperationException();

            var hash = Math.Abs(key.GetHashCode());
            var pos = hash % _data.Length;


#if DEBUG
            Console.WriteLine("POS: {0}", pos);
#endif


            while (_data[pos] != null)
            {
                if (_data[pos].key.CompareTo(key) == 0)
                {
                    throw new Exception();
                }
                pos = (pos + 1) % _data.Length;
            }

            _data[pos] = new KeyValuePair<TKey, TValue>(key, value);
            Count++;
        }

        public TValue Get(TKey key)
        {
            var hash = Math.Abs(key.GetHashCode());
            var pos = hash % _data.Length;
            var stopConditional = pos;

            while (_data[pos] != null)
            {
                if (_data[pos].key.CompareTo(key) == 0)
                {
                    return _data[pos].value;
                }

                pos = (pos + 1) % _data.Length;
                if (pos == stopConditional) throw new KeyNotFoundException();
            }

            throw new KeyNotFoundException();
        }
    }

    record KeyValuePair<TKey, TValue>(TKey key, TValue value);
}
