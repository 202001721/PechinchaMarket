using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PechinchaMarket.Models
{

    /// <summary>
    /// classe de produto
    /// </summary>
    public class Produto
    {
        /// <summary>
        /// id da entrada na tabela
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// O nome do produto
        /// </summary>
        [Display(Name = "Nome")]
        public string Name {  get; set; }

        /// <summary>
        /// O nome da marca do produto
        /// </summary>
        [Display(Name = "Marca")]
        public string Brand { get; set; }

        /// <summary>
        /// Uma imagem do produto
        /// </summary>
        [Display(Name = "Imagem")]
        public byte[] Image { get; set; }

        /// <summary>
        /// Peso do Produto
        /// </summary>
        [Display(Name = "Peso")]
        public float? Weight { get; set; }

        /// <summary>
        /// Medida de Unidade do produto
        /// </summary>
        [EnumDataType(typeof(UnidadeMedida))]
        public UnidadeMedida Unidade { get; set; }

        /// <summary>
        ///  Estado de aprovação do produto
        /// </summary>
        [EnumDataType(typeof(Estado))]
        public Estado? ProdEstado { get; set; }

        /// <summary>
        /// Categoria do Produto
        /// </summary>
        [Display(Name = "Categoria")]
        [EnumDataType(typeof(Categoria))]
        public Categoria ProdCategoria { get; set; }

        /// <summary>
        /// Uma lista de tabelas de relação Lojas-Produtos onde o produto se encontra
        /// </summary>
        public List<ProdutoLoja>? ProdutoLojas { get; set; }

    }
}
