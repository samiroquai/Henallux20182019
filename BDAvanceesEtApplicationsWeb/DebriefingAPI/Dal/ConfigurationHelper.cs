using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DDDDemo.Dal
{
    public class ConfigurationHelper
    {
        public readonly string CONNECTION_STRING_CONFIG_KEY;
        public ConfigurationHelper(string connectionStringConfigurationKey)
        {
            CONNECTION_STRING_CONFIG_KEY = connectionStringConfigurationKey;
        }

        public string GetConnectionString ()
        {
            string configFileName = "appsettings.json";
            string expectedConfigurationFilePath = Path.Combine(Directory.GetCurrentDirectory(), configFileName);
            if (!File.Exists(expectedConfigurationFilePath))
                throw new FileNotFoundException("Le fichier de configuration n'a pas pu être trouvé. Il doit s'agir d'un fichier json nommé appsettings.json qui contient une section ConnectionStrings", expectedConfigurationFilePath);
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(configFileName)
               .Build();
            string connectionString = config.GetConnectionString(CONNECTION_STRING_CONFIG_KEY);
            if (String.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException($"Le fichier de configuration doit contenir une connection string nommée {CONNECTION_STRING_CONFIG_KEY}");
            return connectionString;
        }
    }
}
