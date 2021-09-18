using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proline.Engine.Data;
using Proline.Engine.Debugging;
using Proline.Engine.Internals;
using Proline.Engine.Networking;
using Proline.Engine.Scripting;

namespace Proline.Engine.Componentry
{
    public abstract class ClientComponent : EngineComponent
    { 
        protected override async Task Push()
        {
            if (IsClient)
            {
                var client = new NetClient();
                if (HasChanged)
                {
                    var obj = new object[SyncedProperties.Count];
                    for (int i = 0; i < obj.Length; i++)
                    {
                        obj[i] = SyncedProperties[i].GetValue(this);
                    }
                    var data = JsonConvert.SerializeObject(obj);
                    await client.ExecuteEngineMethodServer(EngineConstraints.PushHandler);
                }
            }
        }

        protected override async Task Pull()
        {
            if (IsOutOfSync)
            {
                if (IsClient)
                {
                    var client = new NetClient();
                    var data = await client.ExecuteEngineMethodServer<string>(EngineConstraints.PullHandler);
                    var objs = JsonConvert.DeserializeObject<object[]>(data);
                    for (int i = 0; i < objs.Length; i++)
                    {
                        var type = SyncedProperties[i].GetType();
                        object value = null;
                        if (type.IsClass)
                        {
                            value = JsonConvert.DeserializeObject(SyncedProperties[i].ToString(), type);
                        }
                        else
                        {
                            value = objs[i];
                        }
                        SyncedProperties[i].SetValue(this, value);
                    }
                }
            }
        }
    }
}
