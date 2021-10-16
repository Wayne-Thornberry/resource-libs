using CitizenFX.Core.Native;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proline.ProlineEditor.APIScrapper
{

    class Program
    {
        static void Main(string[] args)
        {
            var methods = typeof(API).GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var list = new List<string>();
            var methodInfos = new List<APINative>();
            var data = "";

            var names = Enum.GetNames(typeof(Hash));
            var values = Enum.GetValues(typeof(Hash));
            var dic = new Dictionary<string, ulong>();

            foreach (Hash foo in Enum.GetValues(typeof(Hash)))
            {
                string result = null;
                string[] funcsplit = foo.ToString().Split('_');
                //LogDebug(header, foo.ToString() + " " + (ulong)foo);
                foreach (string word in funcsplit)
                {
                    if (word.Length > 0)
                        result += word.ToLower().Insert(0, word.Substring(0, 1).ToUpper()).Remove(1, 1);
                }
                //LogDebug(header, result + " " + (ulong)foo);
                if (!dic.ContainsKey(result))
                    dic.Add(result, (ulong)foo);
            }

            foreach (var item in methods)
            {
                if (item.Name.Equals("ToString") || item.Name.Equals("Equals") || item.Name.Equals("GetType") || item.Name.Equals("GetHashCode")  )
                    continue; 

                var p = "";
                var d = "";
                var x = "";
                var ps = item.GetParameters();
                int i = 0;
                var argTypes = new Dictionary<string, string>();
                foreach (var param in ps)
                {
                    p += param.ParameterType.Name + " " +((i + 1 < ps.Length) ? $"a{i}, " : $"a{i} ");
                    d += ((i + 1 < ps.Length) ? $"a{i}, " : $"a{i}");
                    x += (param.ParameterType.Name.Contains('&') ? "ref " + param.ParameterType.Name.Replace("&", "") : param.ParameterType.Name) + " " + ((i + 1 < ps.Length) ? $"a{i}, " : $"a{i}");
                    argTypes.Add(param.Name, param.ParameterType.Name);
                    i++;
                }

                if (dic.ContainsKey(item.Name))
                { 
                    methodInfos.Add(new APINative()
                    {
                        NativeName = item.Name,
                        NativeReturnType = item.ReturnType.Name,
                        HasReturnType = !item.ReturnType.Name.Equals("void", StringComparison.OrdinalIgnoreCase),
                        NativeHash = dic[item.Name].ToString(),
                        NativeArgCount = argTypes.Count,
                        NativeType = 1,
                        NativeArgs = JsonConvert.SerializeObject(argTypes)
                    });
                }
            };
            doTask(methodInfos);

            Console.ReadKey();
        }

        private static async Task doTask(IEnumerable<APINative> apimethod)
        {
            foreach (var item in apimethod)
            { 
                var httpClient = new HttpClient();
                var data = JsonConvert.SerializeObject(item);
                var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                var re = await httpClient.PostAsync("http://localhost:1068/api/APINatives", stringContent);
                var pla = JsonConvert.DeserializeObject<APINative>(await re.Content.ReadAsStringAsync());
                Console.WriteLine(re.StatusCode);
            }
        }
    }
}
