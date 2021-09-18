using Proline.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.Engine.Internals;

namespace Proline.Engine
{
    public static class Component
    {
        public static void StartAllComponents()
        {
            if (!EngineStatus.IsComponentsInitialized) throw new Exception("Cannot start components, engine not initilized");
            var cm = InternalManager.GetInstance();
            foreach (var component in cm.GetComponents())
            {
                component.Start();
            }

        }

        public static void StopAllComponents()
        {
            if (!EngineStatus.IsComponentsInitialized) throw new Exception("Cannot stop components, engine not initilized");
            var cm = InternalManager.GetInstance();
            foreach (var component in cm.GetComponents())
            {
                component.Stop();
            }
        }

        public static void StartComponent(string componentName)
        {

        }

        public static void StopComponent(string componentName)
        {

        }
    }
}
