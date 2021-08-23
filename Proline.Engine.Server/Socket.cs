using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ProlineEngine.Access;

namespace Proline.ProlineEngine.Script
{
    public partial class EngineScript : BaseScript
    {
        public static string data = null;

        private void SocketThread()
        { // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    _engine.LogDebug(_header,"Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }
                    data = Regex.Replace(data, "<EOF>", "");
                    //var method = JsonConvert.DeserializeObject<NetworkRequest>(data);
                    // Show the data on the console.  
                    _engine.LogDebug(_header, $"Text received : {data}");
                    //var plauerHandle = method.TargetClient;
                    //var player = Players[plauerHandle];
                     

                    //_engine.ExecuteClientNativeAPI(_header, player, method);
                    BaseScript.TriggerClientEvent("networkRequestListener", data);


                    var timeoutTicks = 0;
                    var oldTime = DateTime.UtcNow;
                    var data2 = "";
                    while (DateTime.UtcNow.Subtract(oldTime).Seconds < 30)
                    {
                        //_engine.HasEngineEventTriggered(_header, "networkResponseListener", out bool hasTriggered);
                        //if (hasTriggered)
                        //{
                        //    _engine.GetEngineEventData(_header, "networkResponseListener", out var args);
                        //    _engine.LogDebug(_header, "Engine event found to be triggered with data");
                        //    data2 = (string) args[0];
                        //    break;
                        //}
                        if (_lastResponse != null)
                            break;
                        timeoutTicks++;
                    }

                    _engine.LogDebug(_header, "" + timeoutTicks  + " timeout ticks " + _lastResponse);

                    if (DateTime.UtcNow.Subtract(oldTime).Seconds > 30 || _lastResponse == null)
                    {
                        _engine.LogDebug(_header, "Timeout occured in executing native api, no data returned"); 
                    }
                    else
                    { 
                        // Echo the data back to the client.  
                        byte[] msg = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(_lastResponse));
                        _lastResponse = null;

                        handler.Send(msg);
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }
    }
}
