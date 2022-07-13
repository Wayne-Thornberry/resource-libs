using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.MDebug;

namespace Proline.ClassicOnline.SClassic
{
    public class MPStartup
    {
        public MPStartup()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            //await Game.Player.ChangeModel(new Model(1885233650));
            //for (int i = 0; i < 12; i++)
            //{ 
            //    //NativeAPI.CallNativeAPI(Hash.SET_PED_COMPONENT_VARIATION, Game.PlayerPed.Handle, i, 0, 0, 0 );
            //}
            MDebugAPI.LogDebug("Testing Persistence: " + "");//Globals.Get("EnableSomething"));
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
            stat.SetValue(0);
            stat2.SetValue(0);
            MDebugAPI.LogDebug(stat.GetValue());
            MDebugAPI.LogDebug(stat2.GetValue());
            //MDebugAPI.LogDebug(await ComponentAPI.TestNetworkAPI(1,1,1));
            //ComponentAPI.PlayerAPI(CitizenFX.Core.Game.Player.Name);

            //API.RequestIpl("");
            //await World.CreateVehicle(new Model(1747439474), Game.PlayerPed.Position);
            //NativeStats.RegisterNativeStat("SUCCESSFUL_COUNTERS");
            //NativeStats.RegisterNativeStat("FIRES_EXTINGUISHED");
            //NativeStats.RegisterNativeStat("FIRES_STARTED");
            //NativeStats.RegisterNativeStat("TIMES_CHEATED");
            //Stats.GetStat(new StatAttributes()"MISSION_NAME", "string", false, false, 1, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("TOTAL_PLAYING_TIME");
            //NativeStats.RegisterNativeStat("LONGEST_PLAYING_TIME");
            //NativeStats.RegisterNativeStat("LONGEST_PLAYING_TIME");
            //NativeStats.RegisterNativeStat("LONGEST_CAM_TIME_DRIVING");
            //NativeStats.RegisterNativeStat("MPPLY_LEAST_FAVORITE_STATION", "int", false, false, 1, false, true, -1, -1, "coder");
            //NativeStats.RegisterNativeStat("MPPLY_MOST_FAVORITE_STATION", "int", false, false, 1, false, true, -1, -1, "coder");
            //NativeStats.RegisterNativeStat("DIED_IN_DROWNING");
            //NativeStats.RegisterNativeStat("DIED_IN_DROWNINGINVEHICLE");
            //NativeStats.RegisterNativeStat("DIED_IN_EXPLOSION");
            //NativeStats.RegisterNativeStat("DIED_IN_FALL");
            //NativeStats.RegisterNativeStat("DIED_IN_FIRE");
            //NativeStats.RegisterNativeStat("DIED_IN_ROAD");
            //NativeStats.RegisterNativeStat("BUSTED");
            //NativeStats.RegisterNativeStat("MANUAL_SAVED");
            //NativeStats.RegisterNativeStat("AUTO_SAVED");
            //NativeStats.RegisterNativeStat("PLAYER_KILLS_ON_SPREE");
            //NativeStats.RegisterNativeStat("COPS_KILLS_ON_SPREE");
            //NativeStats.RegisterNativeStat("PEDS_KILLS_ON_SPREE");
            //NativeStats.RegisterNativeStat("LONGEST_KILLING_SPREE");
            //NativeStats.RegisterNativeStat("LONGEST_KILLING_SPREE_TIME");
            //NativeStats.RegisterNativeStat("KILLS_PLAYERS");
            //NativeStats.RegisterNativeStat("KILLS_STEALTH");
            //NativeStats.RegisterNativeStat("KILLS_INNOCENTS");
            //NativeStats.RegisterNativeStat("KILLS_COP");
            //NativeStats.RegisterNativeStat("KILLS_SWAT");
            //NativeStats.RegisterNativeStat("KILLS_ENEMY_GANG_MEMBERS");
            //NativeStats.RegisterNativeStat("KILLS_FRIENDLY_GANG_MEMBERS");
            //NativeStats.RegisterNativeStat("KILLS_BY_OTHERS");
            //NativeStats.RegisterNativeStat("LARGE_ACCIDENTS");
            //NativeStats.RegisterNativeStat("LONGEST_DRIVE_NOCRASH");
            //NativeStats.RegisterNativeStat("DIST_CAR");
            //NativeStats.RegisterNativeStat("DIST_DRIVING_CAR");
            //NativeStats.RegisterNativeStat("TIME_DRIVING_CAR");
            //NativeStats.RegisterNativeStat("DIST_PLANE");
            //NativeStats.RegisterNativeStat("DIST_DRIVING_PLANE");
            //NativeStats.RegisterNativeStat("TIME_DRIVING_PLANE");
            //NativeStats.RegisterNativeStat("DIST_QUADBIKE");
            //NativeStats.RegisterNativeStat("DIST_DRIVING_QUADBIKE");
            //NativeStats.RegisterNativeStat("TIME_DRIVING_QUADBIKE");
            //NativeStats.RegisterNativeStat("DIST_HELI");
            //NativeStats.RegisterNativeStat("DIST_DRIVING_HELI");
            //NativeStats.RegisterNativeStat("TIME_DRIVING_HELI");
            //NativeStats.RegisterNativeStat("DIST_BIKE");
            //NativeStats.RegisterNativeStat("DIST_DRIVING_BIKE");
            //NativeStats.RegisterNativeStat("TIME_DRIVING_BIKE");
            //NativeStats.RegisterNativeStat("DIST_BICYCLE");
            //NativeStats.RegisterNativeStat("DIST_DRIVING_BICYCLE"); */


            //NativeStats.RegisterNativeStat("HAIR_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("EYEBROW_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("FACIALHAIR_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("BLUSHER_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("LIPSTICK_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("OVERLAY_BODY_1_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("SEC_OVERLAY_BODY_1_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("SEC_HAIR_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("SEC_EYEBROW_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("SEC_FACIALHAIR_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("SEC_BLUSHER_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("SEC_LIPSTICK_TINT", "int", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("OVERLAY_BODY_1", "bool", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("OVERLAY_BODY_2", "bool", true, true, 0, false, true, -1, -1, "script");
            //NativeStats.RegisterNativeStat("OVERLAY_BODY_3", "bool", true, true, 0, false, true, -1, -1, "script");

            //NativeStats.RegisterNativeStat("MESH_HEAD0", "int", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_HEAD1", "int", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_HEAD2", "int", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_TEX0", "int", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_TEX1", "int", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_TEX2", "int", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_HEADBLEND", "double", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_TEXBLEND", "double", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_VARBLEND", "double", true, false, 0, false, false, -1, -1, "script");
            //NativeStats.RegisterNativeStat("MESH_ISPARENT", "bool", true, false, 0, false, false, -1, -1, "script");

            //for (int id = 0; id < 10; id++)
            //{
            //    NativeStats.RegisterNativeStat("MPSV_VEHICLE_BS_" + id, "int", true, true, 0, true, true, -1, -1, "script");
            //    NativeStats.RegisterNativeStat("MPSV_IMPOUND_TIME_" + id, "int", true, true, 0, true, true, -1, -1, "script");
            //    NativeStats.RegisterNativeStat("MPSV_PREMIUM_PAID_" + id, "int", true, true, 0, true, true, -1, -1, "script");
            //    NativeStats.RegisterNativeStat("MPSV_MODEL_" + id, "int", true, true, 0, true, true, -1, -1, "script");
            //    NativeStats.RegisterNativeStat("MPSV_FLAGS_" + id, "int", true, true, 0, true, true, -1, -1, "script");
            //    NativeStats.RegisterNativeStat("MPSV_PRICE_PAID_" + id, "int", true, true, 0, true, true, -1, -1, "script");
            //    NativeStats.RegisterNativeStat("MPSV_LP0_" + id, "int", true, true, 0, true, true, -1, -1, "script");
            //}
        }
    }
}
