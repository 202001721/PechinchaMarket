namespace PechinchaMarket.Models
{
    public class Cliente : UtilizadorPMK
    {
        public List<Categoria> categoria { get; set; }
        public string localizacao { get; set; }
}
}
