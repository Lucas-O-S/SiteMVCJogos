using Microsoft.AspNetCore.Mvc;
using SiteJogos.DAO;
using SiteJogos.Models;

namespace SiteJogos.Controllers
{
    public class JogoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
				JogoDAO dao = new JogoDAO();
				List<JogosViewModel> lista = dao.Listagem();
				return View(lista);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));

			}


		}

        public IActionResult Create()
        {
            try
            {
				JogosViewModel jogos = new JogosViewModel();
				jogos.dataAquicicao = DateTime.Now;
				return View("Form", jogos);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));

			}

		}
    
        public IActionResult Salvar(JogosViewModel jogo)
        {
            try
            {
				JogoDAO dao = new JogoDAO();
				dao.Inserir(jogo);
				return RedirectToAction("index");
			}
            catch(Exception ex)
            {
				return View("Error", new ErrorViewModel(ex.ToString()));

			}

		}
    }


}   
