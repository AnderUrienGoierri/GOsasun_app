// ============================================================
// DatuBaseKonexioa.cs - MySQL konexio kudeatzailea
// ============================================================
// Singleton patroia erabiliz MySQL datu-baserako konexioa
// kudeatzen duen klasea. Konexio katea era seguruan gordetzen du.
// ============================================================

using MySql.Data.MySqlClient;

namespace GOsasun_app.Modeloak
{
    /// <summary>
    /// MySQL datu-baserako konexioa kudeatzeko klase estatikoa.
    /// Singleton patroia erabiltzen du konexio bakarra bermatzeko.
    /// </summary>
    public static class DatuBaseKonexioa
    {
        // -----------------------------------------------------------
        // Konexio parametroak (zure ingurunera egokitu)
        // -----------------------------------------------------------
        private static readonly string Zerbitzaria = "localhost";
        private static readonly int Portua = 3306;
        private static readonly string DatuBasea = "GOsasun_DB";
        private static readonly string Erabiltzailea = "root";
        private static readonly string Pasahitza = "";

        /// <summary>
        /// Konexio katea eraikitzen du parametroekin.
        /// </summary>
        private static string KonexioKatea =>
            $"Server={Zerbitzaria};" +
            $"Port={Portua};" +
            $"Database={DatuBasea};" +
            $"Uid={Erabiltzailea};" +
            $"Pwd={Pasahitza};" +
            "SslMode=Preferred;" +
            "CharSet=utf8mb4;" +
            "ConnectionTimeout=10;";

        /// <summary>
        /// MySQL konexio berri bat sortzen du eta irekitzen du.
        /// Erabiltzaileak itxi behar du erabili ondoren (using blokea gomendatzen da).
        /// </summary>
        /// <returns>Irekitako MySqlConnection objektua</returns>
        /// <exception cref="MySqlException">Konexioa ezin bada ezarri</exception>
        public static MySqlConnection LortuKonexioa()
        {
            MySqlConnection konexioa = new MySqlConnection(KonexioKatea);
            konexioa.Open();
            return konexioa;
        }

        /// <summary>
        /// Konexioa modu seguruan ixten du.
        /// </summary>
        /// <param name="konexioa">Itxi beharreko konexioa</param>
        public static void ItxiKonexioa(MySqlConnection? konexioa)
        {
            if (konexioa != null && konexioa.State == System.Data.ConnectionState.Open)
            {
                konexioa.Close();
                konexioa.Dispose();
            }
        }

        /// <summary>
        /// Konexioa probatzeko metodoa. True itzultzen du konexioa ondo badabil.
        /// </summary>
        /// <param name="erroreMezua">Errore mezua huts egiten badu</param>
        /// <returns>True konexioa zuzena bada</returns>
        public static bool ProbatuKonexioa(out string erroreMezua)
        {
            erroreMezua = string.Empty;
            try
            {
                using (var konexioa = LortuKonexioa())
                {
                    erroreMezua = "Konexioa zuzena da!";
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                erroreMezua = $"MySQL errorea: {ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                erroreMezua = $"Errore orokorra: {ex.Message}";
                return false;
            }
        }
    }
}
