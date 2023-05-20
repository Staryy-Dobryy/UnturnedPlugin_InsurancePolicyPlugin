using InsurancePolicyPlugin.DataBase;
using InsurancePolicyPlugin.Modules;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InsurancePolicyPlugin.PlayerConnection
{
    internal static class PlayerConnectionHandler
    {
        internal static void OnPlayerConnected(this InsurancePolicyPlugin plugin, UnturnedPlayer player)
        {
            player.Player.equipment.onEquipRequested += plugin.OnEquipRequested;
            player.Events.OnUpdateGesture += plugin.OnUpdateGesture;
            ItemManager.onTakeItemRequested += plugin.OnTakeItemRequested;


            if (plugin.PlayerPolicyExist(player)) plugin.CheckPlayerPolicy(player);
        }
    }
}
