using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Resource.Eventing;
using Console = Proline.Resource.Console;
using Proline.DBAccess.Proxies;
using System.Net.Sockets;

namespace Proline.ClassicOnline.MData.Events
{
    internal class LoadFileSubscriber : EventSubscriber
    {
        public LoadFileSubscriber() : base(EventHandlerNames.LOADFILEHANDLER)
        {
        }

        public override object OnEventTriggered(Player player, params object[] args)
        {
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
                Debug.WriteLine(EventHandlerNames.FILELOADEDHANDLER);
            }
            return data;
        }

    }
}

