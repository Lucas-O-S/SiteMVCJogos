namespace SiteJogos.Models
{
    public class JogosViewModel : PadraoViewModel
    {
        public string? descricao { get; set; }
        public decimal valorLocacao { get; set; }
        public DateTime dataAquicicao { get; set; }
        public int idCategoria { get; set; }

        public string nomeCategoria { get; set; }
    }
}
