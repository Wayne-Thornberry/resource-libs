
using Proline.Engine;

namespace Proline.Core.Client.Components.CGlobals
{

    public class GlobalsHandler : ComponentHandler
    { 
        //public void SetGlobal(string name, object value, bool localOnly = false)
        //{
        //    var rc = ReturnCode.Success;
        //    CallNetworkAPI("SetGlobal", "" + value);
        //    var gm = GlobalsManager.GetInstance();
        //    rc = (ReturnCode)outrc;
        //    if(rc == ReturnCode.Success)
        //    { 
        //        gm.AddGlobal(name, value, localOnly);
        //        WriteLine("Added Global Value: " + name + " Value: " + value);
        //    }
        //    return rc;
        //}

        //public void SyncGlobals(Header header)
        //{
        //    var rc = ReturnCode.Success;
        //    CallNetworkAPI("SyncGlobals", "", out var outrc);
        //    rc = (ReturnCode)outrc;
        //    return rc;
        //}

        //public void DeleteGlobal(string name)
        //{
        //    var rc = ReturnCode.Success;
        //    CallNetworkAPI("DeleteGlobal", name, out var outrc);
        //    rc = (ReturnCode) outrc;
        //    return rc;
        //}

        //public void ListGlobals(out string[] values)
        //{ 
        //    var rc = ReturnCode.Success;
        //    CallNetworkAPI("ListGlobals", "", out var outrc);
        //    rc = (ReturnCode)outrc;
        //    values = null;
        //    return rc;
        //}
    }
}
