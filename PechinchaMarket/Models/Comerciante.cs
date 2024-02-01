using PechinchaMarket.Areas.Identity.Data;
using System.Runtime.InteropServices;

namespace PechinchaMarket.Models
{
    public class Comerciante 

    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int contato { get; set; }
        public byte[] logo { get; set; }
        public byte[] document { get; set; }
        public bool isApproved { get; set; }
    }
}
