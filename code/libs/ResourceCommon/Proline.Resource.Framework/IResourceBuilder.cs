using System;

namespace Proline.Resource.Framework
{
    public interface IResourceBuilder
    {
        IHost Build();
    }
}