using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rocket.Unturned.Events.UnturnedEvents;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Logger = Rocket.Core.Logging.Logger;
using UnityEngine;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;
using InsurancePolicyPlugin.Modules;
using InsurancePolicyPlugin.PlayerConnection;
using InsurancePolicyPlugin.DataBase;
using Rocket.API.Collections;

namespace InsurancePolicyPlugin
{
    public class InsurancePolicyPlugin : RocketPlugin<InsurancePolicyPluginConfig>
    {
        public static InsurancePolicyPlugin Instance { get; private set; }
        public Color MessageColor { get; private set; }
        protected override void Load()
        {
            Instance = this;
            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, UnityEngine.Color.cyan);

            U.Events.OnPlayerConnected += this.OnPlayerConnected;
            U.Events.OnPlayerDisconnected += this.OnPlayerDisconnected;
            Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!");
            DataBaseManager.GarantyExistDB(Directory, "PlayersPolicyDataBase.json");
        }
        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= this.OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= this.OnPlayerDisconnected;
        }
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "OnEquipPolicyMessage1",  "Документ: Страховой Полис"},
            { "OnEquipPolicyMessage2",  "Оформлен на: {0}"},
            { "OnEquipPolicyMessage3",  "Aктивирован мед.работником: {0}"},
            { "OnEquipPolicyMessage4",  "Действителен до: {0}"},
            { "NotEnoughMoney",  "Недостаточно денег"},
            { "PlayerPolicyExist",  "На игрока уже оформлена страховка"},
            { "MedicActivateMessage",  "Вы активировали страховку {0}"},
            { "PlayerActivateMessage",  "Вам активировали страховку {0}"},
        };
    }
}
