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
using Newtonsoft.Json;

namespace Proline.ClassicOnline.MData
{
    public partial class LoadFileNetworkEvent : ExtendedEvent
    {
        public LoadFileNetworkEvent() : base(LOADFILEHANDLER)
        {
            DisableAutoCallback = true;
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            // old way via getting id
            if(args.Length > 0)
            {

                string arg2 = args[0].ToString();
                LoadFileAsync(player, arg2);
                return null;
            }
            else
            {
                Console.WriteLine("Argument count does not match expected count");
                return null;
            }
        }

        private async Task LoadFileAsync(Player player, string arg2)
        {
            List<SaveFile> data = new List<SaveFile>();
            try
            {
                Console.WriteLine("Load Request Recived " + arg2);
                using (var x = new DBAccessClient())
                {
                    var response = await x.LoadFile(new GetSaveRequest() { Username = arg2 });
                    var saveFiles = response.SaveFiles;
                    foreach (var item in saveFiles)
                    {
                        data.Add(new SaveFile()
                        { 
                            Identifier = item.Identity,
                            Properties = JsonConvert.DeserializeObject<Dictionary<string,object>>(item.Data),
                        });
                        Console.WriteLine("data got " + item.Data);
                    }
                }
                Console.WriteLine("Load Request  " + data.Count);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Cannot access the web api, is the web service down?");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                ExternalInvokeCallback(player, data);
            }

        }
    }
}
