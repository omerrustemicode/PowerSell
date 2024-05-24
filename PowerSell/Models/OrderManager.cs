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

        public void PrintServiceClick(int tableId, int userId, IEnumerable<Service> services, int orderListId)
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
                        Total = service.Total,
                        UserId = userId,
                        OrderListId = orderListId // Assign the OrderListId here
                    });
                }

                dbContext.SaveChanges();  // Save changes to the database
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving orders: " + ex.Message);
            }
        }
     
    }
}
