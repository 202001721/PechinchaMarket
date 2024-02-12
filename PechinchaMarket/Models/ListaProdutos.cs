using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    public class ListaProdutos
    {
        public int Id { get; set; }

        public Cliente? cliente;
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }
        [Display(Name = "Estado")]
        public EstadoProdutoCompra estado;
        public List<Produto>? Produtos { get; set; }

    }
}
