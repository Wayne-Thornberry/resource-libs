using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.ClassicOnline.MData.Events;
using Proline.ClassicOnline.MDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData
{
    public partial class API
    {
        // SendSaveToCloud - Sends a request to the server to upload current Save (Save consists of multiple data files)
        // PullSaveFromCloud - Sends a request to download the file to the client
        public static async Task SendSaveToCloud()
        {
            try
            {
                var fm = SaveManager.GetInstance();
                MDebugAPI.LogDebug($"Saving save file...");
                fm.IsSaveInProgress = true;
                foreach (var item in fm.GetSaveFiles())
                {
                    try
                    { 
                        if (item == null)
                        {
                            throw new Exception($"Cannot save file, current save file is null");
                        };
                        var json = JsonConvert.SerializeObject(item);
                        MDebugAPI.LogDebug($"Saving file... {json}");
                        using (var sfEvent = SaveFileNetworkEvent.TriggerEvent(json))
                        {
                            await sfEvent.WaitForCallback();
                        }
                    }
                    catch (Exception e)
                    {
                        MDebugAPI.LogDebug(e.ToString());
                    }
                }
                fm.IsSaveInProgress = false;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
        } 
        public static bool IsSaveInProgress()
        {
            try
            {
                var fm = SaveManager.GetInstance();
                return fm.IsSaveInProgress;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return false;
        }

        public static int? GetSaveState()
        {
            try
            {
                var fm = SaveManager.GetInstance();
                if (fm.LastSaveResult != null)
                {
                    var state = fm.LastSaveResult;
                    fm.LastSaveResult = null;
                    return state.Value;
                }
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return null;
        }

        public static async Task PullSaveFromCloud(long id)
        {
            try
            {
                MDebugAPI.LogDebug("Load Request put in");
                var fm = SaveManager.GetInstance();
                int result = 0;
                using (var lfEvent = LoadFileNetworkEvent.TriggerEvent(id))
                { 
                    MDebugAPI.LogInfo("Waiting for callback to be completed...");
                    result = await lfEvent.WaitForCallback();
                } ; 
                if (result == 0)
                    MDebugAPI.LogInfo("Callback has completed");
                else if (result == 1)
                    throw new Exception("Callback has timed out");
                fm.HasLoadedFromNet = true;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
        }

        public static bool HasSaveLoaded()
        {
            try
            {
                var fm = SaveManager.GetInstance(); 
                return fm.HasLoadedFromNet;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return false;
        }


        public static async Task PullSaveFromCloud(string username)
        {
            try
            {
                MDebugAPI.LogDebug("Load Request put in");
                var lfEvent = LoadFileNetworkEvent.TriggerEvent(username);
                MDebugAPI.LogInfo("Waiting for callback to be completed...");
                var result = await lfEvent.WaitForCallback();
                if (result == 0)
                    MDebugAPI.LogInfo("Callback has completed");
                else if (result == 1)
                    throw new Exception("Callback has timed out");
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
        }

        public static async Task PullSaveFromCloud()
        {
            try
            {
                var fm = SaveManager.GetInstance();
                //MDebugAPI.LogDebug($"Saving save file...");
                //fm.IsSaveInProgress = true;
                //foreach (var item in fm.GetSaveFiles())
                //{
                //    try
                //    {
                //        if (item == null)
                //        {
                //            throw new Exception($"Cannot save file, current save file is null");
                //        };
                //        var json = JsonConvert.SerializeObject(item);
                //        MDebugAPI.LogDebug($"Saving file... {json}");
                //        var sfEvent = SaveFileNetworkEvent.TriggerEvent(json);
                //        await sfEvent.WaitForCallback();
                //    }
                //    catch (Exception e)
                //    {
                //        MDebugAPI.LogDebug(e.ToString());
                //    }
                //}
                //fm.IsSaveInProgress = false;
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
        }
    }
}
