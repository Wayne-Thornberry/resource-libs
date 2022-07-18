using System;

namespace Proline.ClassicOnline.Resource
{
    [Serializable]
    internal class ScriptDoesNotExistException : Exception
    {
        public ScriptDoesNotExistException()
        {
        }

        public ScriptDoesNotExistException(string message) : base(message)
        {
        }

        public ScriptDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}