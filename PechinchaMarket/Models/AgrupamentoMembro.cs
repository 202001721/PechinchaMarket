namespace PechinchaMarket.Models
{
    public class AgrupamentoMembro
    {
        public Guid ClienteId { get; set; }

        public Guid AgrupamentoId { get; set; }
        
        public NivelPrivilegio Privilegio { get; set; }
    }
}
