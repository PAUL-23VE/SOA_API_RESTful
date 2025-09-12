using MySql.Data.MySqlClient;

namespace SOA_API_RESTful.Data
{
    public static class DB
    {
        private static string connectionString = "Server=localhost;Port=3311;Database=soa_db;User=root;Password=davidgiler21;";

        public static MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
