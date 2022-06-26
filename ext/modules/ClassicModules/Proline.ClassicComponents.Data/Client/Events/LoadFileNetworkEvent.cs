using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData
{
    public partial class LoadFileNetworkEvent : ExtendedEvent
    {
        protected override object OnEventTriggered(bool isCallbackExecution, params object[] args)
        {
            if (isCallbackExecution)
            { 
                // Needs to support callback and calling code
                var obj = args.ToString();
                if (string.IsNullOrEmpty(obj)) return null;
                var instance = SaveFileManager.GetInstance();
                Console.WriteLine("data got " + obj);
                instance.PutFileIntoMemory(obj); 
            }
            else
            { 

            }
            return null;
        }
    }
}
