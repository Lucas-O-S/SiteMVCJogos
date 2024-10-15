using SiteJogos.Models;
using System.Data;
using System.Data.SqlClient;

namespace SiteJogos.DAO
{
    public class JogoDAO
    {
        private SqlParameter[] CriarParametros(JogosViewModel jogo)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", jogo.id);
            parametros[1] = new SqlParameter("descricao", jogo.descricao);
            parametros[2] = new SqlParameter("valor_locacao", jogo.valorLocacao);
            parametros[3] = new SqlParameter("data_aquisicao", jogo.dataAquicicao);
            parametros[4] = new SqlParameter("categoriaID", jogo.idCategoria);
            return parametros;
        }

        private JogosViewModel MontarJogo(DataRow registro)
        {
            JogosViewModel jogo = new JogosViewModel();

            jogo.id = Convert.ToInt32(registro["id"]);
            jogo.descricao = Convert.ToString(registro["descricao"]);

            jogo.valorLocacao = Convert.ToDecimal(registro["valor_locacao"]);
            jogo.dataAquicicao = Convert.ToDateTime(registro["data_aquisicao"]);

            jogo.idCategoria = Convert.ToInt32(registro["categoriaID"]);

            CategoriaDAO dao = new CategoriaDAO();
            CategoriaViewModel categoria = dao.Consulta(jogo.idCategoria);
            jogo.nomeCategoria = categoria.nome;

            return jogo;
        }

      
        public void Inserir(JogosViewModel jogo)
        {

            HelperDAO.ExecutaProc("spIncluiJogo",CriarParametros(jogo));

        }

        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
                    new SqlParameter("id", id)
            };
            HelperDAO.ExecutaProc("spExcluiJogo", p);

        }

        public void Alterar(JogosViewModel jogo)
        {


            HelperDAO.ExecutaProc("spAlteraJogo",CriarParametros(jogo));
        }

        public JogosViewModel Consulta(int id)
        {
            var p = new SqlParameter[]
             {
                new SqlParameter("id", id)
             };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaJogo",p);

            if (tabela.Rows.Count == 0)
                return null;

            else
                return MontarJogo(tabela.Rows[0]);
        }

        public List<JogosViewModel> Listagem()
        {
            List<JogosViewModel> lista = new List<JogosViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListaJogo", null);
            foreach (DataRow dr in tabela.Rows)
                lista.Add(MontarJogo(dr));
            return lista; 
        }

        public int ProximoID()
        {
            var p = new SqlParameter[]
             {
                new SqlParameter("tabela", "jogos")
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
            return Convert.ToInt32(tabela.Rows[0]["Maior"]);
        }

       


    }
}
