using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.DBAccess.Proxies;
using System.Net.Sockets;
using CitizenFX.Core;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData
{
    public partial class LoadFileNetworkEvent : ExtendedEvent
    {
        public LoadFileNetworkEvent() : base(LOADFILEHANDLER)
        {

        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            // Needs to support callback and calling code  
            long arg2 = long.Parse(args[0].ToString());
            return LoadFileAsync(arg2).Result;
        }


        private static async Task<string> LoadFileAsync(long arg2)
        {
            var data = "";
            try
            {
                Console.WriteLine("Load Request Recived " + arg2);
                using (var x = new DBAccessClient())
                {
                    data = (await x.LoadFile(new GetSaveRequest() { Id = arg2 })).Data;
                }
                Console.WriteLine("data got " + data);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Cannot access the web api, is the web service down?");
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }
            finally
            {
               // Debug.WriteLine(MDataEvents.LOADFILEHANDLER);
            }
            return data;
        }
    }
}
