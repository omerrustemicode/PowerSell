using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using PowerSell.Models; // Assuming PowerSell.Models contains User and PowerSellDbContext classes

namespace PowerSell.Views.Account
{
    public partial class Login : MetroWindow
    {
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();

        public Login()
        {
            InitializeComponent();
            Loaded += Login_Loaded;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the loading indicator
            LoadingGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Collapsed;

            string password = PasswordBox.Password;

            // Simulate login delay (replace with actual login logic)
            await Task.Delay(2000);

            // Validate username and password against database
            User user = dbContext.Users.FirstOrDefault(u => u.Password == password);

            if (user != null)
            {
                // Close the loading indicator and show the login UI
                LoadingGrid.Visibility = Visibility.Collapsed;
                LoginGrid.Visibility = Visibility.Visible;

                Dashboard dashboard = new Dashboard(user.UserId); // Pass user ID to Dashboard constructor
                dashboard.Show();
                Close(); // Close Login window
            }
            else
            {
                // Close the loading indicator and show an error message
                LoadingGrid.Visibility = Visibility.Collapsed;
                LoginGrid.Visibility = Visibility.Visible;

                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private async void Login_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateProgressBarAsync();
        }

        private async Task AnimateProgressBarAsync()
        {
            const int animationDurationMs = 2000; // Duration of the animation in milliseconds
            const int steps = 100; // Number of steps for the animation

            double stepValue = ProgressBar.Maximum / steps;
            double currentValue = 0;

            for (int i = 0; i <= steps; i++)
            {
                ProgressBar.Value = currentValue;
                currentValue += stepValue;
                await Task.Delay(animationDurationMs / steps);
            }

            ProgressBar.Value = ProgressBar.Maximum; // Ensure the progress bar reaches 100 at the end

            // Show the login UI after the progress bar animation completes
            ShowLoginUI();
        }

        private void ShowLoginUI()
        {
            LoadingGrid.Visibility = Visibility.Collapsed;
            LoginGrid.Visibility = Visibility.Visible;
        }
    }
}
