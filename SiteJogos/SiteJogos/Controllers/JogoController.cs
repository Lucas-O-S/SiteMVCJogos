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
				if (dao.Consulta(jogo.id) == null)
				{
					dao.Inserir(jogo);
				}
				else
					dao.Alterar(jogo);
				
				return RedirectToAction("index");
				

			}
            catch(Exception ex)
            {
				return View("Error", new ErrorViewModel(ex.ToString()));

			}

		}

		public IActionResult Edit(int id)
		{
			try
			{
				JogoDAO dao = new JogoDAO();
				JogosViewModel jogo = dao.Consulta(id);
				if (jogo == null)
				{
					return RedirectToAction("Index");
				}
				else
					return View("Form",jogo);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));
			}
		}
    }


}   
