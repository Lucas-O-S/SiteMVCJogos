using SiteJogos.Models;
using System.Data.SqlClient;
using System.Data;

namespace SiteJogos.DAO
{
    public class CategoriaDAO
    {
        private SqlParameter[] CriarParametros(CategoriaViewModel categoria)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", categoria.id);
            parametros[1] = new SqlParameter("descricao", categoria.nome);
  
            return parametros;
        }

        private CategoriaViewModel MontarCategoria(DataRow registro)
        {
            CategoriaViewModel categoria = new CategoriaViewModel();

            categoria.id = Convert.ToInt32(registro["id"]);
            categoria.nome = Convert.ToString(registro["nome"]);

            return categoria;
        }


        public void Inserir(CategoriaViewModel categoria)
        {

            HelperDAO.ExecutaProc("spIncluiJogo", CriarParametros(categoria));

        }

        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
                    new SqlParameter("id", id)
            };
            HelperDAO.ExecutaProc("spExcluiJogo", p);

        }

        public void Alterar(CategoriaViewModel categoria)
        {


            HelperDAO.ExecutaProc("spAlteraJogo", CriarParametros(categoria));
        }

        public CategoriaViewModel Consulta(int id)
        {
            var p = new SqlParameter[]
             {
                new SqlParameter("id", id)
             };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaCategoria", p);

            if (tabela.Rows.Count == 0)
                return null;

            else
                return MontarCategoria(tabela.Rows[0]);
        }

        public List<CategoriaViewModel> Listagem()
        {
            List<CategoriaViewModel> lista = new List<CategoriaViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListaCategoria", null);
            foreach (DataRow dr in tabela.Rows)
                lista.Add(MontarCategoria(dr));
            return lista;
        }

        public int ProximoID()
        {
            var p = new SqlParameter[]
             {
                new SqlParameter("tabela", "categorias")
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
            return Convert.ToInt32(tabela.Rows[0]["Maior"]);
        }

    }
}
