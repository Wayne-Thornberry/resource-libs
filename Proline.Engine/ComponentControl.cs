using Proline.Engine.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal static class ComponentControl
    {
        internal static void StartAllComponents()
        {
            if (!EngineStatus.IsComponentsInitialized) throw new Exception("Cannot start components, engine not initilized");
            var cm = ComponentManager.GetInstance();
            foreach (var component in cm.GetComponents())
            {
                StartComponent(component);
            }

        }

        internal static void StopAllComponents()
        {
            if (!EngineStatus.IsComponentsInitialized) throw new Exception("Cannot stop components, engine not initilized");
            var cm = ComponentManager.GetInstance();
            foreach (var component in cm.GetComponents())
            {
                StopComponent(component);
            }
        }


        internal static void StartComponent(EngineComponent component)
        {
            try
            {
                component.Start();
                var em = ExtensionManager.GetInstance();
                var extensions = em.GetExtensions();
                foreach (var extension in extensions)
                {
                    extension.OnComponentStarted(component.Name);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static void StopComponent(EngineComponent component)
        {
            try
            {
                component.Stop();
                var em = ExtensionManager.GetInstance();
                var extensions = em.GetExtensions();
                foreach (var extension in extensions)
                {
                    extension.OnComponentStopped(component.Name);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static void StartComponent(string componentName)
        {
            if (!EngineStatus.IsComponentsInitialized) throw new Exception("Cannot start component, engine not initilized");
            try
            {
                var cm = ComponentManager.GetInstance();
                var component = cm.GetComponent(componentName);
                StartComponent(component);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception("Unable to start component, component not found");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal static void StopComponent(string componentName)
        {
            if (!EngineStatus.IsComponentsInitialized) throw new Exception("Cannot stop component, engine not initilized");
            try
            {
                var cm = ComponentManager.GetInstance();
                var component = cm.GetComponent(componentName);
                StopComponent(component);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception("Unable to stop component, component not found");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
