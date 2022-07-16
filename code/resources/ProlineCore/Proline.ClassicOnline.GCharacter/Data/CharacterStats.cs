using System.Collections.Generic;

namespace Proline.ClassicOnline.GCharacter.Data
{
    public class CharacterStats : Dictionary<string, object>
    {
        public void SetStat(string key, object val)
        {
            if (this.ContainsKey(key))
                this[key] = val;
            this.Add(key, val);
        }

        public object GetStat(string key)
        {
            if (this.ContainsKey(key))
                return this[key];
            return default;
        }

        public T GetStat<T>(string key)
        {
            if (this.ContainsKey(key))
                return (T) this[key];
            return default;
        }
    }
}