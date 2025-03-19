using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using PowerSell.Models; // Contains User and PowerSellDbContext
using PowerSell.Services; // Contains SessionManager
using System.Data.SqlClient;
using System.Windows.Threading;

namespace PowerSell.Views.Account
{
    public partial class Login : MetroWindow
    {
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();
        private readonly DispatcherTimer timer = new DispatcherTimer();

        public Login()
        {
            InitializeComponent();
            Loaded += Login_Loaded;

            // Set up the timer for UTC+1 time display
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime utcPlusOneTime = DateTime.UtcNow.AddHours(1); // Use UTC+1
            UtcPlusOneTimeTextBlock.Text = utcPlusOneTime.ToString("HH:mm:ss");
        }

        private async void Login_Loaded(object sender, RoutedEventArgs e)
        {
            await FastLoadAsync();
        }

        private async Task FastLoadAsync()
        {
            // Minimal delay to show loading UI briefly (optional, can be removed for instant load)
            await Task.Delay(300); // 300ms delay for a quick flash of the progress bar

            // Switch to login UI immediately
            ShowLoginUI();
        }

        private void ShowLoginUI()
        {
            LoadingGrid.Visibility = Visibility.Collapsed;
            LoginGrid.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = false; // Stop indeterminate animation
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Show loading indicator during login attempt
            LoadingGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Collapsed;
            ProgressBar.IsIndeterminate = true; // Show progress during login

            try
            {
                string password = PasswordBox.Password;

                // Execute stored procedure asynchronously
                var user = await Task.Run(() =>
                {
                    return dbContext.Database.SqlQuery<User>("EXEC ValidateUser @password",
                        new SqlParameter("@password", password))
                        .FirstOrDefault();
                });

                if (user != null)
                {
                    // Set session data
                    SessionManager.Instance.UserId = user.UserId;

                    // Route to appropriate dashboard based on user type
                    if (user.UserType == "1")
                    {
                        await LoadAndShowDashboard(user);
                    }
                    else if (user.UserType == "2")
                    {
                        await LoadAndShowAdminDashboard(user);
                    }
                }
                else
                {
                    // Show error and reset UI
                    LoadingGrid.Visibility = Visibility.Collapsed;
                    LoginGrid.Visibility = Visibility.Visible;
                    ProgressBar.IsIndeterminate = false;
                    MessageBox.Show("Invalid PIN. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    PasswordBox.Password = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Handle errors and reset UI
                LoadingGrid.Visibility = Visibility.Collapsed;
                LoginGrid.Visibility = Visibility.Visible;
                ProgressBar.IsIndeterminate = false;
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadAndShowDashboard(User user)
        {
            var dataService = new DataService(dbContext);
            var dashboard = new Dashboard(dataService);

            // Minimal delay for transition (optional)
            await Task.Delay(100); // Quick transition effect

            dashboard.Show();
            Close();
        }

        private async Task LoadAndShowAdminDashboard(User user)
        {
            var adminDashboard = new Admin.AdminDashboard();

            // Minimal delay for transition (optional)
            await Task.Delay(100); // Quick transition effect

            adminDashboard.Show();
            Close();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                PasswordBox.Password += button.Content.ToString();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = string.Empty;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            timer.Stop(); // Clean up timer
        }
    }
}