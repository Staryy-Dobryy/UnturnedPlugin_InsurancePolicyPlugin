using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;
using UnityEngine;
using SDG.Unturned;
using InsurancePolicyPlugin.Utilites;
using InsurancePolicyPlugin.DataBase;
using System.Numerics;
using Rocket.API;

namespace InsurancePolicyPlugin.Modules
{
    internal static class GestureHandler
    {
        internal static void OnUpdateGesture(this InsurancePolicyPlugin plugin, UnturnedPlayer caller, PlayerGesture gesture)
        {
            if (caller.HasPermission("InsurancePolicyPlugin") && gesture == PlayerGesture.Point)
            {
                var targetPlayer = DamageTool.raycast(new Ray(caller.Player.look.aim.position, caller.Player.look.aim.forward), 3f, RayMasks.PLAYER_INTERACT, caller.Player).player;
                if (targetPlayer != null && targetPlayer.equipment.itemID == plugin.Configuration.Instance.PolicyNotActivatedId)
                {
                    UnturnedPlayer player = UnturnedPlayer.FromPlayer(targetPlayer);
                    if (player.Experience < plugin.Configuration.Instance.PolicyActivationPrice)
                    {
                        UnturnedChat.Say(caller, plugin.Translate("NotEnoughMoney"), plugin.MessageColor);
                        return;
                    }

                    if (!plugin.PlayerPolicyExist(player))
                    {
                        player.SwapItems(plugin.Configuration.Instance.PolicyNotActivatedId, plugin.Configuration.Instance.PolicyActivatedId);
                        player.Experience -= plugin.Configuration.Instance.PolicyActivationPrice;
                        plugin.AddPlayerToDB(player, caller.CharacterName);
                        UnturnedChat.Say(player, plugin.Translate("PlayerActivateMessage"), plugin.MessageColor);

                        caller.Experience += plugin.Configuration.Instance.PolicyActivationPrice;
                        UnturnedChat.Say(caller, plugin.Translate("MedicActivateMessage"), plugin.MessageColor);
                    }
                    else
                    {
                        UnturnedChat.Say(caller, plugin.Translate("PlayerPolicyExist"), plugin.MessageColor);
                    }
                }
            }
        }
    }
}
