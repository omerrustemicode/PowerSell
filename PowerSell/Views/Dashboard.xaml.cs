using PowerSell.Models;
using PowerSell.Views.ClientView;
using PowerSell.Views.ToGo;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using PowerSell.Services;
using MahApps.Metro.Controls;
using PowerSell.Views.Account;

namespace PowerSell.Views
{
    public partial class Dashboard : MetroWindow
    {
        public ObservableCollection<Tables> Tables { get; set; } = new ObservableCollection<Tables>();
        private DispatcherTimer colorTimer;
        private DispatcherTimer tableTimer;
        private readonly DataService _dataService;
        private Point? dragStartPoint = null;
        private bool isTablesLocked = true;

        public Dashboard(DataService dataService)
        {
            InitializeComponent();
            this.Loaded += Dashboard_Loaded;
            _dataService = dataService;
            DataContext = this;
        }

        private void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTablesFromDatabase();
            UpdateButtonColors();
            StartColorUpdateTimer();
            StartTableUpdateTimer();
            LockUnlockButton.Content = "Unlock Tables";
        }

        public void LoadTablesFromDatabase()
        {
            Console.WriteLine("Loading tables from database...");
            var tableDetails = _dataService.GetTableOrderDetails();
            Tables.Clear();

            int columnsPerRow = 4;
            double xOffset = 10;
            double yOffset = 10;
            double buttonWidth = 110;
            double buttonHeight = 90;
            int columnIndex = 0;
            int rowIndex = 0;

            foreach (var detail in tableDetails)
            {
                Console.WriteLine($"Loaded TableId: {detail.TableId}, Name: {detail.TableName}, X: {detail.XPosition}, Y: {detail.YPosition}");
                var table = new Tables
                {
                    TableId = detail.TableId,
                    TableName = detail.TableName,
                    XPosition = detail.XPosition ?? (xOffset + columnIndex * buttonWidth),
                    YPosition = detail.YPosition ?? (yOffset + rowIndex * buttonHeight)
                };

                if (detail.TotalSumForTable > 0)
                {
                    table.OrderList = new ObservableCollection<OrderList>
                    {
                        new OrderList
                        {
                            Total = detail.TotalSumForTable ?? 0,
                            IsReady = detail.IsReady,
                            ClientName = $"{detail.ClientName}\n{detail.ClientPhone}"
                        }
                    };
                }

                Tables.Add(table);

                if (detail.XPosition == null || detail.YPosition == null)
                {
                    Console.WriteLine($"No saved position for TableId: {detail.TableId}, using default: X={table.XPosition}, Y={table.YPosition}");
                    columnIndex++;
                    if (columnIndex >= columnsPerRow)
                    {
                        columnIndex = 0;
                        rowIndex++;
                    }
                }
            }

            tablesListBox.ItemsSource = null;
            tablesListBox.ItemsSource = Tables;

            tablesListBox.UpdateLayout();
            foreach (var table in Tables)
            {
                var item = tablesListBox.ItemContainerGenerator.ContainerFromItem(table) as ListBoxItem;
                if (item != null && table.XPosition.HasValue && table.YPosition.HasValue)
                {
                    Canvas.SetLeft(item, table.XPosition.Value);
                    Canvas.SetTop(item, table.YPosition.Value);
                 

                    Console.WriteLine($"Applied UI position for TableId: {table.TableId}, X: {table.XPosition}, Y: {table.YPosition}");
                }
            }

            Console.WriteLine("Tables loaded and positioned.");
        }

        private void StartColorUpdateTimer()
        {
            colorTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            colorTimer.Tick += ColorTimer_Tick;
            colorTimer.Start();
        }

        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            UpdateButtonColors();
        }

        private void StartTableUpdateTimer()
        {
            tableTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            tableTimer.Tick += TableTimer_Tick;
            tableTimer.Start();
        }

        private void TableTimer_Tick(object sender, EventArgs e)
        {
            LoadTablesFromDatabase();
            UpdateButtonColors();
        }

        private void UpdateButtonColors()
        {
            using (var dbContext = new PowerSellDbContext())
            {
                foreach (var table in Tables)
                {
                    var ordersForTable = dbContext.Orders.Where(o => o.TableId == table.TableId).ToList();
                    var orderListIds = ordersForTable.Select(o => o.OrderListId).ToList();
                    var orderListForTable = dbContext.OrderList
                        .Where(ol => orderListIds.Contains(ol.OrderListId))
                        .ToList();

                    var container = tablesListBox.ItemContainerGenerator.ContainerFromItem(table) as ListBoxItem;
                    var button = container != null ? FindVisualChild<Button>(container) : null;

                    if (button != null)
                    {
                        if (orderListForTable.Any(ol => ol.IsReady == 0 && ol.ClientGetService == null && ol.IsPaid == null))
                            button.Background = Brushes.Red;
                        else if (orderListForTable.Any(ol => ol.IsReady == 1 && ol.ClientGetService == null && ol.IsPaid == null))
                            button.Background = Brushes.Green;
                        else if (orderListForTable.Any(ol => ol.IsReady == 0 && ol.ClientGetService == null && ol.IsPaid == true))
                            button.Background = CustomColorHelper.CreateRedGreenGradientBrush();
                        else if (orderListForTable.Any(ol => ol.IsReady == 1 && ol.ClientGetService == null && ol.IsPaid == true))
                            button.Background = CustomColorHelper.CreateYellowGreenGradientBrush();
                        else
                            button.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                    }
                }
            }
        }

        private void TableItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isTablesLocked) return;

            var item = sender as ListBoxItem;
            if (item != null)
            {
                dragStartPoint = e.GetPosition(tablesListBox);
                item.CaptureMouse();
                e.Handled = true;
            }
        }

        private void TableItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (isTablesLocked || dragStartPoint == null || !(sender is ListBoxItem item)) return;

            var currentPosition = e.GetPosition(tablesListBox);
            var offset = currentPosition - dragStartPoint.Value;

            if (e.LeftButton == MouseButtonState.Pressed && 
                (Math.Abs(offset.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(offset.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                var table = item.DataContext as Tables;
                if (table != null)
                {
                    table.XPosition = (table.XPosition ?? 0) + offset.X;
                    table.YPosition = (table.YPosition ?? 0) + offset.Y;

                    table.XPosition = Math.Max(0, Math.Min(table.XPosition.Value, tablesListBox.ActualWidth - item.ActualWidth));
                    table.YPosition = Math.Max(0, Math.Min(table.YPosition.Value, tablesListBox.ActualHeight - item.ActualHeight));

                    Canvas.SetLeft(item, table.XPosition.Value);
                    Canvas.SetTop(item, table.YPosition.Value);

                    dragStartPoint = currentPosition;
                }
            }
        }

        private void TableItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isTablesLocked || !(sender is ListBoxItem item)) return;

            item.ReleaseMouseCapture();
            dragStartPoint = null;

            var table = item.DataContext as Tables;
            if (table != null)
            {
                SaveTablePosition(table);
            }
        }

        private void LockUnlockButton_Click(object sender, RoutedEventArgs e)
        {
            isTablesLocked = !isTablesLocked;
            LockUnlockButton.Content = isTablesLocked ? "Unlock Tables" : "Lock Tables";
        }

        private void SaveTablePositionButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Save Table Position button clicked.");
            int savedCount = 0;

            foreach (var table in Tables)
            {
                Console.WriteLine($"Attempting to save TableId: {table.TableId}, X: {table.XPosition}, Y: {table.YPosition}");
                if (SaveTablePosition(table))
                    savedCount++;
            }

            MessageBox.Show($"Saved {savedCount} table positions successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool SaveTablePosition(Tables table)
        {
            try
            {
                using (var dbContext = new PowerSellDbContext())
                {
                    var dbTable = dbContext.Tables.FirstOrDefault(t => t.TableId == table.TableId);
                    if (dbTable != null)
                    {
                        dbTable.XPosition = table.XPosition;
                        dbTable.YPosition = table.YPosition;
                        int changes = dbContext.SaveChanges();
                        Console.WriteLine($"TableId: {table.TableId} updated. Rows affected: {changes}");
                        return changes > 0;
                    }
                    else
                    {
                        Console.WriteLine($"TableId: {table.TableId} not found in database.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving TableId: {table.TableId} - {ex.Message}");
                return false;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Refresh button clicked.");
            LoadTablesFromDatabase();
            UpdateButtonColors();
        }

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Tables tableModel)
            {
                SingleClientWindow singleClientWindow = new SingleClientWindow(tableModel.TableId);
                singleClientWindow.Show();
                singleClientWindow.Closed += SingleClientWindow_Closed;
            }
        }

        private void SingleClientWindow_Closed(object sender, EventArgs e)
        {
            LoadTablesFromDatabase();
            UpdateButtonColors();
        }

        private void ToGoButton_Click(object sender, RoutedEventArgs e)
        {
            var toGoWindow = new ToGoWindow();
            toGoWindow.ShowDialog();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.Reports reportsDialog = new Reports.Reports();
            reportsDialog.ShowDialog();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.Instance.UserId = 0;
            var login = new Login();
            login.Show();
            Close();
        }

        public void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            ReturnProduct returnProd = new ReturnProduct();
            returnProd.ShowDialog();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            var filteredTables = Tables.Where(table => table.TableName.ToLower().Contains(searchText)).ToList();
            tablesListBox.ItemsSource = filteredTables;
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                T childItem = FindVisualChild<T>(child);
                if (childItem != null)
                    return childItem;
            }
            return null;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            colorTimer?.Stop();
            tableTimer?.Stop();
        }
    }
}