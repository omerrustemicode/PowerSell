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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordBox.Password;

            // Validate username and password against database
            User user = dbContext.Users
                .FirstOrDefault(u => u.Password == password);

            if (user != null)
            {
                Dashboard dashboard = new Dashboard(user.UserId); // Pass user ID to Dashboard constructor
                dashboard.Show();
                Close(); // Close Login window
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }



        private async void Login_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateProgressBarAsync();
            ShowLoginUI();
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
        }

        private void ShowLoginUI()
        {
            LoadingGrid.Visibility = Visibility.Collapsed;
            LoginGrid.Visibility = Visibility.Visible;
        }
    }
}
