using PechinchaMarket.Areas.Identity.Data;
using System.Runtime.InteropServices;

namespace PechinchaMarket.Models
{
    /**
     * Classe Comerciante
     * 
     * Id - id da entrada na tabela
     * UserId - id que relaciona com o id de User
     * Contact - numero de telemovel do comerciante
     * Logo - logotipo da empresa
     * Document - documento que indica a autenticidade do comerciante
     * IsApproved - bool que determina se o utilizador está aprovado por moderadores
     */
    public class Comerciante 

    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int contact { get; set; }
        public byte[] logo { get; set; }
        public byte[] document { get; set; }
        public bool isApproved { get; set; }
    }
}
