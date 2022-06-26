using CitizenFX.Core;
using Proline.ClassicOnline.MData.Entity;
using Proline.ClassicOnline.MData.Events; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Console = Proline.Resource.Console;
#if SERVER 
using Proline.DBAccess.Proxies;
#endif
using System.Net.Sockets;

namespace Proline.ClassicOnline.MData
{

    internal static class EventHandlerNames
    {
        public const string SAVEFILEHANDLER = "SaveFileHandler";
        public const string LOADFILEHANDLER = "LoadFileHandler";

        public const string FILELOADEDHANDLER = "FileLoadedHandler";
        public const string FILESAVEDHANDLER = "FileSavedHandler";

        // public static LoadFileEvent LoadFileEvent { get; set; }

#if CLIENT
        public static async Task LoadFileEvent(long id)
        {
            //BaseScript.TriggerServerEvent(LOADFILEHANDLER, new Action<object>((args) =>
            //{
            //    //// so this will be called back?
            //    /// ok lets see 
            //    var obj = args.ToString();
            //    if (string.IsNullOrEmpty(obj)) return;
            //    var instance = SaveFileManager.GetInstance();
            //    Console.WriteLine("data got " + obj);
            //    instance.PutFileIntoMemory(obj);
            //    return;
            //}), id);
            //await BaseScript.Delay(5000);


            using (var x = new LoadFileNetworkEvent())
            {

            } ;

            //using (var events = new LoadFileNetworkMethod(new Action<long>((args) =>
            //{
            //    // so this will be called back?
            //    /// ok lets see 
            //    var obj = args.ToString();
            //    if (string.IsNullOrEmpty(obj)) return;
            //    var instance = SaveFileManager.GetInstance();
            //    Console.WriteLine("data got " + obj);
            //    instance.PutFileIntoMemory(obj);
            //    return;
            //})))
            //{
            //    events.Invoke(id);
            //}
        }
#endif
    }
}
