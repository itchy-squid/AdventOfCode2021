using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.DataStructures
{
    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue> 
        where TKey : struct
        where TValue : struct
    {
        public new TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out TValue val))
                {
                    val = default;
                    Add(key, val);
                }
                return val;
            }
            set { base[key] = value; }
        }
    }
}
