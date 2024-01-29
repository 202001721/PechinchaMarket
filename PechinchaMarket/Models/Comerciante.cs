using PechinchaMarket.Areas.Identity.Data;

namespace PechinchaMarket.Models
{
    public class Comerciante : PechinchaMarketUser
    {
        public int contato { get; set; }
        public byte[] logo { get; set; }
    }
}
