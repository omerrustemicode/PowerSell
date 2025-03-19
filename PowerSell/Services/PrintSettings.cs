using System.IO;
using System.Windows;
using System;
using System.Xml.Serialization;
using PowerSell.Views.ClientView;

namespace PowerSell.Services
{
    public static class PrinterSettingsManager
    {
        private static string GetSettingsFilePath()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(basePath, @"..\..\"));
            string settingsPath = Path.Combine(projectRoot, "Settings", "PrinterSettings.xml");
            return settingsPath;
        }

        public static PrinterSettingsModel LoadSettings()
        {
            string settingsPath = GetSettingsFilePath();
            try
            {
                if (File.Exists(settingsPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PrinterSettingsModel));
                    using (FileStream fs = new FileStream(settingsPath, FileMode.Open))
                    {
                        return (PrinterSettingsModel)serializer.Deserialize(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading printer settings: {ex.Message}");
            }
            return new PrinterSettingsModel { PrintOrderPrinter = "", PrintFiscPrinter = "" };
        }

        public static void SaveSettings(PrinterSettingsModel settings)
        {
            string settingsPath = GetSettingsFilePath();
            try
            {
                MessageBox.Show($"Saving PrinterSettings.xml to: {settingsPath}");
                XmlSerializer serializer = new XmlSerializer(typeof(PrinterSettingsModel));
                using (FileStream fs = new FileStream(settingsPath, FileMode.Create))
                {
                    serializer.Serialize(fs, settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving printer settings: {ex.Message}");
            }
        }
    }
}