using Microsoft.AspNetCore.Mvc;
using SiteJogos.DAO;
using SiteJogos.Models;

namespace SiteJogos.Controllers
{
    public class JogoController : Controller
    {
        public IActionResult Index()
        {
            JogoDAO dao = new JogoDAO();
            List<JogosViewModel> lista = dao.Listagem();
            return View(lista);
        }

        public IActionResult Create()
        {
            JogosViewModel jogos = new JogosViewModel();
            jogos.dataAquicicao = DateTime.Now;
            return View("Form",jogos);
        }
    
        public IActionResult Salvar(JogosViewModel jogo)
        {
            JogoDAO dao = new JogoDAO();
            dao.Inserir(jogo);
            return RedirectToAction("index");
        }
    }


}   
