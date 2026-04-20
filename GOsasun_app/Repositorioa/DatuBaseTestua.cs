using System.Text;

namespace GOsasun_app.Repositorioa
{
    internal static class DatuBaseTestua
    {
        private const string BilaketaKolazioa = "utf8mb4_unicode_ci";

        public static string Zuzendu(string value)
        {
            if (string.IsNullOrEmpty(value) || !DirudiKodifikazioArazoa(value)) return value;

            try
            {
                string zuzenduta = Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(value));
                return zuzenduta.Contains('\uFFFD') ? value : zuzenduta;
            }
            catch
            {
                return value;
            }
        }

        public static string? ZuzenduNullable(string? value)
        {
            return value == null ? null : Zuzendu(value);
        }

        public static string SortuLikeKonparazioaSql(string sqlAdierazpena, string parametroIzena)
        {
            return $"{sqlAdierazpena} COLLATE {BilaketaKolazioa} LIKE (CONVERT({parametroIzena} USING utf8mb4) COLLATE {BilaketaKolazioa})";
        }

        public static string SortuLikeMultzoaSql(string parametroIzena, params string[] sqlAdierazpenak)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < sqlAdierazpenak.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(" OR ");
                }

                builder.Append(SortuLikeKonparazioaSql(sqlAdierazpenak[i], parametroIzena));
            }

            return builder.ToString();
        }

        public static string SortuBerdinketaKonparazioaSql(string sqlAdierazpena, string parametroIzena)
        {
            return $"{sqlAdierazpena} COLLATE {BilaketaKolazioa} = (CONVERT({parametroIzena} USING utf8mb4) COLLATE {BilaketaKolazioa})";
        }

        private static bool DirudiKodifikazioArazoa(string value)
        {
            return value.IndexOf('\u00C3') >= 0
                || value.IndexOf('\u00C2') >= 0
                || value.IndexOf('\u00E2') >= 0;
        }
    }
}