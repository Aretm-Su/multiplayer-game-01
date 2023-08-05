using System.Collections.Generic;

namespace Assets.Scripts.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool HasNotKey<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
        {
            return source.ContainsKey(key) == false;
        }
    }
}