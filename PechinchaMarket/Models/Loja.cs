namespace PechinchaMarket.Models
{
    public class Loja
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime Open {  get; set; }
        public DateTime Closed { get; set; }

        public Comerciante Comerciante { get; set; }
        public List<ProdutoLoja> ProdutoLojas { get; set; }
    }
}
