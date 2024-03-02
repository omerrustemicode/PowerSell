using PowerSell.Localization;
using PowerSell.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell.Views.ClientView
{
    public partial class SingleClientWindow : Window
    {
        private LocalizationManager _localizationManager;
        public int TableId { get; private set; }

        public SingleClientWindow(int tableId)
        {
            InitializeComponent();
            TableId = tableId;
            Loaded += SingleClientWindow_Loaded;
        }
        private void SingleClientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Use TableId as needed when the window is loaded
            // Example: Update UI elements based on TableId
        }
        // Event handler for button clicks
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Your existing button click logic
        }

        private void Transport_Btn(object sender, RoutedEventArgs e)
        {
            // Open the KeyboardWindow
            var keyboardWindow = new KeyboardWindow();
            keyboardWindow.ShowDialog();
        }
    }
}
