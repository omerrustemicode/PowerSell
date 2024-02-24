using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PowerSell.Localization
{
    public partial class LangManager
    {
        private readonly string _langFolderPath;

        public LangManager(string langFolderPath)
        {
            _langFolderPath = langFolderPath;
        }

        public string CurrentLanguage { get; private set; } = "en-US";

        public void ChangeLanguage(string languageCode)
        {
            CurrentLanguage = languageCode;
        }

        public string GetString(string key)
        {
            var filePath = Path.Combine(_langFolderPath, $"{CurrentLanguage}.json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (translations.TryGetValue(key, out var translation))
                {
                    return translation;
                }
            }

            return key; // Return the key itself if translation not found
        }
    }
}
