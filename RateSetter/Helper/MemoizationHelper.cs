using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateSetter.Helper
{
    public class MemoizationHelper
    {
        private readonly Dictionary<string, bool> _cache = new Dictionary<string, bool>();

        public bool GetOrAdd(string key, Func<bool> valueFunc)
        {
            if (_cache.TryGetValue(key, out bool value))
            {
                return value;
            }

            value = valueFunc();
            _cache[key] = value;
            return value;
        }
    }
}
