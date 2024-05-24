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

        public string Transport => _resourceManager.GetString("Transport");
        public string Bill => _resourceManager.GetString("Bill");
        public string Ready => _resourceManager.GetString("Ready");
        public string PrintService => _resourceManager.GetString("PrintService");
        public string Paid => _resourceManager.GetString("Paid");
        public string DeleteButton => _resourceManager.GetString("DeleteButton");
        public string AddButton => _resourceManager.GetString("AddButton");
        public string Message => _resourceManager.GetString("Message");
    }
}
