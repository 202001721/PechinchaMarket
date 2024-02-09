using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PechinchaMarketTest
{
    public class ApplicationDbContextFixture : IDisposable
    {
        public DBPechinchaMarketContext DbContext { get; set; }

        public ApplicationDbContextFixture() {
            
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DBPechinchaMarketContext>()
                    .UseSqlite("DataSource=:memory:")
                    .Options;
            DbContext = new DBPechinchaMarketContext(options);
            DbContext.Database.EnsureCreated();


            DbContext.SaveChanges();
        }

        public void Dispose() => DbContext.Dispose();
    }
}
