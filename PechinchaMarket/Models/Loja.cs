using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    /// <summary>
    /// Loja de um comerciante.
    /// </summary>
    public class Loja
    {
        /// <summary>
        /// id da entrada na tabela
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// morada da loja do comerciante
        /// </summary>
        [Display(Name = "Morada")]
        public string Address { get; set; }

        /// <summary>
        /// Horário de abertura da loja
        /// </summary>
        [Display(Name = "Horário de Abertura")]
        [DataType(DataType.Time)]
        public DateTime OpeningTime { get; set; }

        /// <summary>
        /// Horário de fecho da loja
        /// </summary>
        [Display(Name = "Horário de Fecho")]
        [DataType(DataType.Time)]
        public DateTime ClosingTime { get; set; }

        /// <summary>
        /// id que relaciona com o id de User
        /// </summary>
        public string UserId { get; set; }
    }
}
