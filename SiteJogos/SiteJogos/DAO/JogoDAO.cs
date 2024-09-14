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

            return jogo;
        }

        public void Inserir(JogosViewModel jogo)
        {
            string sql = "insert into jogos(id, descricao, valor_locacao, data_aquisicao, categoriaID) " +
                 "values (@id, @descricao, @valor_locacao, @data_aquisicao ,@categoriaID)";
            HelperDAO.ExecutarSQL(sql,CriarParametros(jogo));

        }

        public void Excluir(int id)
        {
            string sql = "delete jogos where id = " + id;
            HelperDAO.ExecutarSQL(sql,null);

        }

        public void Alterar(JogosViewModel jogo)
        {
            string sql = "update jogos set descricao=@descricao," +
            "valor_locacao=@valor_locacao, data_aquisicao=@data_aquisicao," +
            "categoriaID=@categoriaID where id = @id";

            HelperDAO.ExecutarSQL(sql,CriarParametros(jogo));
        }

        public JogosViewModel Consulta(int id)
        {
            string sql = "select * from jogos where id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql,null);

            if (tabela.Rows.Count == 0)
                return null;

            else
                return MontarJogo(tabela.Rows[0]);
        }

        public List<JogosViewModel> Listagem()
        {
            List<JogosViewModel> lista = new List<JogosViewModel>();
            string sql = "select * from Jogos order by descricao";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow dr in tabela.Rows)
                lista.Add(MontarJogo(dr));
            return lista; 
        }

        public int ProximoID()
        {
            string sql = "select isnull(max(id) +1,1) as 'Maior' from jogos";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["Maior"]);
        }
        


    }
}
