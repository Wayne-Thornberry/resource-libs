using System;
using System.Runtime.Serialization;

namespace Proline.EngineFramework
{
    [Serializable]
    internal class ScriptTerminatedException : Exception
    {
        public ScriptTerminatedException()
        {
        }

        public ScriptTerminatedException(string message) : base(message)
        {
        }

        public ScriptTerminatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ScriptTerminatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}