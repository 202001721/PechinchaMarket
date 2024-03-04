using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PechinchaMarket.Models
{

    /**
     * Classe Produto
     * 
     * Id - id da entrada na tabela
     * Name - O nome do produto
     * Brand - O nome da marca do produto
     * Image - Uma imagem do produto
     * Weight - Peso/Quantidade depedendo to tipo de unidade do Produto
     * Unidade - Medida de Unidade do produto
     * ProdEstado - Estado de aprovação do produto
     * ProdCategoria - Categoria do Produto
     * ProdutoLojas - Uma lista de tabelas de relação Lojas-Produtos onde o produto se encontra
     */
    public class Produto
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Name {  get; set; }
        [Display(Name = "Marca")]
        public string Brand { get; set; }
        [Display(Name = "Imagem")]
        public byte[] Image { get; set; }
        [Display(Name = "Peso")]
        public float? Weight { get; set; }

        [EnumDataType(typeof(UnidadeMedida))]
        public UnidadeMedida Unidade { get; set; }
        [EnumDataType(typeof(Estado))]
        public Estado? ProdEstado { get; set; }

        [Display(Name = "Categoria")]
        [EnumDataType(typeof(Categoria))]
        public Categoria ProdCategoria { get; set; }

        public List<ProdutoLoja>? ProdutoLojas { get; set; }

    }
}
