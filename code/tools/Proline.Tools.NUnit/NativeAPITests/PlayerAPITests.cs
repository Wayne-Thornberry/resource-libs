using NUnit.Framework; 
using System;
using System.Threading;
using CitizenFX.Core;

namespace Proline.ProlineEngine.NativeAPITests.NUnit
{
    [TestFixture]
    public class PlayerAPITests
    {
        //[Test]
        //public void AssertThatPlayerIsDead()
        //{
        //    var nativeAPI = new NativeAPI();
        //    var handle = nativeAPI.GetPlayerPed(-1);
        //    nativeAPI.SetEntityHealth(handle, 0);
        //    var result = nativeAPI.IsPlayerDead(0); 
        //    Assert.IsTrue(result);
        //}

        //[Test]
        //public void AssertTeleport()
        //{
        //    var nativeAPI = new NativeAPI();
        //    var handle = nativeAPI.GetPlayerPed(-1);
        //    nativeAPI.SetEntityCoords(handle, 0,0,70,false,false,false,false);
        //    //var result = nativeAPI.IsPlayerDead(0);
        //    //Assert.IsTrue(result);
        //}

        //[Test]
        //public void AssertWeatherNative()
        //{
        //    var nativeAPI = new NativeAPI();
        //    nativeAPI.SetWeatherTypeNow("RAIN");
        //    nativeAPI.SetWeatherTypePersist("RAIN");
        //    nativeAPI.SetOverrideWeather("RAIN");
        //    nativeAPI.NetworkOverrideClockTime(6, 30, 0);
        //    //var result = nativeAPI.IsPlayerDead(0);
        //    //Assert.IsTrue(result);
        //}


        //[Test]
        //public void AssertSpawnCar()
        //{
        //    var API = new NativeAPI();
        //    var modelHash = (uint)API.GetHashKey("HUNTER");
        //    if (API.IsModelInCdimage(modelHash) || API.IsModelValid(modelHash) || API.IsWeaponValid(modelHash))
        //    {
        //        API.RequestModel(modelHash);
        //        while (!API.HasModelLoaded(modelHash))
        //        {
        //            Thread.Sleep(10);
        //        }
        //        var pos = API.GetEntityCoords(API.GetPlayerPed(-1), false);
        //        API.CreateVehicle(modelHash, pos.X, pos.Y, pos.Z, 0, true, false);
        //        API.SetModelAsNoLongerNeeded(modelHash);
        //    }
             
        //    //var result = nativeAPI.IsPlayerDead(0);
        //    //Assert.IsTrue(result);
        //}
    }
}
