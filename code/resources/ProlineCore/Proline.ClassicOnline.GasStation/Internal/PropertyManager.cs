using Proline.ClassicOnline.MWord;
using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWorld.Internal
{
    internal class PropertyManager
    {
        private static PropertyManager _instance;
        private Dictionary<string, PropertyMetadata> _properties;

        public PropertyManager()
        {
            _properties = new Dictionary<string, PropertyMetadata>();
        }

        internal void Register(string propertyName, PropertyMetadata catalouge)
        {
            if (!_properties.ContainsKey(propertyName))
                _properties.Add(propertyName, catalouge);
        }

        internal PropertyMetadata GetProperty(string propertyName)
        { 
            if (_properties.ContainsKey(propertyName))
                return _properties[propertyName];
            return null;
        }

        internal static PropertyManager GetInstance()
        {
            if (_instance == null)
                _instance = new PropertyManager();
            return _instance;
        }
    }
}