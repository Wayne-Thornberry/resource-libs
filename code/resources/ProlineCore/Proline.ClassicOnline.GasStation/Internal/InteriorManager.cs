using Proline.ClassicOnline.MWord;
using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWorld.Internal
{
    internal class InteriorManager
    {
        private static InteriorManager _instance;
        private Dictionary<string, PropertyInterior> _interiors;

        public InteriorManager()
        {
            _interiors = new Dictionary<string, PropertyInterior>();
        }

        internal void Register(string interiorName, PropertyInterior catalouge)
        {
            if (!_interiors.ContainsKey(interiorName))
                _interiors.Add(interiorName, catalouge);
        }

        internal PropertyInterior GetInterior(string interiorName)
        { 
            if (_interiors.ContainsKey(interiorName))
                return _interiors[interiorName];
            return null;
        }

        internal static InteriorManager GetInstance()
        {
            if (_instance == null)
                _instance = new InteriorManager();
            return _instance;
        }
    }
}