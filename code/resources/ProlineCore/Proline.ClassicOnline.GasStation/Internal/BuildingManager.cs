using Proline.ClassicOnline.MWord;
using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWorld.Internal
{
    internal class BuildingManager
    {
        private static BuildingManager _instance;
        private Dictionary<string, BuildingMetadata> _interiors;

        public BuildingManager()
        {
            _interiors = new Dictionary<string, BuildingMetadata>();
        }

        internal void Register(string interiorName, BuildingMetadata catalouge)
        {
            if (!_interiors.ContainsKey(interiorName))
                _interiors.Add(interiorName, catalouge);
        }

        internal BuildingMetadata GetBuilding(string interiorName)
        { 
            if (_interiors.ContainsKey(interiorName))
                return _interiors[interiorName];
            return null;
        }

        internal static BuildingManager GetInstance()
        {
            if (_instance == null)
                _instance = new BuildingManager();
            return _instance;
        }
    }
}