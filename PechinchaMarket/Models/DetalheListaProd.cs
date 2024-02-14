namespace PechinchaMarket.Models
{
    public class DetalheListaProd
    {
        public Guid Id { get; set; }
        public int quantity { get; set; }

        public ListaProdutos ListaProdutos { get; set; }
        public ProdutoLoja ProdutoLoja { get; set; } 
    }
}
