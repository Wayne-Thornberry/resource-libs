namespace Proline.Resource.Common.CFX
{
    public interface IFXConsole : IFXWrapper
    {
        void Write(object data);
        void WriteLine(object data);
    }
}