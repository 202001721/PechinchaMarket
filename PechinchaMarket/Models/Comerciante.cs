using PechinchaMarket.Areas.Identity.Data;

namespace PechinchaMarket.Models
{
    public class Comerciante 

    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int contato { get; set; }
        public byte[] logo { get; set; }
    }
}
