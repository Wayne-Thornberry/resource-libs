using System.Configuration;

namespace Proline.ModuleFramework.Core.Config
{
    public class ModuleInstanceCollection : ConfigurationElementCollection
    {
        // Create a property that lets us access an element in the
        // collection with the int index of the element
        public ModuleInstanceElement this[int index]
        {
            get
            {
                // Gets the SageCRMInstanceElement at the specified
                // index in the collection
                return (ModuleInstanceElement)BaseGet(index);
            }
            set
            {
                // Check if a SageCRMInstanceElement exists at the
                // specified index and delete it if it does
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                // Add the new SageCRMInstanceElement at the specified
                // index
                BaseAdd(index, value);
            }
        }

        // Create a property that lets us access an element in the
        // colleciton with the name of the element
        public new ModuleInstanceElement this[string key]
        {
            get
            {
                // Gets the SageCRMInstanceElement where the name
                // matches the string key specified
                return (ModuleInstanceElement)BaseGet(key);
            }
            set
            {
                // Checks if a SageCRMInstanceElement exists with
                // the specified name and deletes it if it does
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));

                // Adds the new SageCRMInstanceElement
                BaseAdd(value);
            }
        }

        // Method that must be overriden to create a new element
        // that can be stored in the collection
        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleInstanceElement();
        }

        // Method that must be overriden to get the key of a
        // specified element
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleInstanceElement)element).Name;
        }
    }
}
