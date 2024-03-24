using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PowerSell.Models
{
    public class OrderManager
    {
        private PowerSellDbContext dbContext;

        public OrderManager(PowerSellDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void PrintServiceClick(int tableId, int userId, IEnumerable<Service> services)
        {
            try
            {
                foreach (var service in services)
                {
                    dbContext.Orders.Add(new Orders
                    {
                        OrdersId = service.ServiceId,
                        ServiceId = service.ServiceId,
                        Quantity = service.Quantity,
                        ServicePrice = service.ServicePrice,
                        TableId = tableId,
                        ServiceDateCreated = service.ServiceDateCreated,
                        IsReady = 0,
                        IsPaid = false,
                        ServiceDIscount = 0,
                        ClientGetService = false,
                        Total = service.Total,
                        UserId = userId
                    });
                }

                dbContext.SaveChanges();  // Save changes to the database
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving orders: " + ex.Message);
            }
        }
    }
}
