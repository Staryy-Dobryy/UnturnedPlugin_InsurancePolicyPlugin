using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyPlugin
{
    public class InsurancePolicyPluginConfig : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public uint PolicyActivationPrice { get; set; }
        public uint TimeToDeletePolicy { get; set; }
        public ushort PolicyNotActivatedId { get; set; }
        public ushort PolicyActivatedId { get; set; }
        public ushort PolicyOverdueId { get; set; }
        public ushort PolicyDamagedId { get; set; }

        public void LoadDefaults()
        {
            MessageColor = "cyan";
            PolicyActivationPrice = 500;
            TimeToDeletePolicy = 259200;
            PolicyNotActivatedId = 81;
            PolicyActivatedId = 82;
            PolicyOverdueId = 83;
            PolicyDamagedId = 84;
        }
    }
}
