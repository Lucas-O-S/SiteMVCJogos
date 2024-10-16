using Microsoft.AspNetCore.Mvc;
using SiteJogos.DAO;
using SiteJogos.Models;

namespace SiteJogos.Controllers
{
	public class PadraoController<T> : Controller where T : PadraoViewModel
	{
		protected PadraoDAO<T> dao { get; set; }
		protected bool GeraProximoId {  get; set; }
		protected string NomeViewIndex { get; set; } = "index";
		protected string NomeViewForm { get; set; } = "Form";
		public virtual IActionResult Index()
		{
			try
			{
				var lista = dao.Listagem();
				return View(NomeViewIndex, lista);

			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));
			}
		}

		public virtual IActionResult Create()
		{
			try
			{
				ViewBag.operacao = "I";
				T model = Activator.CreateInstance(typeof(T)) as T;
				PreencherDadosView("I", model);
				return View(NomeViewForm, model);
			}
			catch (Exception ex)
			{
				return View("Erro", new ErrorViewModel(ex.ToString()));
			}

		}

		protected virtual void PreencherDadosView(string operacao, T model)
		{
			if (GeraProximoId && operacao == "I")
				model.id = dao.ProximoId();
		}

		public virtual IActionResult Salvar(T model, string operacao)
		{
			try
			{
				ValidaDados(model, operacao);
				if (ModelState.IsValid == false)
				{
					ViewBag.operacao = operacao;
					PreencherDadosView(operacao, model);
					return View(NomeViewForm,model);
				}
				else
				{
					if (operacao == "I")
						dao.Insert(model);
					else
						dao.Update(model);
					return RedirectToAction(NomeViewIndex);
				}
			}
			catch (Exception ex)
			{
				return View("Erro", new ErrorViewModel(ex.ToString()));
			}
		}

		public virtual void ValidaDados(T model, string operacao)
		{
			ModelState.Clear();
			if (operacao == "I" && dao.Consulta(model.id) != null)
			{
				ModelState.AddModelError("Id", "Codigo já em uso");
			}
			if (operacao == "A" && dao.Consulta(model.id) == null)
			{
				ModelState.AddModelError("Id", "Codigo não existe");
			}
			if (model.id <= 0)
			{
				ModelState.AddModelError("Id", "Codigo deve ser maior que 0");
			}
		}

		public virtual IActionResult Edit(int id)
		{
			try
			{
				ViewBag.operacao = "A";
				var model = dao.Consulta(id);
				if (model == null)
					return RedirectToAction(NomeViewIndex);
				else
				{
					PreencherDadosView("A", model);
					return View(NomeViewForm, model);
				}

			}
			catch(Exception ex) 
			{
				return View("Erro", new ErrorViewModel(ex.ToString()));

			}
		}

		public IActionResult Delete(int id)
		{
			try
			{
				dao.Delete(id);
				return RedirectToAction(NomeViewIndex);
			}
			catch(Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));

			}
		}
	}
}
