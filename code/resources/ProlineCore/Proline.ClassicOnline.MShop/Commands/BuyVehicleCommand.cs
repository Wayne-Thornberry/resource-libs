using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.GCharacter;
using Proline.ClassicOnline.MShop;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class BuyVehicleCommand : ModuleCommand
    {
        public BuyVehicleCommand() : base("BuyVehicle")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length > 0)
            {
                var vehicle = args[0].ToString();
                MShopAPI.BuyVehicle(vehicle);
            }

        }
    }
}
