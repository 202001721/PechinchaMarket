using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    /// <summary>
    /// Classe para um produto de uma loja especifica.
    /// </summary>
    public class ProdutoLoja
    {
        /// <summary>
        /// id da entrada na tabela
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// preço do produto na loja
        /// </summary>
        [Display(Name = "Preço")]
        public float Price { get; set;}

        /// <summary>
        /// Disconto do produto (opcional)
        /// </summary>
        [Display(Name = "Desconto")]
        public float? Discount { get; set;}

        [Display(Name = "Inicio do desconto")]
        [DataType(DataType.Date)]
        public DateTime? StartDiscount { get; set;}
        [Display(Name = "Fim do desconto")]
        [DataType(DataType.Date)]
        public DateTime? EndDiscount { get; set; }


        /// <summary>
        /// O produto que ira ser posto a venda na loja
        /// </summary>
        public Produto Produto { get; set;}

        /// <summary>
        /// A loja que ira por o produto a venda
        /// </summary>
        public Loja? Loja { get; set;}
    }
}
