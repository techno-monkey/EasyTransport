﻿using trans =ET.Models.DataBase.Transport.Bus;
using Microsoft.EntityFrameworkCore;

namespace ET.Trans.Bus.DataBase
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> dbContext):base(dbContext)
        {

        }

        public DbSet<trans.Transporter> Transporters { get; set; }
        public DbSet<trans.Bus> Buses { get; set; }
    }
}
