using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.ToolKit.Extensions
{
    public static class DictionaryExtessions
    {
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key)
        {
            TValue o;
            dic.TryGetValue(key, out o);
            return o;
        }
    }
}
