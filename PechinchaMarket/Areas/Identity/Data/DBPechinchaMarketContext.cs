using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;

namespace PechinchaMarket.Areas.Identity.Data;

public class DBPechinchaMarketContext : IdentityDbContext<PechinchaMarketUser>
{
    public DBPechinchaMarketContext(DbContextOptions<DBPechinchaMarketContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
     

        base.OnModelCreating(builder);
      
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<PechinchaMarket.Models.Cliente> Cliente { get; set; } = default!;

public DbSet<PechinchaMarket.Models.Comerciante> Comerciante { get; set; } = default!;


public DbSet<PechinchaMarket.Models.Produto> Produto { get; set; } = default!;

public DbSet<PechinchaMarket.Models.Loja> Loja { get; set; } = default!;

public DbSet<PechinchaMarket.Models.ProdutoLoja> ProdutoLoja { get; set; } = default!;

public DbSet<PechinchaMarket.Models.ListaProdutos> ListaProdutos { get; set; } = default!;

}
