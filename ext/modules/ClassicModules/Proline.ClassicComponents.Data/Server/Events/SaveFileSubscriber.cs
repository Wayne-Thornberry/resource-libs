using CitizenFX.Core;
using Proline.DBAccess.Proxies;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Events
{
    internal class SaveFileSubscriber : EventSubscriber
    {
        public SaveFileSubscriber() : base(EventHandlerNames.SAVEFILEHANDLER)
        {
        }

        public override object OnEventTriggered(Player player, params object[] args)
        {
            string arg2 = args[0].ToString();
            return SaveFileAsync(player, arg2).Result;
        } 

        private static async Task<int> SaveFileAsync(Player player, string arg2)
        {
            var responseCode = -1;
            try
            {
                Debug.WriteLine(arg2);
                using (var x = new DBAccessClient())
                {
                    var response = await x.RegisterPlayer(new RegisterPlayerRequest() { Name = player.Name });
                    long id = response.Id;
                    Debug.WriteLine(String.Format("Registered user returned {0}, id {1}", response.ReturnCode, response.Id));
                    if (response.ReturnCode == 1)
                    {
                        var getPlayerResponse = await x.GetPlayer(new GetPlayerRequest() { Username = player.Name });
                        Debug.WriteLine(String.Format("Getting Player {0}, id {1}", getPlayerResponse.ReturnCode, getPlayerResponse.PlayerId));
                        id = getPlayerResponse.PlayerId;
                    }
                    var response2 = await x.SaveFile(new InsertSaveRequest() { PlayerId = id, Data = arg2 }); 
                }
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
                Debug.WriteLine(EventHandlerNames.FILESAVEDHANDLER);
            }
            return responseCode;
        }
    }
}
