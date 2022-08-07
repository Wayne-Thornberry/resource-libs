using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MWorld.Internal;
using Proline.ClassicOnline.MWord;
using Proline.ClassicOnline.MWorld.Client.Data;

using Proline.Resource.IO;

namespace Proline.ClassicOnline.MWorld.Client.Scripts
{
    public class InitCore 
    {


        public async Task Execute()
        { 
            try
            {
                var apartmentData = ResourceFile.Load("data/world/apt_properties.json"); 
                var pm = PropertyManager.GetInstance();
                var apartmentInteriors = JsonConvert.DeserializeObject<List<PropertyMetadata>>(apartmentData.Load());  
                foreach (var item in apartmentInteriors)
                {
                    pm.Register(item.Id, item);
                }  
            }
            catch (System.Exception e) 
            {
                MDebug.MDebugAPI.LogDebug(e);
            }

        }


    }
}
