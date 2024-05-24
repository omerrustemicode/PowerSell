using PowerSell.Models;
using PowerSell.Models.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Services
{
    public class DataService
    {
        private readonly PowerSellDbContext _context;

        public DataService(PowerSellDbContext context)
        {
            _context = context;
        }

        public List<TableOrderDetailView> GetTableOrderDetails()
        {
            var sql = "SELECT * FROM vw_TableOrderDetails";
            return _context.Database.SqlQuery<TableOrderDetailView>(sql).ToList();
        }

        public TableOrderDetailView GetTableOrderDetailById(int tableId)
        {
            var sql = "SELECT * FROM vw_TableOrderDetails WHERE TableId = @p0";
            return _context.Database.SqlQuery<TableOrderDetailView>(sql, tableId).FirstOrDefault();
        }
    }


}
