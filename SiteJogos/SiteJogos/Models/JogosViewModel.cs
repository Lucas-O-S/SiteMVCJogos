namespace SiteJogos.Models
{
    public class JogosViewModel
    {
        public int id { get; set; }
        public string? descricao { get; set; }
        public decimal valorLocacao { get; set; }
        public DateTime dataAquicicao { get; set; }
        public int idCategoria { get; set; }
    }
}
