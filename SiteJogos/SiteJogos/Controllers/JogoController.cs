using Microsoft.AspNetCore.Mvc;
using SiteJogos.DAO;
using SiteJogos.Models;

namespace SiteJogos.Controllers
{
    public class JogoController : PadraoController<JogosViewModel>
    {
		public JogoController() {
			dao = new JogoDAO();
			GeraProximoId = true;
		}

        public override IActionResult Create()
        {
            try
            {

				ViewBag.operacao = "I";

				JogosViewModel jogos = new JogosViewModel();
				jogos.dataAquicicao = DateTime.Now;

				PreencherDadosView("I", jogos);

				CategoriaDAO catDAO = new CategoriaDAO();
				ViewBag.categorias = catDAO.Listagem();


				return View("Form", jogos);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));

			}

		}
    
        public override IActionResult Salvar(JogosViewModel jogo,string operacao)
        {
            try
            {

				ValidarDados(jogo, operacao);
				if (ModelState.IsValid == false)
				{
					ViewBag.operacao = operacao;

					CategoriaDAO catDAO = new CategoriaDAO();
					ViewBag.categorias = catDAO.Listagem();

					return View("form", jogo);


				}
				else
				{
					JogoDAO dao = new JogoDAO();
					if (operacao == "I")
					{
						dao.Insert(jogo);
					}
					else
						dao.Update(jogo);
					return RedirectToAction("index");

				}






			}
			catch (Exception ex)
            {
				return View("Error", new ErrorViewModel(ex.ToString()));

			}

		}

		public override IActionResult Edit(int id)
		{
			try
			{
				ViewBag.operacao = "A";

				CategoriaDAO catDAO = new CategoriaDAO();
				ViewBag.categorias = catDAO.Listagem();

				JogoDAO dao = new JogoDAO();
				JogosViewModel jogo = dao.Consulta(id);
				if (jogo == null)
				{
					return RedirectToAction("Index");
				}
				else
					return View("Form", jogo);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));
			}
		}



		private void ValidarDados(JogosViewModel jogo, string operacao)
		{

			base.ValidaDados(jogo, operacao);
			
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
