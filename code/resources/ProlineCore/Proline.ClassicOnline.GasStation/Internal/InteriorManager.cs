using Proline.ClassicOnline.MWord;
using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWorld.Internal
{
    internal class InteriorManager
    {
        private static InteriorManager _instance;
        private Dictionary<string, Interior> _interiors;

        public InteriorManager()
        {
            _interiors = new Dictionary<string, Interior>();
        }

        internal void Register(string interiorName, Interior catalouge)
        {
            if (!_interiors.ContainsKey(interiorName))
                _interiors.Add(interiorName, catalouge);
        }

        internal Interior GetInterior(string interiorName)
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