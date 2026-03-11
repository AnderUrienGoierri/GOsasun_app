using System;
using MySql.Data.MySqlClient;

namespace GOsasun_WinForms
{
    public class DatuBaseKonexioa
    {
        private string connectionString;

        public DatuBaseKonexioa()
        {
            connectionString = "server=127.0.0.1;userid=root;password=1MG32025;database=GOsasun_DB";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
