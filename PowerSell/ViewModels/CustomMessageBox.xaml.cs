using System;
using System.Collections.Generic;
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
using PowerSell.Services;

namespace PowerSell.ViewModels
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : UserControl
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CustomMessageBox()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            OKCommand?.Execute(null);
            CloseCustomMessageBox();
        }
        private void CloseCustomMessageBox()
        {
            // Find the CustomMessageBox window
            Window window = Application.Current.Windows.OfType<Window>()
                .FirstOrDefault(w => w.GetType() == typeof(CustomMessageBox));

            // Close the window if found
            if (window != null)
            {
                window.Close();
            }
        }

        private void CloseWindow()
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
