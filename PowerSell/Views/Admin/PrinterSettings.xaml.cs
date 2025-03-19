using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System;

namespace PowerSell.Views.ClientView
{
    public partial class PrinterSettings : UserControl
    {
        private PrinterSettingsModel settings;

        public PrinterSettings()
        {
            InitializeComponent();
            settings = PrinterSettingsManager.LoadSettings();
            LoadPrinters();
            PopulateComboBoxes();
        }

        private void LoadPrinters()
        {
            // Get all installed printers
            LocalPrintServer printServer = new LocalPrintServer();
            PrintQueueCollection printers = printServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            List<string> printerNames = printers.Select(p => p.FullName).ToList();

            //// Optionally add a network printer if reachable
            //string customPrinterIP = "192.168.1.100";
            //if (!string.IsNullOrEmpty(customPrinterIP))
            //{
            //    try
            //    {
            //        Ping ping = new Ping();
            //        PingReply reply = ping.Send(customPrinterIP);
            //        if (reply.Status == IPStatus.Success)
            //        {
            //            printerNames.Add($"Network Printer ({customPrinterIP})");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Error pinging {customPrinterIP}: {ex.Message}");
            //    }
            //}

            // Set ItemsSource for both ComboBoxes
            PrintOrderComboBox.ItemsSource = printerNames;
            PrintFiscComboBox.ItemsSource = printerNames;

            // Ensure default selections are applied after loading
            PopulateComboBoxes();

            // Debug: Show available printers
            MessageBox.Show($"Available printers: {string.Join(", ", printerNames)}");
        }

        private void PopulateComboBoxes()
        {
            // Set the selected item based on saved settings, or default to the first printer if settings are null or not found
            PrintOrderComboBox.SelectedItem = settings.PrintOrderPrinter != null && printerContains(settings.PrintOrderPrinter)
                ? settings.PrintOrderPrinter
                : PrintOrderComboBox.Items.Cast<string>().FirstOrDefault();
            PrintFiscComboBox.SelectedItem = settings.PrintFiscPrinter != null && printerContains(settings.PrintFiscPrinter)
                ? settings.PrintFiscPrinter
                : PrintFiscComboBox.Items.Cast<string>().FirstOrDefault();
        }

        // Helper method to check if a printer name exists in the current list
        private bool printerContains(string printerName)
        {
            return PrintOrderComboBox.Items.Cast<string>().Contains(printerName);
        }

        private void PrintOrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: Add logic here if you want to react to selection changes
        }

        private void PrintFiscComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: Add logic here if you want to react to selection changes
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedOrderPrinter = PrintOrderComboBox.SelectedItem?.ToString() ?? "";
            string selectedFiscPrinter = PrintFiscComboBox.SelectedItem?.ToString() ?? "";

            MessageBox.Show($"Saving PrintOrderPrinter: {selectedOrderPrinter}\nSaving PrintFiscPrinter: {selectedFiscPrinter}");

            settings.PrintOrderPrinter = selectedOrderPrinter;
            settings.PrintFiscPrinter = selectedFiscPrinter;
            PrinterSettingsManager.SaveSettings(settings);
            MessageBox.Show("Printer settings saved successfully!");
        }
    }
}