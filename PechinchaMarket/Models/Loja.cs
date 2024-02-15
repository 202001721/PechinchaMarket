using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    /**
     * Classe Loja
     * 
     * Id - id da entrada na tabela
     * UserId - id que relaciona com o id de User
     * Address - morada da loja do comerciante
     * OpeningTime - Horário de abertura da loja
     * ClosingTime - Horário de fecho da loja
     */
    public class Loja
    {
        public Guid Id { get; set; }

        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Display(Name = "Horário de Abertura")]
        [DataType(DataType.Time)]
        public DateTime OpeningTime { get; set; }

        [Display(Name = "Horário de Fecho")]
        [DataType(DataType.Time)]
        public DateTime ClosingTime { get; set; }

        public string UserId { get; set; }
    }
}
