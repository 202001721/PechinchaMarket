namespace PechinchaMarket.Models
{
    public class AgrupamentoMembro
    {
        public Guid Id { get; set; }

        public Cliente Cliente { get; set; }

        public Agrupamento Agrupamento { get; set; }
        
        public NivelPrivilegio Privilegio { get; set; }
    }
}
