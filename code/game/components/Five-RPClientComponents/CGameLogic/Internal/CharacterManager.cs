using CGameLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGameLogic.Internal
{
    internal static class CharacterManager
    {
        internal static int NextCharacterIndex { get; set; }
        internal static List<PlayerCharacter> Characters { get; set; }
    }
}
