using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    public class Loja
    {
        public Guid Id { get; set; }

        [Display(Name = "Morada")]
        public string adress { get; set; }

        [Display(Name = "Horário de Abertura")]
        [DataType(DataType.Time)]
        public DateTime OpeningTime { get; set; }

        [Display(Name = "Horário de Fecho")]
        [DataType(DataType.Time)]
        public DateTime ClosingTime { get; set; }

        public string UserId { get; set; }

        //public virtual required Comerciante Comerciante { get; set; }
    }
}
