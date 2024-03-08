namespace PechinchaMarket.Models
{
    public class Agrupamento
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int Codigo { get; set; }

        public List<ListaProdutos>? ListaProdutos { get; set; }
    }
}
