using System;
using CitizenFX.Core.Native;

namespace Proline.CFXExtended.Core
{
    internal interface IStat
    {
        string Name { get; }
        string Value { get; }
        uint Hash { get; }
    }

    public static class MPStat
    {

        public static MPStat<T> GetStat<T>(string key)
        {
            var hash = (uint)API.GetHashKey(key);
            if (!IsValidStat(hash)) return null;
            return new MPStat<T>(key);
        }
        internal static bool IsValidStat(uint hash)
        {
            return hash != 0;
        }
        public static T GetStatValue<T>(uint hash)
        {
            if (!MPStat.IsValidStat(hash)) return default;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    var intValue = 0;
                    if (API.StatGetInt(hash, ref intValue, -1))
                        return (T)Convert.ChangeType(intValue, typeof(T));
                    break;
                case TypeCode.Double:
                    var floatValue = 0.0f;
                    if (API.StatGetFloat(hash, ref floatValue, -1))
                        return (T)Convert.ChangeType((double)floatValue, typeof(T));
                    break;
                case TypeCode.Boolean:
                    var boolValue = false;
                    if (API.StatGetBool(hash, ref boolValue, -1))
                        return (T)Convert.ChangeType(boolValue, typeof(T));
                    break;
                case TypeCode.String: return (T)Convert.ChangeType(API.StatGetString(hash, -1), typeof(T));
                default: return default;
            }

            return default;
        } 
        public static void SetStatValue<T>(uint hash, T value)
        {
            if (!MPStat.IsValidStat(hash)) return;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    API.StatSetInt(hash, Convert.ToInt32(value), false);
                    break;
                case TypeCode.Double:
                    API.StatSetFloat(hash, (float)Convert.ToDouble(value), false);
                    break;
                case TypeCode.Boolean:
                    API.StatSetBool(hash, Convert.ToBoolean(value), false);
                    break;
                case TypeCode.String:
                    API.StatSetString(hash, Convert.ToString(value), false);
                    break;
                default: return;
            }
        }
    }

    public class MPStat<T> : IStat
    {
        private string _name;
        private uint _hash;
        private T _value;

        public MPStat(string statName)
        {
            _name = statName;
            _hash = GetHash();
            _value = GetValue();
        }

        public string Name => _name;
        public string Value => _value.ToString();
        public uint Hash => _hash;

        private uint GetHash()
        {
            return (uint)API.GetHashKey(_name);
        }

        public T GetValue()
        {
            return MPStat.GetStatValue<T>(_hash);
        }

        public void SetValue(T value)
        {
            MPStat.SetStatValue(_hash, value);
        }

        public StatAttributes GetAttributes()
        {
            return new StatAttributes();
        } 
        
    }

    public class StatAttributes
    {
        public int SaveCategory { get; set; }
        public bool ServerAuthoritative { get; set; }
        public long Min { get; set; }
        public long Max { get; set; }
        public bool CharacterStat { get; set; }
        public bool Profile { get; set; }
        public bool IsVehicle { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
    }
}