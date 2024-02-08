using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PechinchaMarket.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Name {  get; set; }
        [Display(Name = "Marca")]
        public string Brand { get; set; }
        [Display(Name = "Imagem")]
        public byte[]? Image { get; set; }
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
