using System.Reflection;

namespace Proline.Engine
{
    internal class APIKeyPair
    {
        private object engineComponent;
        private MethodInfo item;

        public APIKeyPair(object engineComponent, MethodInfo item)
        {
            this.engineComponent = engineComponent;
            this.item = item;
        }

        internal object Invoke(params object[] args)
        {
           return item.Invoke(engineComponent, args);
        }
    }
}