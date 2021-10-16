using Proline.Common.Hashing;
using System;
using System.Reflection;

namespace Proline.Resource.Component.Framework
{
    public class ComponentAPI
    {
        private long _hash;
        private object _owner;
        private MethodInfo _item;
        private bool _debugOnly;
        private int _type;

        public ComponentAPI(object owner, MethodInfo method)
        {
            _owner = owner;
            _item = method;
        }

        public long GetAPIMethodHash()
        { 
            if (_hash == 0)
                _hash = GenerateHash();
            return _hash;
        } 

        private long GenerateHash()
        {
            var h = _item.Name;
            foreach (var item in _item.GetParameters())
            {
                h += item.ParameterType.Name;
            }
            h += _item.ReturnType.Name;
            return Joat.GetHash(h);
        }


        public object Invoke(params object[] args)
        { 
            if (_debugOnly) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1)
                throw new Exception("Cannot invoke a server method while as a client non async");
            return _item.Invoke(_owner, args);
        } 
    }
}