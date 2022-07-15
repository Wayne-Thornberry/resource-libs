using CitizenFX.Core;
using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.GCharacter.Data
{
    public class PlayerStats : Dictionary<string, object>
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

    public class PlayerPersonalVehicle : Entity
    {
        public PlayerPersonalVehicle(int handle) : base(handle)
        {

        }
    }

    public class PlayerCharacter : Entity
    {
        public long BankBalance { get; set; }
        public long WalletBalance { get; set; }

        public PlayerCharacter(int handle) : base(handle)
        {
        }

        public PlayerPersonalVehicle PersonalVehicle { get; set;}
        public PlayerStats Stats { get; set; }
    }
}