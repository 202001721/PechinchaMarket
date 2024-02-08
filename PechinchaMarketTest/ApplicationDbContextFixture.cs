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
        public DBPechinchaMarketContext DbContext { get; private set; }

        public ApplicationDbContextFixture() {
            
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DBPechinchaMarketContext>()
                    .UseSqlite("DataSource=:memory:")
                    .Options;
            DbContext = new DBPechinchaMarketContext(options);
            DbContext.Database.EnsureCreated();

            // Adicione uma nova liga para teste
            var user = CreateUser();
            
            DbContext.Cliente.Add(new Cliente { Id=Guid.NewGuid(), UserId="", Localizacao="Lisboa", Preferencias=new List<Categoria>() });

            DbContext.SaveChanges();
        }

        public void Dispose() => DbContext.Dispose();
    }
}
