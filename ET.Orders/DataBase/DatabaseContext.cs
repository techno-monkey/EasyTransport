using ET.Models.DataBase.Order;
using Microsoft.EntityFrameworkCore;

namespace ET.Orders.DataBase
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> dbContext):base(dbContext)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
