using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PowerSell.Views.Admin
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void ChangeBackground_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to change the background here
            // For example:
            this.Background = Brushes.LightBlue;

            // Save the background color to XML
            SaveSettingsToXml();
        }

        private void SaveSettingsToXml()
        {
            // Create a SettingsData object to store settings
            SettingsData settingsData = new SettingsData { BackgroundColor = (this.Background as SolidColorBrush)?.Color };

            // Serialize the settingsData object to XML
            XmlSerializer serializer = new XmlSerializer(typeof(SettingsData));
            using (TextWriter writer = new StreamWriter("Settings.xml"))
            {
                serializer.Serialize(writer, settingsData);
            }
        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Implement logic to update language settings here
            if (LanguageComboBox.SelectedItem != null)
            {
                var selectedLanguage = (LanguageComboBox.SelectedItem as ComboBoxItem).Content.ToString();
                MessageBox.Show($"Selected language: {selectedLanguage}");
                // Implement language change logic based on selectedLanguage
            }
        }
    }
    public class SettingsData
    {
        public Color? BackgroundColor { get; set; }
        // Add more properties as needed for other settings
    }
}
