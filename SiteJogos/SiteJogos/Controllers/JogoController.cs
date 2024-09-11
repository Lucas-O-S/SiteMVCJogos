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
    }
}
