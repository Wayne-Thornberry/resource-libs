using System;

namespace Proline.Resource.Common.CFX
{
    public interface IFXEventHandler : IFXWrapper
    {
        void RegisterEventMethod(string eventName, Delegate x);
        void UnregisterEventMethod(string eventName);
    }
}