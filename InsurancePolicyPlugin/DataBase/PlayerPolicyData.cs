using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyPlugin.DataBase
{
    public class PlayerPolicyData
    {
        public ulong PlayerSteamID { get; set; }
        public DateTime EndDateTime { get; set; }
        public string NameWhoActivate { get; set; }
        public string PlayerName { get; set; }
        public PlayerPolicyData
        (
            ulong playerSteamID, 
            DateTime endDateTime, 
            string nameWhoActivate, 
            string playerName
        )
        {
            PlayerSteamID = playerSteamID;
            EndDateTime = endDateTime;
            NameWhoActivate = nameWhoActivate;
            PlayerName = playerName;
        }
    }
}
