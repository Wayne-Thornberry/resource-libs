using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.MGame;
using Proline.ClassicOnline.MWorld;
using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class EnterApt : ResourceCommand
    {
        public EnterApt() : base("EnterApt")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (MGameAPI.GetCharacterBankBalance() > 1000 && args.Length == 3)
            {
                MGameAPI.SubtractValueFromBankBalance(1000);
                WorldAPI.EnterApartmentProperty(args[0].ToString(), args[1].ToString(), int.Parse(args[2].ToString()));
            }
        }
    }
}
