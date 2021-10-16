using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Tools.APIGenerator
{
    internal class Config
    {
        public string Assembly { get; set; }
        public string[] APIClasses { get; set; }
    }

    internal class APINative
    {
        public long NativeId { get; set; }
        public string NativeName { get; set; }
        public bool HasReturnType { get; set; }
        public string NativeReturnType { get; set; }
        public string NativeHash { get; set; }
        public int NativeArgCount { get; set; }
        public string NativeArgs { get; set; }
        public int NativeType { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var configJson = File.ReadAllText("Config.json");
            var confgi = JsonConvert.DeserializeObject<Config>(configJson);
            var assembly = Assembly.Load(confgi.Assembly);

            var methods = typeof(API).GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var list = new List<string>();
            var methodInfos = new List<APINative>();
            var data = "";

            var names = Enum.GetNames(typeof(Hash));
            var values = Enum.GetValues(typeof(Hash));
            var dic = new Dictionary<string, ulong>();
            var dic2 = new Dictionary<string, string>();

            foreach (Hash foo in Enum.GetValues(typeof(Hash)))
            {
                string result = foo.ToString();
                string resultX = "";
                string[] funcsplit = foo.ToString().Split('_');
                ////LogDebug(header, foo.ToString() + " " + (ulong)foo);
                foreach (string word in funcsplit)
                {
                    if (word.Length > 0)
                        resultX += word.ToLower().Insert(0, word.Substring(0, 1).ToUpper()).Remove(1, 1);
                }
                //LogDebug(header, result + " " + (ulong)foo);
                if (!dic.ContainsKey(result))
                {
                    dic.Add(result, (ulong)foo);
                    if(!dic2.ContainsKey(resultX))
                        dic2.Add(resultX, result);
                }
            }

            foreach (var item in methods)
            {
                var h = "public static " + item.ReturnType.Name + $" {item.Name}({GetParametersFormat(item)})\n" +
                    "{\n" +
                    $"{GetBodyFormat(dic[dic2[item.Name]], item)}" +
                    "}\n";
                Console.WriteLine(h);
            }

            var l = new List<string>();
            foreach (var item in confgi.APIClasses)
            {
                var type = assembly.GetType(item);
                var methods2 = type.GetMethods();//.Where(m => m.GetCustomAttributes(lookFor, false).Length > 0); 
                var filer = methods2.Where(m => m.GetCustomAttributes(typeof(ComponentAPIAttribute), false).Length > 0)
                 .ToArray();

                foreach (var x in filer)
                {
                    var enumName = x.Name + ((l.Where(e => e.Contains(x.Name)).Count() > 0) ? "_" + l.Where(e => e.Contains(x.Name)).Count() : "");
                    var hash = GenerateHash(GenerateSignature(x));
                    l.Add(enumName + "=" + hash);
                }
            }

            foreach (var item in l)
            {
                Console.WriteLine( item);
            }

            //GenerateHash(GenerateSignature());



            var enums = "namespace Proline.Tools.APIGenerator\n{\n";
            enums += "\tpublic enum Hash : ulong\n\t{\n";
            foreach (var item in dic)
            {
                enums += string.Format("\t\t{0} = {1},\n", item.Key, item.Value);
            }
            enums += "\n\t}\n}";
            File.WriteAllText("Hash.cs",enums);
            Console.WriteLine(enums);
            Console.ReadKey();
        }

        private static string GetBodyFormat(ulong v, MethodInfo item)
        {
            var p = item.GetParameters();
            var h = "";
            for (int i = 0; i < p.Length; i++)
            {
                var x = p[i];
                h += $"{x.Name}" + (i++ == p.Length ? "" : ",");
            }
            return h;
            var d = "var objs = new object[]{" + h +"};\n";
            d += "NativeAPI.CallNativeAPI" + (item.ReturnType == typeof(void) ? "" : $"<{item.ReturnType}>") + $"({v},objs);";

        }

        private static string GetParametersFormat(MethodInfo item)
        {
            var p = item.GetParameters();
            var h = "";
            for (int i = 0; i < p.Length; i++)
            {
                var x = p[i];
                h += $"{x.ParameterType.Name} {x.Name}" + (i++ == p.Length ? "" : ",");
            }
            return h;
        }

        public static string GenerateSignature(MethodInfo info)
        {
            var h = info.Name;
            foreach (var item in info.GetParameters())
            {
                h += item.ParameterType.Name;
            }
            h += info.ReturnType.Name;
            return h;
        }


        internal static int GenerateHash(string key)
        {
            var hash = 0;
            int x = key.Length;
            var chars = key.ToCharArray();

            for (int i = x - 1; i >= 0; i--)
            {
                hash += chars[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }
    }
}
