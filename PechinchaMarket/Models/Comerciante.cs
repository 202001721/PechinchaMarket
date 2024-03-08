using PechinchaMarket.Areas.Identity.Data;
using System.Runtime.InteropServices;

namespace PechinchaMarket.Models
{
    /// <summary>
    /// Classe <c>Comerciante</c>
    /// </summary>
    public class Comerciante 

    {
        /// <summary>
        /// id da entrada na tabela
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// id que relaciona com o id de User
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Nome do comerciante 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// numero de telemovel para contactar o comerciante
        /// </summary>
        public int contact { get; set; }

        /// <summary>
        /// logotipo da empresa
        /// </summary>
        public byte[] logo { get; set; }

        /// <summary>
        /// documento que indica a autenticidade do comerciante
        /// </summary>
        public byte[] document { get; set; }

        /// <summary>
        /// boolean que determina se o utilizador está aprovado pelos moderadores
        /// </summary>
        public bool isApproved { get; set; }

        /// <summary>
        /// as suas lojas respetivas
        /// </summary>
        public List<Loja> Lojas { get; set; }
    }
}
