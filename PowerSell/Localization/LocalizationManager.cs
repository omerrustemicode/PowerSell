using System.Resources;

namespace PowerSell.Localization
{
    public class LocalizationManager
    {
        private readonly ResourceManager _resourceManager;

        public LocalizationManager()
        {
            // Assuming that "Strings" is the base name of your resource file
            _resourceManager = new ResourceManager("PowerSell.Localization.Strings", typeof(LocalizationManager).Assembly);
        }

        public string Button1 => _resourceManager.GetString("Button1");
        public string Button2 => _resourceManager.GetString("Button2");
        public string Button3 => _resourceManager.GetString("Button3");
    }
}
