using PechinchaMarket.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PechinchaMarket.Models
{

    /// <summary>
    /// Define a informação necessária do cliente da plataforma.
    /// </summary>
    public class Cliente
    {
        /// <summary> id da entrada na tabela</summary>
        public Guid Id { get; set; }

        /// <summary> nome do cliente</summary>
        public string Name { get; set; }

        /// <summary> id que relaciona com o id de User</summary>
        public string UserId { get; set; }

        /// <summary> Lista de categorias que definem as preferencias do cliente</summary>
        [EnumDataType(typeof(Categoria))]
        public List<Categoria>? Preferencias { get; set; }

        /// <summary> localização do cliente</summary>
        public string Localizacao { get; set; }
    }
}
