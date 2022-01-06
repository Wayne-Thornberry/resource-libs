using System;
using System.Collections.Generic;
using System.Reflection;

namespace Proline.Resource.Component.Framework
{
    public class PropertyManager
    {
        private List<PropertyInfo> _properties;

        internal PropertyManager()
        {
            _properties = new List<PropertyInfo>();
        }

        public void ManageProperties(IEnumerable<PropertyInfo> properties)
        {
            _properties.AddRange(properties);
        }

        internal void ManageProperty(PropertyInfo item)
        {
            _properties.Add(item);
        }
    }
}