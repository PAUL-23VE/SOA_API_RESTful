using MySql.Data.MySqlClient;

namespace SOA_API_RESTful.Data
{
    public static class DB
    {
        private static string connectionString = "Server=10.79.25.70;Port=3306;Database=soa_db;User=paul;Password=123;";

        public static MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
