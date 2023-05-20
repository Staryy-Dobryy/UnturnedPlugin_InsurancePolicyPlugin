using Rocket.Unturned.Chat;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InsurancePolicyPlugin.Modules
{
    internal static class InventoryHandler
    {
        internal static void OnTakeItemRequested(this InsurancePolicyPlugin plugin, Player player, byte x, byte y, uint instanceID, byte to_x, byte to_y, byte to_rot, byte to_page, ItemData itemData, ref bool shouldAllow)
        {
            var caller = UnturnedPlayer.FromPlayer(player);
            var count = caller.Inventory.search(plugin.Configuration.Instance.PolicyActivatedId, true, true).Count;
            if (itemData.item.id == plugin.Configuration.Instance.PolicyActivatedId && count > 0)
            {
                x = player.inventory.has(plugin.Configuration.Instance.PolicyActivatedId).jar.x;
                y = player.inventory.has(plugin.Configuration.Instance.PolicyActivatedId).jar.y;
                byte page = caller.Inventory.has(plugin.Configuration.Instance.PolicyActivatedId).page;
                byte index = player.inventory.getIndex(page, x, y);
                player.inventory.removeItem(page, index);
                caller.GiveItem(plugin.Configuration.Instance.PolicyDamagedId, 1);
            }
            else if (itemData.item.id == plugin.Configuration.Instance.PolicyActivatedId && count == 0)
                caller.Events.OnInventoryAdded += OnInventoryAdded;
            
        }
        private static void OnInventoryAdded(UnturnedPlayer player, InventoryGroup inventoryGroup, byte inventoryIndex, ItemJar P)
        {
            if (P.item.id == InsurancePolicyPlugin.Instance.Configuration.Instance.PolicyActivatedId)
            {
                player.Inventory.removeItem(player.Inventory.has(81).page, inventoryIndex); ;
                player.GiveItem(InsurancePolicyPlugin.Instance.Configuration.Instance.PolicyDamagedId, 1);
                player.Events.OnInventoryAdded -= OnInventoryAdded;
            }
        }
    }
}
