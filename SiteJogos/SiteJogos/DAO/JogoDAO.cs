using SiteJogos.Models;
using System.Data;
using System.Data.SqlClient;

namespace SiteJogos.DAO
{
    public class JogoDAO : PadraoDAO<JogosViewModel>
    {
        protected override void SetTabela()
        {
            tabela = "jogos";
        }
        protected override SqlParameter[] CriarParametros(JogosViewModel jogo)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", jogo.id);
            parametros[1] = new SqlParameter("descricao", jogo.descricao);
            parametros[2] = new SqlParameter("valor_locacao", jogo.valorLocacao);
            parametros[3] = new SqlParameter("data_aquisicao", jogo.dataAquicicao);
            parametros[4] = new SqlParameter("categoriaID", jogo.idCategoria);
            return parametros;
        }

        protected override JogosViewModel MontarModel(DataRow registro)
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

    }
}
