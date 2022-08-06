using System.Collections.Generic;

namespace Proline.ClassicOnline.MWord
{
    internal interface IPropertyBuilding
    {
        List<PropertyEntrance> Entrances { get; set; }
        List<PropertyExitPoint> ExitPoints { get; set; }
        string Id { get; set; }
        string Title { get; set; }
    }
}