using CitizenFX.Core;
using Proline.ClassicOnline.MData;
using Proline.ClassicOnline.MData.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection.Internal
{
    internal class ExtendedPlayer
    {
        public long PlayerId { get; set; }
        public Save SaveFiles { get; set; }
        public Player Player { get; set; }
    }

    internal class PlayerManager
    {
        private static PlayerManager _instance;
        private List<ExtendedPlayer> _playerDictionary;

        private PlayerManager()
        {
            _playerDictionary = new List<ExtendedPlayer>();
        }

        internal static PlayerManager GetInstance()
        {
            if (_instance == null)
                _instance = new PlayerManager();
            return _instance;
        }

        internal void AddPlayer(ExtendedPlayer player)
        {
            _playerDictionary.Add(player);
        }

        internal ExtendedPlayer GetPlayer(long playerId)
        {
            return _playerDictionary.FirstOrDefault(e => e.PlayerId == playerId);
        }

        internal void RemovePlayer(ExtendedPlayer player)
        {
            _playerDictionary.Remove(player);
        }
    }
}
