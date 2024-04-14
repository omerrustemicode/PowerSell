using PowerSell.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell.Views.Admin
{
    public partial class Workers : UserControl
    {
        private PowerSellDbContext _context;
        private ObservableCollection<User> Users { get; set; }

        public Workers()
        {
            InitializeComponent();
            _context = new PowerSellDbContext(); // Initialize your DbContext
            LoadUsers();
        }

        private void LoadUsers()
        {
            Users = new ObservableCollection<User>(_context.Users.ToList());
            // Bind Users collection to your UI controls for listing users
             userList.ItemsSource = Users;
        }

        private void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            Users.Add(user);
        }

        private void EditUser(User user)
        {
            // Implement edit logic here
            _context.SaveChanges();
        }

        private void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            Users.Remove(user);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Create a new user and add it to the database
            User newUser = new User
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Password,
                Email = txtEmail.Text,
                RegisteredDate = DateTime.Now,
                LastLogin = DateTime.Now,
                UserType = cboUserType.SelectedValue?.ToString() // Assuming you have a ComboBox for UserType
            };

            AddUser(newUser);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected user and edit it
            User selectedUser = userList.SelectedItem as User;
            if (selectedUser != null)
            {
                selectedUser.UserName = txtEditUserName.Text;
                selectedUser.Password = txtEditPassword.Password;
                selectedUser.Email = txtEditEmail.Text;
                selectedUser.UserType = cboEditUserType.SelectedValue?.ToString(); // Assuming you have a ComboBox for UserType
                EditUser(selectedUser);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected user and delete it
            User selectedUser = userList.SelectedItem as User;
            if (selectedUser != null)
            {
                DeleteUser(selectedUser);
            }
        }
    }
}
