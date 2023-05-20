using InsurancePolicyPlugin.DataBase;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InsurancePolicyPlugin.Modules
{
    internal static class EquipHandler
    {
        internal static void OnEquipRequested(this InsurancePolicyPlugin plugin, PlayerEquipment equipment, ItemJar jar, ItemAsset asset, ref bool shouldAllow)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(equipment.player);
            if (asset.id == plugin.Configuration.Instance.PolicyActivatedId)
            {
                var playerData = plugin.GetPlayerData(player);
                UnturnedChat.Say(player, plugin.Translate("OnEquipPolicyMessage1"), plugin.MessageColor);
                UnturnedChat.Say(player, plugin.Translate("OnEquipPolicyMessage2", playerData.PlayerName), plugin.MessageColor);
                UnturnedChat.Say(player, plugin.Translate("OnEquipPolicyMessage3", playerData.NameWhoActivate), plugin.MessageColor);
                UnturnedChat.Say(player, plugin.Translate("OnEquipPolicyMessage4", playerData.EndDateTime.ToShortDateString()), plugin.MessageColor);
            }
        }
    }
}
