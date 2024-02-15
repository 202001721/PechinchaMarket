using PechinchaMarket.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{
    /**
     * Classe Cliente
     * 
     * Id - id da entrada na tabela
     * UserId - id que relaciona com o id de User
     * Preferencias - Lista de categorias que definem as preferencias do cliente
     * Localizacao - localização do cliente
     */
    public class Cliente
    {
        public Guid Id { get; set; }

        public string Name { get; set; } 
        public string UserId { get; set; }
        [EnumDataType(typeof(Categoria))]
        public List<Categoria>? Preferencias { get; set; }
        public string Localizacao { get; set; }
    }
}
