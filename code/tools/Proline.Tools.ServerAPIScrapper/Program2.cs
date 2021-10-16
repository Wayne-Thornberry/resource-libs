using CitizenFX.Core.Native;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proline.ProlineEditor.APIScrapper
{

    class Program2
    {
        static void Main2(string[] args)
        {
            var methods = typeof(API).GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var list = new List<string>();
            var methodInfos = new List<APIMethodInfo>();
            var data = "";

            foreach (var item in methods)
            {
                if (item.Name.Equals("ToString") || item.Name.Equals("Equals") || item.Name.Equals("GetType") || item.Name.Equals("GetHashCode")  )
                    continue; 

                var p = "";
                var d = "";
                var x = "";
                var ps = item.GetParameters();
                int i = 0;
                var argTypes = new List<string>();
                foreach (var param in ps)
                {
                    p += param.ParameterType.Name + " " +((i + 1 < ps.Length) ? $"a{i}, " : $"a{i} ");
                    d += ((i + 1 < ps.Length) ? $"a{i}, " : $"a{i}");
                    x += (param.ParameterType.Name.Contains('&') ? "ref " + param.ParameterType.Name.Replace("&", "") : param.ParameterType.Name) + " " + ((i + 1 < ps.Length) ? $"a{i}, " : $"a{i}");
                    argTypes.Add(param.ParameterType.Name);
                    i++;
                }

                //Console.WriteLine("{0},{1},{2}", item.Name, p, item.ReturnType.Name);

                methodInfos.Add(new APIMethodInfo()
                {
                    MethodName = item.Name,
                    MethodArgTypes = argTypes.ToArray(),
                    ReturnTypeName = item.ReturnType.Name,
                });

                data += @"
        public " + item.ReturnType.Name + " " + item.Name + $"({x})" + @"
        {
            "+ (item.ReturnType.Name.ToLower() == "void" ? "MakeCall" : "return MakeCall<" + item.ReturnType.Name + ">")+"(\"" + item.Name + "\"" + (!string.IsNullOrEmpty(d) ? "," + d : " ") + @"); 
        }
";

                if (!list.Contains(item.ReturnType.Name))
                    list.Add(item.ReturnType.Name);
            }
            //Console.WriteLine();
            var csFileData = @"
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Proline.ProlineEditor.EngineProxy
{ 
    public class NativeAPI : APIProxy
    {
            " + data + @"
    } 
}";

            foreach (var item in list)
            {
               // Console.WriteLine(item);
            }

            Console.WriteLine("Done");
            File.WriteAllText("nativemethodinfo.json", JsonConvert.SerializeObject(methodInfos, Formatting.Indented));
            csFileData = Regex.Replace(csFileData, "String", "string")
                .Replace("Void", "void")
                .Replace("Int32", "int")
                .Replace("Int64", "long")
                .Replace("Boolean", "bool")
                .Replace("Single", "float")
                .Replace("Object", "object")
                .Replace("Uint", "uint");
            File.WriteAllText("NativeAPI.cs", csFileData);
            
            Console.ReadKey();
        }
    }
}
