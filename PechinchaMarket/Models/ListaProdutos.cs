using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    public class ListaProdutos
    {
        public Guid Id { get; set; }
        [Display(Name = "Nome")]
        public string name { get; set; }

        public string ClienteId { get; set; }
        
        [Display(Name = "Estado")]
        public EstadoProdutoCompra state { get; set; }

        public List<DetalheListaProd>? detalheListaProds { get; set; }

        //public List<ProdutoLoja> produtoLojas { get; set; }
    }
}
