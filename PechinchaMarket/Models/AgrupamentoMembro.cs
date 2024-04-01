namespace PechinchaMarket.Models
{
    /// <summary>
    /// Define a relação entre um cliente e um agrupamento a que pertence
    /// </summary>
    public class AgrupamentoMembro
    {
        /// <summary>
        /// id da entrada na tabela
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Cliente associado
        /// </summary>
        public Cliente Cliente { get; set; }

        /// <summary>
        /// Agrupamento associado
        /// </summary>
        public Agrupamento Agrupamento { get; set; }
        
        /// <summary>
        /// Privilegios que o utilizador tem sobre o agrupamento respetivo
        /// </summary>
        public NivelPrivilegio Privilegio { get; set; }
    }
}
