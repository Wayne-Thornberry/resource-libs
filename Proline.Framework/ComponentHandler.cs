using System;

namespace Proline.Framework
{
    /// <summary>
    /// an engine component is a class that is the first thing that is created and called before everything else. Provides
    /// Methods for starting, intilization and updating, called and created by basescript.
    /// </summary>
    public abstract class ComponentHandler 
    {
        private string _componentName;

        public ComponentHandler()
        {
            _componentName = this.GetType().Name;
        } 

        /// <summary>
        /// Called before the first tick, but after the constructor
        /// </summary>
        public virtual void OnComponentInitialized()
        {

        }

        /// <summary>
        /// Called on the first tick but before update
        /// </summary>
        public virtual void OnComponentStart()
        {

        }

        public void OnComponentStop()
        {

        }
    }
}