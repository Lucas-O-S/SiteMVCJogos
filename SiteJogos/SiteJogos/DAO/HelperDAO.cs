using System.Data;
using System.Data.SqlClient;

namespace SiteJogos.DAO
{
    public static class HelperDAO
    {

        public static void ExecutarSQL(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(sql, conexao))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();
                    
                    conexao.Close();
                }
            }

        }

        public static DataTable ExecutaSelect(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql,conexao))
                {
                    if (parametros!= null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    conexao.Close();
                    return tabela;

                }
            }
        }

        public static void ExecutaProc(string nomeProc, SqlParameter[] parametros)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(nomeProc, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros!=null)
                                   comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();
                    

                }
            }
        }

        public static DataTable ExecutaProcSelect(string nomeProc, SqlParameter[] parameters)
        {
            using (SqlConnection conexao = ConexaoDB.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(nomeProc, conexao))
                {
                    if (parameters != null)
                        adapter.SelectCommand.Parameters.AddRange(parameters);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    return tabela;
                }
            }
        }

    }
}
