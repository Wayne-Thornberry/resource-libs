using Proline.ClassicOnline.MBrain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.Entity
{
    internal class ScriptObjectManager
    {
        private static ScriptObjectManager _instance;
        private Dictionary<int, List<ScriptObjectData>> _scriptObjData;
        private Dictionary<int, ScriptObject> _processScriptObjs;

        private ScriptObjectManager()
        {
            _scriptObjData = new Dictionary<int, List<ScriptObjectData>>();
            _processScriptObjs = new Dictionary<int, ScriptObject>();
        }

        public static ScriptObjectManager GetInstance()
        {
            if (_instance == null)
                _instance = new ScriptObjectManager();
            return _instance;
        }

        internal bool ContainsSO(int handle)
        {
            return _processScriptObjs.ContainsKey(handle);
        }

        internal IEnumerable<ScriptObject> GetValues()
        {
            return _processScriptObjs.Values;
        }

        internal void AddSO(int handle, ScriptObject scriptObject)
        {
            _processScriptObjs.Add(handle, scriptObject);
        }

        internal bool ContainsKey(int hash)
        {
            return _scriptObjData.ContainsKey(hash);
        }

        internal void Add(int hash, List<ScriptObjectData> scriptObjectDatas)
        {
            _scriptObjData.Add(hash, scriptObjectDatas);
        }

        internal void Remove(int handle)
        {
            _processScriptObjs.Remove(handle);
        }

        internal List<ScriptObjectData> Get(int hash)
        {
            return _scriptObjData[hash];
        }
    }
}
