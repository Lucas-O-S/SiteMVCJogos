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
				ValidarDados(jogo,operacao);
				if (ModelState.IsValid == false)
				{
					ViewBag.operacao = operacao;
					return View("form",jogo);


				}
				else
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

		private void ValidarDados(JogosViewModel jogo, string operacao)
		{
			ModelState.Clear();
			JogoDAO dao = new JogoDAO();
			if (operacao == "I" && dao.Consulta(jogo.id) != null)
			{
				ModelState.AddModelError("Id","Codigo já em uso");
			}
			if (operacao == "A" && dao.Consulta(jogo.id) == null)
			{
				ModelState.AddModelError("Id", "Codigo não existe");
			}
			if (jogo.id <= 0)
			{
				ModelState.AddModelError("Id", "Codigo deve ser maior que 0");
			}
			if (jogo.valorLocacao <= 0 || jogo.valorLocacao ==null )
			{
				ModelState.AddModelError("valorLocacao", "valor invalido");

			}
			if (String.IsNullOrEmpty(jogo.descricao))
			{
				ModelState.AddModelError("descricao", "Descrição está vazia");

			}

			if (jogo.idCategoria <= 0 || jogo.idCategoria == null )
			{
				ModelState.AddModelError("idCategoria", "valor invalido");

			}
			if (jogo.dataAquicicao > DateTime.UtcNow)
			{
				ModelState.AddModelError("dataAquicicao", "Valor invalido");

			}

		}

    }


}   
