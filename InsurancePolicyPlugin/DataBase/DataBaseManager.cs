using InsurancePolicyPlugin.Utilites;
using Newtonsoft.Json.Bson;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.PlayerLoop.Initialization;

namespace InsurancePolicyPlugin.DataBase
{
    public static class DataBaseManager
    {
        public static void GarantyExistDB(string dir, string fileName)
        {
            var path = Path.Combine(dir, fileName);
            if (!File.Exists(path)) File.Create(path);
        }
        public static bool PlayerPolicyExist(this InsurancePolicyPlugin plugin, UnturnedPlayer targetPlayer)
        {
            var playersData = new DataContext<List<PlayerPolicyData>>(plugin.Directory, "PlayersPolicyDataBase.json").Read();
            return playersData.Any(x => x.PlayerSteamID == targetPlayer.CSteamID.m_SteamID);
        }
        public static void CheckPlayerPolicy(this InsurancePolicyPlugin plugin, UnturnedPlayer targetPlayer)
        {
            var data = new DataContext<List<PlayerPolicyData>>(plugin.Directory, "PlayersPolicyDataBase.json");
            var playersData = data.Read();
            Console.WriteLine(playersData);
            var playerData = GetPlayerData(playersData, targetPlayer);
            Console.WriteLine(playerData);
            if (playerData != null && DateTime.Now > playerData.EndDateTime && playersData.Remove(playerData))
            {
                targetPlayer.SwapItems(plugin.Configuration.Instance.PolicyActivatedId, plugin.Configuration.Instance.PolicyOverdueId);
                data.Save(playersData);
            }
        }
        public static PlayerPolicyData GetPlayerData(this InsurancePolicyPlugin plugin, UnturnedPlayer targetPlayer)
        {
            var playersData = new DataContext<List<PlayerPolicyData>>(plugin.Directory, "PlayersPolicyDataBase.json").Read();
            return playersData.FirstOrDefault(x => x.PlayerSteamID == targetPlayer.CSteamID.m_SteamID);
        }
        public static PlayerPolicyData GetPlayerData(List<PlayerPolicyData> playersData, UnturnedPlayer targetPlayer)
        {
            return playersData.FirstOrDefault(x => x.PlayerSteamID == targetPlayer.CSteamID.m_SteamID);
        }
        public static void AddPlayerToDB(this InsurancePolicyPlugin plugin, UnturnedPlayer targetPlayer, string callerName)
        {
            var playerData = new PlayerPolicyData
            (
                targetPlayer.CSteamID.m_SteamID, 
                DateTime.Now.AddDays(plugin.Configuration.Instance.TimeToDeletePolicy), 
                callerName, 
                targetPlayer.CharacterName
            );

            var data = new DataContext<List<PlayerPolicyData>>(plugin.Directory, "PlayersPolicyDataBase.json");

            if (data.Read() == null) data.Save(new List<PlayerPolicyData>() { playerData });
            else data.Save(data.Read().Append(playerData).ToList());
        }
    }
}