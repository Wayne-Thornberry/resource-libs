using Proline.ClassicOnline.MWord;
using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWorld.Internal
{
    internal class PropertyManager
    {
        private static PropertyManager _instance;
        private Dictionary<string, IPropertyBuilding> _properties;

        public PropertyManager()
        {
            _properties = new Dictionary<string, IPropertyBuilding>();
        }

        internal void Register(string propertyName, IPropertyBuilding catalouge)
        {
            if (!_properties.ContainsKey(propertyName))
                _properties.Add(propertyName, catalouge);
        }

        internal IPropertyBuilding GetProperty(string propertyName)
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