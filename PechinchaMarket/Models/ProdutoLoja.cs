namespace PechinchaMarket.Models
{
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
