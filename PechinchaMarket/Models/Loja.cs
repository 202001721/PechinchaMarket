using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    public class Loja
    {
        public Guid Id { get; set; }
        public string adress { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OpeningTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ClosingTime { get; set; }
    }
}
