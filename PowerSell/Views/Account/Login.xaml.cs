using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using PowerSell.Models; // Assuming PowerSell.Models contains User and PowerSellDbContext classes
using PowerSell.Services; // Import the SessionManager namespace
using System.Data.Entity;
using System.Data.SqlClient;

namespace PowerSell.Views.Account
{
    public partial class Login : MetroWindow
    {
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();
        private readonly System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public Login()
        {
            InitializeComponent();
            Loaded += Login_Loaded;

            // Set up the timer to update every second
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the UTC+1 time TextBlock
            DateTime utcPlusOneTime = DateTime.Now;
            UtcPlusOneTimeTextBlock.Text = $"{utcPlusOneTime.ToString("HH:mm:ss")}";

        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the loading indicator
            LoadingGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Collapsed;
            string password = PasswordBox.Password;

            // Simulate login delay (replace with actual login logic)
            await Task.Delay(2500);

            // Call the stored procedure using SqlQuery
            var user = dbContext.Database.SqlQuery<User>("EXEC ValidateUser @password",
                new SqlParameter("@password", password))
                .FirstOrDefault();

            if (user != null && user.UserType == "worker")
            {
                // Set the user ID in the session manager
                SessionManager.Instance.UserId = user.UserId;

                // Close the loading indicator and show the login UI
                LoadingGrid.Visibility = Visibility.Collapsed;
                LoginGrid.Visibility = Visibility.Visible;

                var dataService = new DataService(dbContext);
                var dashboard = new Dashboard(dataService);
                dashboard.Show();
                Close(); // Close Login window
            }
            else if (user != null & user.UserType == "admin")
            {
                // Set the user ID in the session manager
                SessionManager.Instance.UserId = user.UserId;

                // Close the loading indicator and show the login UI
                LoadingGrid.Visibility = Visibility.Collapsed;
                LoginGrid.Visibility = Visibility.Visible;

                Admin.AdminDashboard dashboard = new Admin.AdminDashboard(); // Pass user ID to Dashboard constructor
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
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                PasswordBox.Password += button.Content.ToString();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = string.Empty;
        }

    }
}
