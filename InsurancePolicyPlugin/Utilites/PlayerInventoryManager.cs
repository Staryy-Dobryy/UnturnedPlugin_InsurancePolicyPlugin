using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDG.Provider.SteamGetInventoryResponse;

namespace InsurancePolicyPlugin.Utilites
{
    public static class PlayerInventoryManager
    {
        public static void RemoveItem(this UnturnedPlayer player, ushort itemId, byte count = 1)
        {
            var itemsList = player.Inventory.search(itemId, true, true);
            byte x, y, page, index;
            for (byte i = 0; i < count && itemsList.Count > 0; i++)
            {
                x = itemsList[i].jar.x;
                y = itemsList[i].jar.y;
                page = itemsList[i].page;
                index = player.Inventory.getIndex(page, x, y);
                player.Inventory.removeItem(page, index);
            }
        }
        public static void SwapItems(this UnturnedPlayer player, ushort baseItemId, ushort targetItemId, byte count = 1)
        {
            if (player.Inventory.search(baseItemId, true, true).Count > 0)
            {
                RemoveItem(player, baseItemId, count);
                player.GiveItem(targetItemId, count);
            }
        }
    }
}
