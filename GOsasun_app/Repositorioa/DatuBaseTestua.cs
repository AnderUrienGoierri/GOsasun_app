using System.Text;

namespace GOsasun_app.Repositorioa
{
    internal static class DatuBaseTestua
    {
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

        private static bool DirudiKodifikazioArazoa(string value)
        {
            return value.IndexOf('\u00C3') >= 0
                || value.IndexOf('\u00C2') >= 0
                || value.IndexOf('\u00E2') >= 0;
        }
    }
}