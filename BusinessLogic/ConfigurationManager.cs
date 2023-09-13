using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ConfigurationManager
    {
        #region Singleton
        protected static ConfigurationManager __instance;
        public static ConfigurationManager Instance
        {
            get
            {
                if (__instance == null)
                {
                    __instance = new ConfigurationManager();
                }

                return __instance;
            }
        }
        #endregion
        private readonly IConfiguration _config;

        public ConfigurationManager()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: true); // Optional: Set optional parameter as false if the file is mandatory
            _config = builder.Build().GetSection("AppConfigs");
        }

        public string GetConfig(string key)
        {
            // Get the value from appsettings.json
            string value = _config[key];
            return value;
        }
    }
}
