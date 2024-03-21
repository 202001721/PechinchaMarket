namespace PechinchaMarket.Models
{
    /// <summary>
    /// Define o agrupamento de utilizadores que partilham listas entre si
    /// </summary>
    public class Agrupamento
    {
        /// <summary>
        /// Id da entrada na tabela
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do agrupamento
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Código do agrupamento que pode ser utilizado para o partilhar
        /// </summary>
        public long Codigo { get; set; }

        /// <summary>
        /// Lista de listas de produtos que são partilhadas
        /// </summary>
        public List<ListaProdutos>? ListaProdutos { get; set; }
    }
}
