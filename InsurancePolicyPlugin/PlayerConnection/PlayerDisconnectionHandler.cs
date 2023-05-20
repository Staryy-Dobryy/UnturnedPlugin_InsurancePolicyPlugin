using InsurancePolicyPlugin.Modules;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyPlugin.PlayerConnection
{
    internal static class PlayerDisconnectionHandler
    {
        internal static void OnPlayerDisconnected(this InsurancePolicyPlugin plugin, UnturnedPlayer player)
        {
            player.Player.equipment.onEquipRequested -= plugin.OnEquipRequested;
            player.Events.OnUpdateGesture -= plugin.OnUpdateGesture;
            ItemManager.onTakeItemRequested -= plugin.OnTakeItemRequested;
        }
    }
}
