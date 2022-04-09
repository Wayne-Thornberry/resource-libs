using System;

namespace Proline.ResourceFramework
{
    public interface IResourceBuilder
    {
        IHost Build();
    }
}