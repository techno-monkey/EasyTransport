using ET.Models.DataBase.Transport.Bus;
using Microsoft.EntityFrameworkCore;

namespace ET.Trans.Bus.DataBase
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> dbContext):base(dbContext)
        {

        }

        public DbSet<Transporter> Transporters { get; set; }
        public DbSet<ET.Models.DtoModels.Bus.Bus> Buses { get; set; }
    }
}
