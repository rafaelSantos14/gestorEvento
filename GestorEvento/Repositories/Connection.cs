using System.Configuration;

namespace GestorEvento.Repositories
{
    public static class Connection
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }
    }
}
