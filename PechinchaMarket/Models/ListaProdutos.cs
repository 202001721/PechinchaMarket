using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    /// <summary>
    /// Lista de produtos de um cliente.
    /// </summary>
    public class ListaProdutos
    {
        /// <summary>
        /// identificador da entrada na tabela
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da lista
        /// </summary>
        [Display(Name = "Nome")]
        public string name { get; set; }

        /// <summary>
        /// id do cliente dono da lista
        /// </summary>
        public string ClienteId { get; set; }
        
        /// <summary>
        /// estado da lista
        /// </summary>
        [Display(Name = "Estado")]
        public EstadoProdutoCompra state { get; set; }

        /// <summary>
        /// produtos que fazem parte da lista
        /// </summary>
        public List<DetalheListaProd>? detalheListaProds { get; set; }

    }
}
