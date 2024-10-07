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
                ViewBag.operacao = "I";

                JogosViewModel jogos = new JogosViewModel();
				jogos.dataAquicicao = DateTime.Now;

				JogoDAO dao = new JogoDAO();
				jogos.id = dao.ProximoID();

				return View("Form", jogos);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));

			}

		}
    
        public IActionResult Salvar(JogosViewModel jogo,string operacao)
        {
            try
            {
				JogoDAO dao = new JogoDAO();
				if (operacao == "I")
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
				ViewBag.operacao = "A";

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

		public IActionResult Delete(int id) {
			try{
				JogoDAO dao = new JogoDAO();
				dao.Excluir(id);
				return RedirectToAction("Index");
			}
			catch (Exception ex) { 
				return View("Error", new ErrorViewModel(ex.ToString()));
			}
		
		}

    }


}   
