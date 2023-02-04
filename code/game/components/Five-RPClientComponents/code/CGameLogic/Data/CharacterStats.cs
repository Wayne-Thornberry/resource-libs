using System.Collections.Generic;

namespace CGameLogic.Data
{
    public class CharacterStats : Dictionary<string, object>
    {
        public void SetStat(string key, object val)
        {
            if (ContainsKey(key))
                this[key] = val;
            Add(key, val);
        }

        public object GetStat(string key)
        {
            if (ContainsKey(key))
                return this[key];
            return default;
        }

        public T GetStat<T>(string key)
        {
            if (ContainsKey(key))
                return (T)this[key];
            return default;
        }
    }
}