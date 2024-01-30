using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PechinchaMarket.Models;

namespace PechinchaMarket.Areas.Identity.Data;

// Add profile data for application users by adding properties to the PechinchaMarketUser class
public class PechinchaMarketUser : IdentityUser
{
}

public class Cliente : PechinchaMarketUser
{
    public List<Categoria> categoria { get; set; }
    public string localizacao { get; set; }
}

public class Comerciante : PechinchaMarketUser
{
    public int contato { get; set; }
    public byte[] logo { get; set; }
}
