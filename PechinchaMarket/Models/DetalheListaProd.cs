namespace PechinchaMarket.Models
{
    public class DetalheListaProd
    {
        public Guid Id { get; set; }
        public int quantity { get; set; }
        public string ListaProdutosId { get; set; }
        public string ProdutoId { get; set; }
    }
}
