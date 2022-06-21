using Proline.DBAccess.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit.Helpers
{

    public enum IdentifierType
    {
        STEAM,
        IP,
        SOCIAL,
        DISCORD,
        EPIC,
        HWID,
    }

    public static class IdentityHelper
    {
        public static string CreatePlayerIdentity(string identity = "steam:00012371704", int identityType = 0)
        {
            return identity;
        }

        public static string[] CreatePlayerIdentifiers(string[] identitifers, int[] identityType)
        {
            if (identitifers.Length != identityType.Length)
                return new string[0];
            var identities = new string[identitifers.Length];
            for (int i = 0; i < identities.Length; i++)
            {
                identities[i] = CreatePlayerIdentity(identitifers[i], identityType[i]);
            }
            return identities;
        }

        internal static string GenerateIdentifier(int type)
        {
            var t = (IdentifierType)type;
            if (t == IdentifierType.IP)
            {
                var data = new byte[4];
                new Random().NextBytes(data);
                IPAddress ip = new IPAddress(data);
                return ip.ToString();
            }
            else if (t == IdentifierType.STEAM)
            {
                return GetRandomHexNumber(25);
            }
            else if (t == IdentifierType.SOCIAL)
            {
                return Util.GenerateRandomString(25);
            }
            else if (t == IdentifierType.DISCORD)
            {
                var rng = new Random();
                var x = "";
                for (int i = 0; i < rng.Next(0, 120); i++)
                {
                    x += rng.Next(0, 9);
                };
                return x;
            }
            else
            {
                return Util.GenerateRandomString(25);
            }
        }

        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            var rng = new Random();
            rng.NextBytes(buffer);
            string result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + rng.Next(16).ToString("X");
        }
    }
}
