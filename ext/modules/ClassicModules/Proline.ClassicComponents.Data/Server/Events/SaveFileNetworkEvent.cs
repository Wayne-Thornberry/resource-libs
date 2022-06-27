using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.DBAccess.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData.Events
{
    public partial class SaveFileNetworkEvent
    {
        public SaveFileNetworkEvent() : base(SAVEFILEHANDLER)
        {
            DisableAutoCallback = true;
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            if (args.Length > 0)
            {

                var data = args[0].ToString();
                //Debug.WriteLine(data);
                var saveFile = JsonConvert.DeserializeObject<SaveFile>(data);
                SaveFileAsync(player, saveFile);
                return null;
            }
            else
            { 
                Console.WriteLine("Argument count does not match expected count");
                return null;
            }
        }

        private async Task SaveFileAsync(Player player, SaveFile saveFile)
        {
            var responseCode = -1;
            try
            {
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
                    var response2 = await x.SaveFile(new InsertSaveRequest() { PlayerId = id, Identity = saveFile.Identifier, Data = JsonConvert.SerializeObject(saveFile.Properties)});
                    responseCode = response2.ReturnCode;
                }
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
                ExternalInvokeCallback(player, responseCode);
            } 
        }
    }
}
