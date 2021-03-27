using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace HermesBot
{
    public class RuntimeConfig
    {
        public readonly BotConfigModel BotConfig;

        public RuntimeConfig()
        {
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
                + ".runtimeconfig.json");

            using var fileStream = new StreamReader(configPath);
            var jsonString = fileStream.ReadToEnd();
            BotConfig = JsonConvert.DeserializeObject<RuntimeConfigModel>(jsonString,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Converters = new List<JsonConverter>()
                })?.RuntimeOptions?.BotConfig;
        }
    }

    internal class RuntimeConfigModel
    {
        public RuntimeOptionsModel RuntimeOptions { get; set; }
        
    }

    internal class RuntimeOptionsModel
    {
        public BotConfigModel BotConfig { get; set; }
    }

    public class BotConfigModel
    {
        public string Token { get; set; }
    }
}