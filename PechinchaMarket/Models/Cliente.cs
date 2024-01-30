using PechinchaMarket.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    public class Cliente
    {
  
        public Guid Id { get; set; }
        public string UserId { get; set; }

        [Required]
        [EnumDataType(typeof(Categoria))]
        public List<Categoria> Preferecias { get; set; }

        public string Localizacao { get; set; }


}
}
