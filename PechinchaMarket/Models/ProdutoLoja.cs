using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Preço")]
        public float Price { get; set;}
        [Display(Name = "Desconto")]
        public float? Discount { get; set;}
        [Display(Name = "Inicio do desconto")]
        [DataType(DataType.Date)]
        public DateTime? StartDiscount { get; set;}
        [Display(Name = "Fim do desconto")]
        [DataType(DataType.Date)]
        public DateTime? EndDiscount { get; set; }

        public Produto Produto { get; set;}
        public Loja? Loja { get; set;}
    }
}
