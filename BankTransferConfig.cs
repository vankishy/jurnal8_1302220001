using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace modul8_1302220001
{
    internal class BankTransferConfig
    {
        public const string filePath = "bank_transfer_config.json";
        Config bankConfig;
        public BankTransferConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }

        private Config ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            bankConfig = JsonSerializer.Deserialize<Config>(configJsonData);
            return bankConfig;

        }
        public void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(bankConfig, options);
            File.WriteAllText(filePath, jsonString);
        }

        public void SetDefault()
        { 
            List<String> methods = new List<String>() {"RTO (real-time)", "SKN", "RTGS", "BI FAST"};
            Transfer transfer1 = new Transfer(25000000, 6500, 15000);
            Confirmation confirmation1 = new Confirmation("yes", "ya");
            bankConfig = new Config("en", methods, transfer1, confirmation1);
        }

        class Config
        {
            public string lang { get; set; }
            public List<string> methods { get; set; }
            public Transfer transfer { get; set; }
            public Confirmation confirmation { get; set; }

            public Config(string lang, List<string> methods, Transfer transfer, Confirmation confirmation)
            {
                this.lang = lang;
                this.methods = methods;
                this.transfer = transfer;
                this.confirmation = confirmation;
            }
        }

        class Transfer
        {
            public int threshold { get; set; }
            public int low_fee { get; set; }
            public int high_fee { get; set; }

            public Transfer(int threshold, int low_fee, int high_fee)
            {
                this.threshold = threshold;
                this.low_fee = low_fee;
                this.high_fee = high_fee;
            }
        }

        class Confirmation
        {
            public string en { get; set; }
            public string id { get; set; }
            
            public Confirmation(string en, string id)
            {
                this.en = en;
                this.id = id;
            }
        }
    }
}
