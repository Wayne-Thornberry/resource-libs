using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData
{
    public partial class LoadFileNetworkEvent : ExtendedEvent
    {

        public LoadFileNetworkEvent() : base(LOADFILEHANDLER, true)
        {

        }

        protected override void OnEventCallback(params object[] args)
        { 
            var obj = args[0].ToString();
            if (string.IsNullOrEmpty(obj)) return;
            var instance = SaveFileManager.GetInstance();
            Console.WriteLine("data got " + obj);
            instance.PutFileIntoMemory(obj); 
        }
    }
}
