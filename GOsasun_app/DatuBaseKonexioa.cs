using System;
using MySql.Data.MySqlClient;

namespace GOsasun_WinForms
{
    public class DatuBaseKonexioa
    {
        private string server = "127.0.0.1";
        private string userid = "root";
        private string password = "1MG32025";
        private string database = "GOsasun_DB";
        private string connectionString;

        public DatuBaseKonexioa()
        {
            connectionString = $"server={server};userid={userid};password={password};database={database}";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
