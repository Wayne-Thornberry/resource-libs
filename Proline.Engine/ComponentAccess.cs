using Proline.Engine.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class ComponentAccess
    {
        internal static object ExecuteComponentAPI(string componentName, string control, object[] args)
        {
            var cm = ComponentManager.GetInstance();
            var component = cm.GetComponent(componentName);
            return component.ExecuteControl(control, args);
        }

        internal static object ExecuteComponentControl(string componentName, string control, object[] args)
        {
            var cm = ComponentManager.GetInstance();
            var component = cm.GetComponent(componentName);
            return component.ExecuteControl(control, args);
        }

        internal static void ExecuteComponentCommand(string componentName, string command, object[] args)
        {
            var cm = ComponentManager.GetInstance();
            var component = cm.GetComponent(componentName);
            component.ExecuteCommand(command, args);
        }
    }
}
