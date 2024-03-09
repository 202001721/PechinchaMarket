namespace PechinchaMarket.Models
{
    /// <summary>
    /// Classe de relação entre um <c>ProdutoLoja</c> e uma <c>ListaProdutos</c>. 
    /// Isto é, um produto de uma loja especifica com colocado dentro de uma lista do cliente.
    /// </summary>
    public class DetalheListaProd
    {
        /// <summary>
        /// identificador da entrada na tabela
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// quantidade dod produto na lista
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// a lista a que o produto pertence
        /// </summary>
        public ListaProdutos ListaProdutos { get; set; }

        /// <summary>
        /// o produto de uma loja especifica
        /// </summary>
        public ProdutoLoja ProdutoLoja { get; set; } 

    }
}
