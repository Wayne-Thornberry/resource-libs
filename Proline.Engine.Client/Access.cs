using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Script
{
    public partial class EngineScript : BaseScript, IScriptSource
    {
        public void AddTick(Func<Task> task)
        {
            Tick += task;
        }
        public object GetGlobal(string key)
        {
            return GlobalState.Get(key); 
        }

        public void SetGlobal(string key, object data, bool replicated)
        {
            GlobalState.Set(key, data, replicated);
        }


        public object CallFunction<T>(ulong hash, params object[] inputParameters)
        {
            if (inputParameters.Length == 0)
            {
                return Function.Call<T>((Hash)hash);
            }
            else
            {
                List<InputArgument> list = new List<InputArgument>();
                for (int i = 0; i < inputParameters.Length; i++)
                {
                    var type = inputParameters[i].GetType();
                    var value = inputParameters[i];
                    InputArgument x = null;
                    switch (type.Name)
                    {
                        case "Int32":
                            x = int.Parse(value.ToString());
                            break;
                        case "String":
                            x = (string)value;
                            break;
                        case "Single":
                            x = float.Parse(value.ToString());
                            break;
                        case "Int64":
                            x = long.Parse(value.ToString());
                            break;
                        case "UInt64":
                            x = ulong.Parse(value.ToString());
                            break;
                        case "UInt32":
                            x = uint.Parse(value.ToString());
                            break;
                        case "UInt16":
                            x = ushort.Parse(value.ToString());
                            break;
                        case "Double":
                            x = double.Parse(value.ToString());
                            break;
                        case "Boolean":
                            x = bool.Parse(value.ToString());
                            break;
                    }
                    if (x != null)
                        list.Add(x);
                }
                return Function.Call<T>((Hash)hash, list.ToArray());
            }
        }

        public void CallFunction(ulong hash, params object[] inputParameters)
        {
            if(inputParameters.Length == 0)
            {
                Function.Call((Hash)hash); 
            }
            else
            {
                List<InputArgument> list = new List<InputArgument>();
                for (int i = 0; i < inputParameters.Length; i++)
                {
                    var type = inputParameters[i].GetType();
                    var value = inputParameters[i];
                    InputArgument x = null;
                    switch (type.Name)
                    {
                        case "Int32":
                            x = int.Parse(value.ToString());
                            break;
                        case "String":
                            x = (string)value;
                            break;
                        case "Single":
                            x = float.Parse(value.ToString());
                            break;
                        case "Int64":
                            x = long.Parse(value.ToString());
                            break;
                        case "UInt64":
                            x = ulong.Parse(value.ToString());
                            break;
                        case "UInt32":
                            x = uint.Parse(value.ToString());
                            break;
                        case "UInt16":
                            x = ushort.Parse(value.ToString());
                            break;
                        case "Double":
                            x = double.Parse(value.ToString());
                            break;
                        case "Boolean":
                            x = bool.Parse(value.ToString());
                            break;
                    }
                    if (x != null)
                        list.Add(x);
                }
                Function.Call((Hash)hash, list.ToArray());
            }
        }

        //public void CallNativeAPI(string apiName,bool hasReturn, string returnType, params object[] inputParameters)
        //{
        //    try
        //    {
        //        var names = Enum.GetNames(typeof(Hash));
        //        var values = Enum.GetValues(typeof(Hash));
        //        var dic = new Dictionary<string, ulong>();

        //        foreach (Hash foo in Enum.GetValues(typeof(Hash)))
        //        {
        //            string result = null;
        //            string[] funcsplit = foo.ToString().Split('_');
        //            //LogDebug(foo.ToString() + " " + (ulong)foo);
        //            foreach (string word in funcsplit)
        //            {
        //                if (word.Length > 0)
        //                    result += word.ToLower().Insert(0, word.Substring(0, 1).ToUpper()).Remove(1, 1);
        //            }
        //            //LogDebug(result + " " + (ulong)foo);
        //            if (!dic.ContainsKey(result))
        //                dic.Add(result, (ulong)foo);
        //        }

        //        var hash = (Hash)dic[apiName]; 
        //        Debugger.LogDebug(hash);
        //        object returnData = null;

        //        List<InputArgument> list = null;
        //        if (inputParameters.Length > 0)
        //        {
        //            list = new List<InputArgument>();
        //            for (int i = 0; i < inputParameters.Length; i++)
        //            {
        //                var type = inputParameters[i].GetType();
        //                var value = inputParameters[i];
        //                Debugger.LogDebug(type);
        //                Debugger.LogDebug(value);
        //                InputArgument x = null;
        //                switch (type.Name)
        //                {
        //                    case "Int32":
        //                        x = int.Parse(value.ToString());
        //                        break;
        //                    case "String":
        //                        x = (string)value;
        //                        break;
        //                    case "Single":
        //                        x = float.Parse(value.ToString());
        //                        break;
        //                    case "Int64":
        //                        x = long.Parse(value.ToString());
        //                        break;
        //                    case "UInt64":
        //                        x = ulong.Parse(value.ToString());
        //                        break;
        //                    case "UInt32":
        //                        x = uint.Parse(value.ToString());
        //                        break;
        //                    case "UInt16":
        //                        x = ushort.Parse(value.ToString());
        //                        break;
        //                    case "Double":
        //                        x = double.Parse(value.ToString());
        //                        break;
        //                    case "Boolean":
        //                        x = bool.Parse(value.ToString());
        //                        break;
        //                }
        //                if (x != null)
        //                    list.Add(x);
        //            }
        //            Debugger.LogDebug("Has args");
        //        }


        //        if (hasReturn)
        //        {
        //            Debugger.LogDebug("Has return");
        //            switch (returnType)
        //            {
        //                case "Boolean":
        //                    returnData = list != null ? Function.Call<bool>(hash, list.ToArray()) :
        //                        Function.Call<bool>(hash);
        //                    Debugger.LogDebug("dsd");
        //                    break;
        //                case "String":
        //                    returnData = list != null ? Function.Call<string>(hash, list.ToArray()) :
        //                        Function.Call<string>(hash);
        //                    Debugger.LogDebug("ddasa");
        //                    break;
        //                case "Int32":
        //                    returnData = list != null ? Function.Call<int>(hash, list.ToArray()) :
        //                        Function.Call<int>(hash);
        //                    Debugger.LogDebug("dsaddsa d");
        //                    break;
        //                case "Int64":
        //                    returnData = list != null ? Function.Call<long>(hash, list.ToArray()) :
        //                        Function.Call<long>(hash);
        //                    Debugger.LogDebug("dsaddsdsadasda d");
        //                    break;
        //                case "Int16":
        //                    returnData = list != null ? Function.Call<short>(hash, list.ToArray()) :
        //                        Function.Call<short>(hash);
        //                    Debugger.LogDebug("dsdasdaaaaaaaaddddddsa d");
        //                    break;
        //                case "UInt64":
        //                    returnData = list != null ? Function.Call<ulong>(hash, list.ToArray()) :
        //                        Function.Call<ulong>(hash);
        //                    break;
        //                case "UInt32":
        //                    returnData = list != null ? Function.Call<uint>(hash, list.ToArray()) :
        //                        Function.Call<uint>(hash);
        //                    Debugger.LogDebug("d d");
        //                    break;
        //                case "UInt16":
        //                    returnData = list != null ? Function.Call<ushort>(hash, list.ToArray()) :
        //                        Function.Call<ushort>(hash);
        //                    Debugger.LogDebug("as d");
        //                    break;
        //                case "Byte":
        //                    returnData = list != null ? Function.Call<byte>(hash, list.ToArray()) :
        //                        Function.Call<byte>(hash);
        //                    Debugger.LogDebug("ewq d");
        //                    break;
        //                case "sbyte":
        //                    returnData = list != null ? Function.Call<sbyte>(hash, list.ToArray()) :
        //                        Function.Call<sbyte>(hash);
        //                    Debugger.LogDebug("rwerw d");
        //                    break;
        //                case "Vector3":
        //                    returnData = list != null ? Function.Call<Vector3>(hash, list.ToArray()) :
        //                        Function.Call<Vector3>(hash);
        //                    Debugger.LogDebug("iiyu d");
        //                    break;
        //                case "Double":
        //                    returnData = list != null ? Function.Call<double>(hash, list.ToArray()) :
        //                        Function.Call<double>(hash);
        //                    Debugger.LogDebug("yui5hfg d");
        //                    break;
        //                default:
        //                    break;
        //            }

        //            //if (methodInvokation.ReturnType.Equals("Vector3"))
        //            //{
        //            //    var x = (Vector3)returnData;
        //            //    var dir = new Dictionary<string, float>();
        //            //    dir.Add("X", x[0]);
        //            //    dir.Add("Y", x[1]);
        //            //    dir.Add("Z", x[2]);
        //            //    returnData = JsonConvert.SerializeObject(dir);
        //            //}

        //            Debugger.LogDebug(returnData);
        //        }
        //        else
        //        {
        //            if (list != null)
        //            {
        //                Function.Call(hash, list.ToArray());
        //                Debugger.LogDebug("aaaaaaaaaaaaaaa");
        //            }
        //            else
        //            {
        //                Function.Call(hash);
        //                Debugger.LogDebug("aaaaadddddddddddddddddddaaaaaaaaaa");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debugger.LogError(e.ToString());
        //        throw;
        //    }
        //    return;

        //}

        public async Task Delay(int ms)
        {
            await BaseScript.Delay(ms);
        }

        public string GetCurrentResourceName()
        {
            return API.GetCurrentResourceName();
        }

        public string LoadResourceFile(string resourceName, string filePath)
        {
            return API.LoadResourceFile(resourceName, filePath);
        }

        public void RemoveTick(Func<Task> task)
        {
            Tick -= task;
        }

        public new void TriggerClientEvent(string eventName, params object[] args)
        {

        }

        public void TriggerClientEvent(int playerId, string eventName, params object[] args)
        {

        }

        public new void TriggerEvent(string eventName, params object[] data)
        {
            BaseScript.TriggerEvent(eventName, data);
        }

        public void TriggerServerEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerServerEvent(eventName, args);
        }

        public void Write(object data)
        {
            Debug.Write(data.ToString());
        }

        public void WriteLine(object data)
        {
            Debug.WriteLine(data.ToString());
        }
    }
}
