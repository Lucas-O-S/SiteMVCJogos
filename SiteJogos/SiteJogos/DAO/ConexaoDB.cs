using System.Data.SqlClient;

namespace SiteJogos.DAO
{
    public static class ConexaoDB
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=LOCALHOST;  Database=AULADB; user id=SA; password=123456";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
