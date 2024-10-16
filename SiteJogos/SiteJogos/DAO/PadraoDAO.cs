using SiteJogos.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace SiteJogos.DAO
{
	public abstract class PadraoDAO<T> where T: PadraoViewModel
	{
		public PadraoDAO()
		{
			SetTabela();
		}

		protected string tabela {  get; set; }
		protected string NomeSpListagem { get; set; } = "spLista";
		protected abstract SqlParameter[] CriarParametros(T model);
		protected abstract T MontarModel(DataRow registro);
		protected abstract void SetTabela();

		public virtual void Insert(T model)
		{
			HelperDAO.ExecutaProc("spInclui" + tabela, CriarParametros(model));
		}
		public virtual void Update(T model)
		{
			HelperDAO.ExecutaProc("spAltera" + tabela, CriarParametros(model));
		}

		public virtual void Delete(int id)
		{
			var p = new SqlParameter[]
			{
				new SqlParameter("id", id),
				new SqlParameter("tabela", tabela)
			};
			HelperDAO.ExecutaProc("spDelete",p);
		}

		public virtual T Consulta(int id)
		{
			var p = new SqlParameter[]
			{
				new SqlParameter ("id", id),
				new SqlParameter ("tabela", tabela)

			};

			var dt = HelperDAO.ExecutaProcSelect("SpConsulta",p);
			if (dt.Rows.Count == 0)
				return null;
			else
				return MontarModel(dt.Rows[0]);
		}

		public virtual int ProximoId()
		{
			var p = new SqlParameter[]
			  {
				new SqlParameter("tabela", tabela)
			  };
			var dt = HelperDAO.ExecutaProcSelect("spProximoId", p);
			return Convert.ToInt32(dt.Rows[0][0]);
		}

		public virtual List<T> Listagem()
		{
			var p = new SqlParameter[]
			{
				new SqlParameter("tabela",tabela)
			};
			var dt = HelperDAO.ExecutaProcSelect(NomeSpListagem, p);
			List <T> lista = new List<T>();
			foreach ( DataRow t in dt.Rows)
				lista.Add(MontarModel(t));
			
			return lista;
		}

	}
}
