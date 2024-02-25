using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Button = System.Windows.Controls.Button;

namespace PowerSell.Views.ClientView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeyboardWindow : Window
    {
        public KeyboardWindow()
        {
            InitializeComponent();
        }

        private void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle virtual keyboard button click

            var button = (Button)sender;
            descriptionTextBox.Text += button.Content.ToString();
        }


        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            descriptionTextBox.Text += "\n";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Delete button click
            if (!string.IsNullOrEmpty(descriptionTextBox.Text))
            {
                descriptionTextBox.Text = descriptionTextBox.Text.Substring(0, descriptionTextBox.Text.Length - 1);
            }
        }

        private void Suugested1_Click(object sender, RoutedEventArgs e)
        {
            // Handle sample button click
            var button = (Button)sender;
            descriptionTextBox.Text += button.Content.ToString();
        }

        private void Suugested2_Click(object sender, RoutedEventArgs e)
        {
            // Handle additional sample button click
            var button = (Button)sender;
            descriptionTextBox.Text += button.Content.ToString();
        }

        private void Suugested3_Click(object sender, RoutedEventArgs e)
        {
            // Handle additional sample button click
            var button = (Button)sender;
            descriptionTextBox.Text += button.Content.ToString();
        }

        private void SuugestedDay_Click(object sender, RoutedEventArgs e)
        {
            // Handle additional sample button click
            var button = (Button)sender;
            descriptionTextBox.Text += button.Content.ToString();
        }
        private void DescriptionTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            // Get the current text from the TextBox
            string text = descriptionTextBox.Text;

            // Split the text into words
            string[] words = text.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            // Check if the number of words exceeds the limit (e.g., 30)
            if (words.Length > 30)
            {
                // If it exceeds, show an error message
                errorTextBlock.Text = "Maximum 30 words allowed.";

                // Trim the text to the last 30 words
                string trimmedText = string.Join(" ", words.Skip(words.Length - 31));
                descriptionTextBox.Text = trimmedText;
            }
            else
            {
                // Clear the error message if within the limit
                errorTextBlock.Text = string.Empty;
            }
        }




    }
}
