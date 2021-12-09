using System.Collections;

namespace AdventOfCode.Solutions.DataStructures
{
    public interface IDefaultDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : struct
    {
        TValue this[TKey key] {get; set;}

        IEnumerable<TValue> Values {get; }
    }

    public class DefaultDictionary<TKey, TValue> : IDefaultDictionary<TKey, TValue>
        where TKey : struct
    {
        private readonly Func<TValue> _defaultSelector;
        private readonly Dictionary<TKey, TValue> _dictionary;

        public DefaultDictionary(Func<TValue> defaultSelector)
        {
            _defaultSelector = defaultSelector;
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public TValue this[TKey key]
        {
            get
            {
                return _dictionary.TryGetValue(key, out TValue? val) ? val : _defaultSelector();
            }
            set
            {
                if (!_dictionary.ContainsKey(key))
                {
                    _dictionary.Add(key, _defaultSelector());
                }
                _dictionary[key] = value; 
            }
        }

        IEnumerable<TValue> IDefaultDictionary<TKey, TValue>.Values 
        { 
            get => _dictionary.Values; 
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
    }
}
