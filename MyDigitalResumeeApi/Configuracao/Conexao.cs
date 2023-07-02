using System.Data.SqlClient;

namespace MyDigitalResumeeApi.Configuracao
{
    public class Conexao
    {
        private static IConfiguration Configuration;
        public static SqlConnection SqlConnection = new SqlConnection(Configuration.GetConnectionString("MyDigitalResumeeDb"));
    }
}
