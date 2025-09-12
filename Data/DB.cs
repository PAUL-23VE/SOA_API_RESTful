using MySql.Data.MySqlClient;

namespace SOA_API_RESTful.Data
{
    public static class DB
    {
        private static string connectionString = "Server=localhost;Database=soa_db;User=root;Password=tu_password;";

        public static MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
