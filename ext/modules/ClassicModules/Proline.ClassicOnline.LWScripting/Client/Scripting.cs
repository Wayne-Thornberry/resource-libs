using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting
{
    public static partial class MScriptingAPI
    {
        public static int StartNewScript(string scriptName, params object[] args)
        {
            try
            { 
                var sm = LWScriptManager.GetInstance();
                if (sm.DoesScriptExist(scriptName))
                {
                    var type = sm.GetScriptType(scriptName);
                    if (type == null)
                        return -1;
                    var instance = sm.CreateScriptInstance(type);
                    if (instance == null)
                        return -1;
                    var id = sm.StartScriptTask(instance, args);
                    return id.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return -1;
        }

        public static int GetInstanceCountOfScript(string scriptName)
        {
            try
            { 
                var sm = LWScriptManager.GetInstance();
                if (sm.DoesScriptExist(scriptName))
                {
                    return sm.GetScriptInstanceCount(scriptName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            } 
            return 0;
        }

        public static void MarkScriptAsNoLongerNeeded()
        {
            try
            {
                var sm = LWScriptManager.GetInstance();
                var mth = new StackTrace().GetFrame(1).GetMethod();
                var cls = mth.ReflectedType.Name;
                Console.WriteLine(String.Format("{0} Requested all scripts to be stopped", cls));
                if (sm.DoesScriptExist(cls))
                {
                    Console.WriteLine(String.Format("{0} Scripts exist", cls));
                    sm.StopScriptTask(cls);

                }
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.ToString());
            } 
        }
    }
}
