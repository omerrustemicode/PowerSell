using MahApps.Metro.Controls;
using PowerSell.Services;
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
using System.Windows.Shapes;

namespace PowerSell.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : MetroWindow
    {
        string username = SessionManager.Instance.UserName;
        public AdminDashboard()
        {
            InitializeComponent();
            DisplayWorkerName.Title = username;
        }

        private void DisplayWorkerName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void HomeButton_Click_1(object sender, RoutedEventArgs e)
        {
            RightPanelContent.Content = new Home(); // Load Home.xaml into the RightPanelContent
        }

        private void WorkersButton_Click(object sender, RoutedEventArgs e)
        {
            RightPanelContent.Content = new Workers(); // Load Workers.xaml into the RightPanelContent
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            RightPanelContent.Content = new Reports(); // Load Reports.xaml into the RightPanelContent
        }

        private void TablesButton_Click(object sender, RoutedEventArgs e)
        {
            RightPanelContent.Content = new Tables(); // Load Tables.xaml into the RightPanelContent
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
            RightPanelContent.Content = new Services(); // Load Services.xaml into the RightPanelContent
        }

        private void CategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            RightPanelContent.Content = new Categories(); // Load Categories.xaml into the RightPanelContent
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            RightPanelContent.Content = new Settings(); // Load Settings.xaml into the RightPanelContent
        }

    }
}
