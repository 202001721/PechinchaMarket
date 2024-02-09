namespace PechinchaMarket.Models
{
    /**
     * Class ProdutoLoja
     * 
     * Id - id da entrada na tabela
     * Price - preço do produto na loja
     * Discount - Disconto do produto (opcional)
     * DiscountDuration - Tempo de duração do disconto
     * Produto - O produto que ira ser posto a venda na loja
     * Loja - A loja que ira por o produto a venda
     */
    public class ProdutoLoja
    {
        public int Id { get; set; }
        public float Price { get; set;}
        public float? Discount { get; set;}
        public DateTime? DiscountDuration { get; set;}

        public Produto Produto { get; set;}
        public Loja? Loja { get; set;}
    }
}
