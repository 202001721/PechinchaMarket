using PechinchaMarket.Areas.Identity.Data;

namespace PechinchaMarket.Models
{
    public class Cliente : PechinchaMarketUser
    {
        public List<Categoria> preferecias { get; set; }
        public string localizacao { get; set; }


}
}
