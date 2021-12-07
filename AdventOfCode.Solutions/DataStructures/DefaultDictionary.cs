using System.Collections;

namespace AdventOfCode.Solutions.DataStructures
{
    public interface IDefaultDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : struct
        where TValue : struct
    {
        TValue this[TKey key] {get; set;}

        IEnumerable<TValue> Values {get; }
    }

    public class DefaultDictionary<TKey, TValue> : IDefaultDictionary<TKey, TValue>
        where TKey : struct
        where TValue : struct
    {
        private readonly Dictionary<TKey, TValue> _dictionary;

        public DefaultDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        TValue IDefaultDictionary<TKey,TValue>.this[TKey key]
        {
            get
            {
                if (!_dictionary.TryGetValue(key, out TValue val))
                {
                    val = default;
                    _dictionary.Add(key, val);
                }
                return val;
            }
            set
            {
                if (!_dictionary.ContainsKey(key))
                {
                    _dictionary.Add(key, default);
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
