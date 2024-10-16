using SiteJogos.Models;
using System.Data.SqlClient;
using System.Data;

namespace SiteJogos.DAO 
{
    public class CategoriaDAO : PadraoDAO<CategoriaViewModel>
    {
		protected override void SetTabela()
		{
			tabela = "categorias";
		}
		protected override SqlParameter[] CriarParametros(CategoriaViewModel categoria)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", categoria.id);
            parametros[1] = new SqlParameter("descricao", categoria.nome);
  
            return parametros;
        }

        protected override CategoriaViewModel MontarModel(DataRow registro)
        {
            CategoriaViewModel categoria = new CategoriaViewModel();

            categoria.id = Convert.ToInt32(registro["id"]);
            categoria.nome = Convert.ToString(registro["nome"]);

            return categoria;
        }


    }
}
